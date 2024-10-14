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


    protected ArenaPlace(GopetMap m, int ID) : base(m, ID)
    {

    }

    public ArenaPlace(Player playerOne, Player playerTwo, GopetMap m, int ID) : this(m, ID)
    {
        placeTime = Utilities.CurrentTimeMillis + 60000 * 2;
        startFightPlayer(playerOne.user.user_id, playerTwo, false, 0);
        sendTimePlace();
    }

    public void addArena(Player playerOne, Player playerTwo)
    {
        _data.Add(new ArenaData(playerOne, playerTwo));
        this.add(playerOne);
        this.add(playerTwo);
        startFightPlayer(playerOne.user.user_id, playerTwo, false, 0);
        sendTimePlace();
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

    public class ArenaData
    {
        public Player PlayerOne { get; }
        public Player PlayerTwo { get; }

        public long placeTime { get; }

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

        public ArenaData(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne ?? throw new ArgumentNullException(nameof(playerOne));
            PlayerTwo = playerTwo ?? throw new ArgumentNullException(nameof(playerTwo));
            PlayerTwo.playerData.x = 174;
            PlayerTwo.playerData.y = 181 + 39;
            PlayerOne.playerData.x = 258;
            PlayerOne.playerData.y = 181 + 39;
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