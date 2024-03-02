using Gopet.Data.Collections;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event
{
    public class ArenaEvent : EventBase
    {
        public static ArenaEvent Instance { get; private set; } = new ArenaEvent();

        public bool IsRunning { get; set; } = false;
        public bool IsFighting { get; set; } = false;

        public override string Name => "Đấu trường";
        public override bool Condition
        {
            get
            {
                if (IsRunning || IsFighting) return true;
                return DateTime.Now.Hour == 12 && DateTime.Now.Minute <= 5;
            }
        }


        public bool CanJournalism
        {
            get
            {
                return timeWaitPlayerJournalism > 0;
            }
        }

        public override bool NeedRemove => false;

        private long timeWaitPlayerJournalism = 0;
        private long lastTimeWait = 0;
        private long timeWaitNextTurn = 0;
        private uint showBanner = 0;
        public const long TIME_WAIT_COST = 60000 * 30;
        public const long TIME_WAIT_TURN_COST = 60000 * 3;
        public CopyOnWriteArrayList<int> IdPlayerJoin = new CopyOnWriteArrayList<int>();


        public bool CanRegister()
        {
            return !IsFighting;
        }

        public override void Update()
        {
            if (!IsFighting)
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    timeWaitPlayerJournalism = TIME_WAIT_COST;
                    lastTimeWait = Utilities.CurrentTimeMillis;
                }

                if (Utilities.CurrentTimeMillis - lastTimeWait >= 1000)
                {
                    timeWaitPlayerJournalism -= Utilities.CurrentTimeMillis - lastTimeWait;
                    lastTimeWait = Utilities.CurrentTimeMillis;
                    showBanner++;
                    if (timeWaitPlayerJournalism <= 0)
                    {
                        IsFighting = true;
                        NextTurn();
                    }
                    else if (showBanner % 60 == 0)
                    {
                        PlayerManager.showBanner($"Các người chơi nhanh chóng đến đấu trường báo danh tham gia lôi đài còn {timeWaitPlayerJournalism / 60000l} phút nữa bắt đầu rồi!!! ");
                    }
                }
            }
            else if (IsFighting)
            {
                if (MapManager.maps[MapManager.ID_MAP_INSIDE_ARENA].places.Any(p => p is ArenaPlace))
                {
                    return;
                }
                if (IdPlayerJoin.Count <= 1)
                {
                    IsFighting = false;
                    IsRunning = false;
                    IdPlayerJoin.Clear();
                }
                else
                {
                    NextTurn();
                }
            }
        }

        public void NextTurn()
        {
            foreach (var id in IdPlayerJoin.ToArray())
            {
                Player player = PlayerManager.get(id);

                if (player == null)
                {
                    IdPlayerJoin.remove(id);
                    continue;
                }
                else
                {
                    GopetPlace gopetPlace = (GopetPlace)player.getPlace();
                    if (gopetPlace == null)
                    {
                        IdPlayerJoin.remove(id);
                        continue;
                    }
                    else
                    {
                        if (gopetPlace.map.mapID != MapManager.ID_MAP_OUTSIDE_ARENA)
                        {
                            IdPlayerJoin.remove(id);
                            continue;
                        }
                    }
                }
            }

            var arr = IdPlayerJoin.ToArray().ToList();
            IdPlayerJoin.Clear();
            while (arr.Count > 0)
            {
                if (arr.Count == 1)
                {
                    Player player = PlayerManager.get(arr[0]);
                    if (player != null)
                    {
                        player.okDialog($"Vòng này đối thủ của bạn đã đầu hàng bạn nhận được 1 (diem)");
                        player.playerData.AccumulatedPoint++;
                        IdPlayerJoin.addIfAbsent(arr[0]);
                        HistoryManager.addHistory(new History(player).setLog($"Thắng  do đối thủ bỏ cuộc nhận 1 điểm hiện tại có {player.playerData.AccumulatedPoint}"));
                    }
                    arr.Clear();
                }
                else
                {
                    int playerOne = Utilities.RandomArray(arr);
                    arr.Remove(playerOne);
                    int playerTwo = Utilities.RandomArray(arr);
                    arr.Remove(playerTwo);
                    Player player1 = PlayerManager.get(playerOne);
                    Player player2 = PlayerManager.get(playerTwo);

                    if (player1 != null && player2 != null)
                    {
                        MapManager.maps[MapManager.ID_MAP_INSIDE_ARENA].addPlace(new ArenaPlace(player1, player2, MapManager.maps[MapManager.ID_MAP_INSIDE_ARENA], Utilities.nextInt(0, 232421341)));
                    }
                    else if (player2 != null)
                    {
                        player2.okDialog($"Vòng này đối thủ của bạn đã đầu hàng bạn nhận được 1 (diem)");
                        player2.playerData.AccumulatedPoint++;
                        IdPlayerJoin.addIfAbsent(player2.playerData.user_id);
                        HistoryManager.addHistory(new History(player2).setLog($"Thắng đối thủ do đối thủ bỏ cuộc nhận 1 điểm hiện tại có {player2.playerData.AccumulatedPoint}"));
                    }
                    else if (player1 != null)
                    {
                        player1.okDialog($"Vòng này đối thủ của bạn đã đầu hàng bạn nhận được 1 (diem)");
                        player1.playerData.AccumulatedPoint++;
                        IdPlayerJoin.addIfAbsent(player1.playerData.user_id);
                        HistoryManager.addHistory(new History(player1).setLog($"Thắng đối thủ do đối thủ bỏ cuộc nhận 1 điểm hiện tại có {player1.playerData.AccumulatedPoint}"));
                    }
                }
            }
        }
    }
}
