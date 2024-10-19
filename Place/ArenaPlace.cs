using Gopet.Data.Collections;
using Gopet.Data.Event;
using Gopet.Data.Map;
using Gopet.IO;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class ArenaPlace : GopetPlace
{
    private CopyOnWriteArrayList<ArenaData> _data = new CopyOnWriteArrayList<ArenaData>();

    public static readonly PointArena[] POINTS = new PointArena[]
    {
        new (174, 220, 258, 220),
        new (71, 91, 138, 97),
        new (285, 90, 356, 97),
        new (70, 311, 138, 309),
        new (285, 313, 352, 314)
    };

    public ArenaPlace(GopetMap m, int ID) : base(m, ID)
    {

    }


    public void addArena(Player playerOne, Player playerTwo)
    {
        PointArena pointArena = POINTS.Where(x => !this._data.Any(m => m.arena == x)).FirstOrDefault();
        _data.Add(new ArenaData(playerOne, playerTwo, pointArena));
        this.add(playerOne);
        this.add(playerTwo);
        startFightPlayer(playerOne.user.user_id, playerTwo, false, 0);
        sendTimePlace();
    }

    public override bool canAdd(Player player)
    {
        return this._data.Count < 5;
    }

    public override bool needRemove()
    {
        return this.players.IsEmpty || this._data.IsEmpty;
    }

    private bool isDisposed = false;

    public override void removeAllPlayer()
    {

    }

    public override void update()
    {
        base.update();
        if (!this._data.IsEmpty)
        {
            foreach (var item in _data)
            {
                if (item.needRemove())
                {
                    _data.remove(item);
                    item.removeAllPlayer();
                }
            }
        }
    }

    public struct PointArena : IEquatable<PointArena>
    {
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }

        public int Y2 { get; }

        public PointArena(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public override bool Equals(object? obj)
        {
            return obj is PointArena arena && Equals(arena);
        }

        public bool Equals(PointArena other)
        {
            return X1 == other.X1 &&
                   Y1 == other.Y1 &&
                   X2 == other.X2 &&
                   Y2 == other.Y2;
        }

        public static bool operator ==(PointArena left, PointArena right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PointArena left, PointArena right)
        {
            return !(left == right);
        }
    }

    public class ArenaData
    {
        public Player PlayerOne { get; }
        public Player PlayerTwo { get; }

        public long placeTime { get; }

        public PointArena arena { get; }

        public void sendTimePlace()
        {
            Message message = GopetPlace.messagePetService(GopetCMD.TIME_PLACE);
            message.putInt(Utilities.round(placeTime - Utilities.CurrentTimeMillis) / 1000);
            message.cleanup();
            foreach (var item in new Player[] { PlayerOne, PlayerTwo })
            {
                item.session.sendMessage(message);
            }
        }

        public ArenaData(Player playerOne, Player playerTwo, PointArena arena)
        {
            PlayerOne = playerOne ?? throw new ArgumentNullException(nameof(playerOne));
            PlayerTwo = playerTwo ?? throw new ArgumentNullException(nameof(playerTwo));
            this.arena = arena;
            PlayerTwo.playerData.x = arena.X1;
            PlayerTwo.playerData.y = arena.Y1;
            PlayerOne.playerData.x = arena.X2;
            PlayerOne.playerData.y = arena.Y2;
            placeTime = Utilities.CurrentTimeMillis + 60000 * 2;
            sendTimePlace();
        }

        public bool needRemove()
        {
            return placeTime < Utilities.CurrentTimeMillis || PlayerOne.playerData.petSelected.hp <= 0 || PlayerTwo.playerData.petSelected.hp <= 0;
        }


        public void removeAllPlayer()
        {
            try
            {
                if (PlayerOne.playerData.petSelected.hp <= 0)
                {
                    PlayerTwo.playerData.AccumulatedPoint++;
                    PlayerTwo.Popup(PlayerTwo.Language.WinEventMessage + " (diem)");
                    ArenaEvent.Instance.IdPlayerJoin.addIfAbsent(PlayerTwo.playerData.user_id);
                    HistoryManager.addHistory(new History(PlayerTwo).setLog($"Thắng đối thủ trong map lôi đài nhận 1 điểm hiện tại có {PlayerTwo.playerData.AccumulatedPoint}"));
                }
                else if (PlayerTwo.playerData.petSelected.hp <= 0)
                {
                    PlayerOne.playerData.AccumulatedPoint++;
                    PlayerOne.Popup(PlayerOne.Language.WinEventMessage + " (diem)");
                    ArenaEvent.Instance.IdPlayerJoin.addIfAbsent(PlayerOne.playerData.user_id);
                    HistoryManager.addHistory(new History(PlayerOne).setLog($"Thắng đối thủ trong map lôi đài nhận 1 điểm hiện tại có {PlayerOne.playerData.AccumulatedPoint}"));
                }
                else if (PlayerOne.playerData.petSelected.hp < PlayerTwo.playerData.petSelected.hp)
                {
                    PlayerTwo.playerData.AccumulatedPoint++;
                    PlayerTwo.Popup(PlayerTwo.Language.WinEventMessage + " (diem)");
                    ArenaEvent.Instance.IdPlayerJoin.addIfAbsent(PlayerTwo.playerData.user_id);
                    HistoryManager.addHistory(new History(PlayerTwo).setLog($"Thắng đối thủ trong map lôi đài nhận 1 điểm hiện tại có {PlayerTwo.playerData.AccumulatedPoint}"));
                }
                else
                {
                    PlayerOne.playerData.AccumulatedPoint++;
                    PlayerOne.Popup(PlayerOne.Language.WinEventMessage + " (diem)");
                    ArenaEvent.Instance.IdPlayerJoin.addIfAbsent(PlayerOne.playerData.user_id);
                    HistoryManager.addHistory(new History(PlayerOne).setLog($"Thắng đối thủ trong map lôi đài nhận 1 điểm hiện tại có {PlayerOne.playerData.AccumulatedPoint}"));
                }
                foreach (var p in new Player[] { PlayerOne, PlayerTwo })
                {
                    p.playerData.x = 154;
                    p.playerData.y = 253;
                    MapManager.maps[MapManager.ID_MAP_OUTSIDE_ARENA].addRandom(p);
                }
            }
            catch (Exception ex)
            {
                ex.printStackTrace();
            }
        }
    }
}