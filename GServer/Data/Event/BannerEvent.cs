using Gopet.Data.Collections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event
{
    internal class BannerEvent : EventBase
    {
        public static BannerEvent Instance = new BannerEvent();

        public CopyOnWriteArrayList<Tuple<string, TimeSpan, DateTime>> Banners = new();

        private ConcurrentDictionary<string, DateTime> WaitData = new();

        public BannerEvent()
        {

        }


        public override bool NeedRemove => false;

        public override bool Condition => true;

        public override string Name => "Banner Auto";

        public override void Update()
        {
            foreach (var item1 in Banners)
            {
                if (item1.Item3 < DateTime.Now)
                {
                    Banners.Remove(item1);
                    continue;
                }

                if (WaitData.TryGetValue(item1.Item1, out var time))
                {
                    if (time < DateTime.Now)
                    {
                        goto SHOW_BANNER;
                    }
                    continue;
                }
            SHOW_BANNER:
                PlayerManager.showBannerZ(item1.Item1);
                WaitData[item1.Item1] = DateTime.Now + item1.Item2;
            }
        }
    }
}
