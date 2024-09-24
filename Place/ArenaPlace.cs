using Gopet.Data.Event;
using Gopet.Data.Map;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class ArenaPlace : GopetPlace
{

    public Player PlayerOne { get; }
    public Player PlayerTwo { get; }

    protected ArenaPlace(GopetMap m, int ID) : base(m, ID)
    {

    }

    public ArenaPlace(Player playerOne, Player playerTwo, GopetMap m, int ID) : this(m, ID)
    {
        PlayerOne = playerOne ?? throw new ArgumentNullException(nameof(playerOne));
        PlayerTwo = playerTwo ?? throw new ArgumentNullException(nameof(playerTwo));
        PlayerTwo.playerData.x = 174;
        PlayerTwo.playerData.y = 181 + 39;
        PlayerOne.playerData.x = 258;
        PlayerOne.playerData.y = 181 + 39;
        add(playerOne);
        add(playerTwo);
        placeTime = Utilities.CurrentTimeMillis + 60000 * 2;
        startFightPlayer(playerOne.user.user_id, playerTwo, false, 0);
        sendTimePlace();
    }

    public override bool needRemove()
    {
        return placeTime < Utilities.CurrentTimeMillis || PlayerOne.playerData.petSelected.hp <= 0 || PlayerTwo.playerData.petSelected.hp <= 0;
    }

    private bool isDisposed = false;

    public override void removeAllPlayer()
    {
        if (isDisposed) return;

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

            foreach (var p in players)
            {
                p.playerData.x = 154;
                p.playerData.y = 253;
                MapManager.maps[MapManager.ID_MAP_OUTSIDE_ARENA].addRandom(p);
            }
        }
        catch(Exception ex)
        {
            ex.printStackTrace();
        }
        isDisposed = true;
    }
}