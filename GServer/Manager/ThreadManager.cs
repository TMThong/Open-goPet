using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Manager
{
    internal class ThreadManager
    {
        public const sbyte THREAD_MSG_SENDER = 0;

        public static readonly CopyOnWriteArrayList<Thread> THREADS = new CopyOnWriteArrayList<Thread>();

        private static Mutex mutex = new Mutex();

        static ThreadManager()
        {
            new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        foreach (var item in THREADS)
                        {
                            if (!item.IsAlive)
                            {
                                //GopetManager.ServerMonitor.LogWarning("REMOVE THREAD: " + item.Name);
                                RemoveThread(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.printStackTrace();
                    }
                    Thread.Sleep(1000);
                }
            })
            { IsBackground = true }.Start();

        }

        public static void AddThread(Thread thread)
        {
            if (thread == null)
            {
                return;
            }
            THREADS.Add(thread);
        }

        public static void RemoveThread(Thread thread)
        {
            THREADS.remove(thread);
        }
    }
}
