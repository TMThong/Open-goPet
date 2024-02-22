using Gopet.Data.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public static void Start()
        {
            IsRunning = true;
            thread.IsBackground = true;
            thread.Name = "Luồng Event";
            thread.Start();
        }

        private static void Run()
        {
            while (IsRunning)
            {
                foreach (var item in _events.ToArray())
                {
                    if(item.Condition)
                    {
                        try
                        {
                            item.Update();
                        }
                        catch(Exception ex)
                        {
                            GopetManager.ServerMonitor.LogError(ex.ToString());
                            ResetEvent.WaitOne(1000);
                        }
                    }

                    if(item.NeedRemove)
                    {
                        _events.Remove(item);
                    }
                }
                ResetEvent.WaitOne(1000);
            }
        }
    }
}
