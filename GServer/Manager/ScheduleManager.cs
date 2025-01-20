
using Gopet.Data.Collections;
using Gopet.Util;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Gopet.Manager
{
    /// <summary>
    /// Quản lí hẹn giờ
    /// </summary>
    public class ScheduleManager : UpdateThread
    {
        public static readonly ScheduleManager Instance = new ScheduleManager();
        public CopyOnWriteArrayList<ScheduleItem> ScheduleItems { get; } = new CopyOnWriteArrayList<ScheduleItem>();

        public class ScheduleItem
        {
            public int? Hour { get; set; }

            public int? Minute { get; set; }

            public bool IsNeedRemove { get; set; } = false;

            public DateTime NextExcute { get; set; } = DateTime.MinValue;

            public ScheduleItem(int? hour, int? minute, bool isNeedRemove)
            {
                Hour = hour;
                Minute = minute;
                IsNeedRemove = isNeedRemove;
            }

            public ScheduleItem(int? hour, int? minute)
            {
                Hour = hour;
                Minute = minute;
            }

            public ScheduleItem(int? minute)
            {
                Minute = minute;
            }

            public ScheduleItem()
            {
            }

            public virtual void Execute()
            {

            }
        }
        protected ScheduleManager() : base("Quản lí hẹn")
        {
            TimeSleep = TimeSpan.FromMinutes(1);
        }


        public void AddScheduleItem(ScheduleItem item)
        {
            ScheduleItems.Add(item);
        }

        public override void Update()
        {
            foreach (var item in ScheduleItems)
            {
                DateTime now = DateTime.Now;
                if ((!item.Hour.HasValue || (item.Hour.HasValue && item.Hour.Value == now.Hour)) && (!item.Minute.HasValue || (item.Minute.HasValue && item.Minute.Value == now.Minute)) && item.NextExcute < DateTime.Now.AddMilliseconds(100))
                {
                    item.NextExcute = DateTime.Now.AddDays(1);
                    try
                    {
                        item.Execute();
                    }
                    catch (Exception e)
                    {
                        GopetManager.ServerMonitor.LogError(e.GetBaseException().ToString());
                    }
                }
                if (item.IsNeedRemove)
                {
                    this.ScheduleItems.Remove(item);
                }
            }
        }
    }
}
