using Gopet.Data.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gopet.Data.Event.Year2024;
namespace Gopet.Manager
{
    public class EventManager
    {
        private static readonly List<EventBase> _events = new List<EventBase>();
        static Thread thread = new Thread(Run);
        public static AutoResetEvent ResetEvent = new AutoResetEvent(false);
        public static bool IsRunning { get; private set; } = false;

        static EventManager()
        {
            _events.Add(ArenaEvent.Instance);
            _events.Add(Summer2024Event.Instance);
        }

        public static void Start()
        {
            IsRunning = true;
            thread.IsBackground = true;
            thread.Name = "Luồng Event";
            thread.Start();
        }

        public static void FindAndUseItemEvent(int itemId, Player player)
        {
            var findEvent = _events.Where(p => p.ItemsOfEvent.Contains(itemId));
            if (findEvent.Any())
            {
                findEvent.First().UseItem(itemId, player);
            }
            else
            {
                player.redDialog(player.Language.CannotFindEvents);
            }
        }

        private static void Run()
        {
            while (IsRunning)
            {
                foreach (var item in _events.ToArray())
                {
                    if (item.Condition)
                    {
                        try
                        {
                            item.Update();
                        }
                        catch (Exception ex)
                        {
                            GopetManager.ServerMonitor.LogError(ex.ToString());
                            ResetEvent.WaitOne(1000);
                        }
                    }

                    if (item.NeedRemove)
                    {
                        _events.Remove(item);
                    }
                }
                ResetEvent.WaitOne(1000);
            }
        }
    }
}
