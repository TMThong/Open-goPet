using Gopet.Data.Map;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ArenaPlace : GopetPlace
{

    public Player PlayerOne { get; }
    public Player PlayerTwo { get; }

    private long timeWait = 0;

    protected ArenaPlace(GopetMap m, int ID) : base(m, ID)
    {

    }

    public ArenaPlace(Player playerOne, Player playerTwo, GopetMap m, int ID) : this(m, ID)
    {
        PlayerOne = playerOne ?? throw new ArgumentNullException(nameof(playerOne));
        PlayerTwo = playerTwo ?? throw new ArgumentNullException(nameof(playerTwo));
        add(playerOne);
        add(playerTwo);
        timeWait = Utilities.CurrentTimeMillis + 60000 * 2;
    }

    public override void update()
    {
        base.update();
    }

    public override bool needRemove()
    {
        return timeWait < Utilities.CurrentTimeMillis || PlayerOne.playerData.petSelected.hp <= 0 || PlayerTwo.playerData.petSelected.hp <= 0;
    }

    public override void removeAllPlayer()
    {
        if (PlayerOne.playerData.petSelected.hp <= 0)
        {
            PlayerTwo.playerData.ArenaPoint++;
            PlayerTwo.Popup("Thắng trận nhận được 1 (diem)");
        }
        else if (PlayerTwo.playerData.petSelected.hp <= 0)
        {
            PlayerOne.playerData.ArenaPoint++;
            PlayerOne.Popup("Thắng trận nhận được 1 (diem)");
        }
        else if (PlayerOne.playerData.petSelected.hp > PlayerTwo.playerData.petSelected.hp)
        {
            PlayerTwo.playerData.ArenaPoint++;
            PlayerTwo.Popup("Thắng trận nhận được 1 (diem)");
        }
        else
        {
            PlayerOne.playerData.ArenaPoint++;
            PlayerOne.Popup("Thắng trận nhận được 1 (diem)");
        }

        foreach (var p in players)
        {
            p.playerData.x = 154;
            p.playerData.y = 253;
            MapManager.maps[MapManager.ID_MAP_INSIDE_ARENA].addRandom(p);
        }
    }
}