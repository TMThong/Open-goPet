
using Dapper;
using Gopet.Data.Collections;
using Gopet.Data.Map;
using Gopet.Data.Mob;
using Gopet.Data.User;
using Gopet.IO;
using Gopet.Util;
using System.Diagnostics;

public class ChallengePlace : GopetPlace
{
    public const long TIME_WAIT = 60 * 1000;
    public const long TIME_ATTACK = 60 * 3 * 1000;
    public const int MAX_PLAYER_JOIN = 4;
    public const int MAX_TURN = 25;
    public bool isWait = true;
    public bool isFinish = false;
    public bool isWaitForNewTurn = false;
    public int turn = 0;
    public static readonly int[][] MOB_XY = new int[][]{
        new int[]{240, 161},
        new int[]{341, 97},
        new int[]{300, 229},
        new int[]{95, 323},
        new int[]{35, 181},
        new int[]{79, 59},
        new int[]{347, 347},
        new int[]{333, 261},
        new int[]{200, 161},
        new int[]{301, 97},
        new int[]{339, 229},
        new int[]{135, 323},
        new int[]{55, 181},
        new int[]{119, 59},
        new int[]{347, 347},
        new int[]{302, 255},
        new int[]{12, 90}
    };
    public static readonly Dictionary<int, int> MOB_LVL = new Dictionary<int, int>()
    {
        [1] = 3,
        [2] = 4,
        [3] = 5,
        [4] = 6,
        [6] = 11,
        [7] = 12,
        [8] = 13,
        [9] = 14,
        [11] = 21,
        [12] = 22,
        [13] = 23,
        [14] = 24,
        [16] = 31,
        [17] = 32,
        [18] = 33,
        [19] = 34,
        [21] = 42,
        [22] = 43,
        [23] = 44,
        [24] = 45,
    };

    private Stopwatch stopwatch = new Stopwatch();
    private CopyOnWriteArrayList<int> TeamId = new CopyOnWriteArrayList<int>();
    private CopyOnWriteArrayList<string> TeamName = new CopyOnWriteArrayList<string>();

    public ChallengePlace(GopetMap m, int ID) : base(m, ID)
    {

        placeTime = Utilities.CurrentTimeMillis + TIME_WAIT;
        maxPlayer = MAX_PLAYER_JOIN;
    }


    public override void add(Player player)
    {
        base.add(player);
        player.controller.sendPlaceTime(Utilities.round(placeTime - Utilities.CurrentTimeMillis) / 1000);
        player.controller.showBigTextEff(player.Language.WaitRoomPlace);
        TeamId.Add(player.user.user_id);
        TeamName.Add(player.playerData.name);
    }


    public override bool canAdd(Player player)
    {
        return base.canAdd(player) && isWait;
    }


    public override void update()
    {
        base.update();
        if (isWait)
        {
            if (placeTime < Utilities.CurrentTimeMillis)
            {
                isWait = false;
                stopwatch.Start();
                nextTurn();
            }
        }
        else if (mobs.Count == 0)
        {
            if (isWaitForNewTurn && Utilities.CurrentTimeMillis > placeTime)
            {
                nextTurn();
            }
            else if (!isWaitForNewTurn)
            {
                isWaitForNewTurn = true;
                placeTime = Utilities.CurrentTimeMillis + 15000;
                this.sendTimePlace();
            }
        }
    }


    public override bool needRemove()
    {
        return !isWait && (numPlayer == 0 || placeTime < Utilities.CurrentTimeMillis);
    }


    public override void removeAllPlayer()
    {
        foreach (Player player in players)
        {
            player.controller.LoadMap();
        }
        this.stopwatch.Stop();
        TopChallengeData topChallengeData = new TopChallengeData(0, 0, this.stopwatch.Elapsed, turn, this.TeamName.ToArray(), this.TeamId.ToArray());
        using (var conn = MYSQLManager.create())
        {
            conn.Execute("INSERT INTO `top_challenge`(`Type`, `Time`, `Turn`, `Name`, `TeamId`) VALUES (@Type, @Time, @Turn, @Name, @TeamId)", topChallengeData);
        }
    }

    private void nextTurn()
    {
        turn++;
        isWaitForNewTurn = false;
        if (turn <= MAX_TURN)
        {
            PetTemplate[] templates = GopetManager.petEnable.ToArray();
            bool isBossTurn = turn % 5 == 0;
            if (!isBossTurn)
            {
                for (int i = 0; i < getNumMob(); i++)
                {
                    int[] XY = MOB_XY[getNumMob() > MOB_XY.Length ? i % MOB_XY.Length : i];
                    PetTemplate petTemplate = Utilities.RandomArray(templates);
                    MobLocation mobLocation = new MobLocation(this.map.mapID, XY[0], XY[1]);
                    int lvlMob = turn;
                    if (MOB_LVL.ContainsKey(turn))
                    {
                        lvlMob = MOB_LVL[turn];
                    }
                    MobLvlMap mobLvlMap = new MobLvlMap(map.mapID, lvlMob, lvlMob, petTemplate.petId);
                    MobLvInfo mobLvInfo = GopetManager.MOBLVLINFO_HASH_MAP.get(lvlMob);
                    Mob mob = new Mob(petTemplate, this, mobLvlMap, mobLocation, mobLvInfo);
                    mob.setMobId(-(i + 1));
                    this.addNewMob(mob);
                }
            }
            else
            {
                int indexBoss = (turn / 5) - 1;
                for (int i = 0; i < numBoss(); i++)
                {
                    int[] XY = MOB_XY[i];
                    MobLocation mobLocation = new MobLocation(this.map.mapID, XY[0], XY[1]);
                    Boss b = new Boss(GopetManager.ID_BOSS_CHALLENGE[indexBoss], mobLocation);
                    b.TimeOut = DateTime.Now.AddMilliseconds(TIME_ATTACK);
                    b.setMobId(-(i + 1));
                    this.addNewMob(b);
                }
            }

            foreach (Player player in players)
            {
                player.controller.getTaskCalculator().onNextChellengePlace(this.turn);
                this.add(player);
            }
            placeTime = Utilities.CurrentTimeMillis + TIME_ATTACK;
            this.sendTimePlace();
            this.showBigTextEff(string.Format("LƯỢT {0}", turn));
        }
        else
        {
            isFinish = true;
        }
    }

    private int getNumMob()
    {
        if (this.numPlayer <= 1)
        {
            return 4;
        }
        else
        {
            return 4 + (numPlayer - 1) * 4;
        }
    }

    private int numBoss()
    {
        if (numPlayer >= 4)
        {
            return 2;
        }
        return 1;
    }


    public void mobDie(Mob gopetMob)
    {
        this.mobs.remove(gopetMob);
    }

    ~ChallengePlace()
    {
        this.stopwatch.Stop();
    }
}
