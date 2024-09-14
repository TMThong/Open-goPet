using Gopet.Data.Mob;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event
{
    internal class DailyBossEvent : EventBase
    {
        public static DailyBossEvent Instance;

        private Dictionary<int, BossTemplate[]> BossTimer = new Dictionary<int, BossTemplate[]>();
        private List<BossTemplate> WasSummonBoss = new List<BossTemplate>();

        public DailyBossEvent()
        {
            Dictionary<int, List<BossTemplate>> keyValuePairs = new Dictionary<int, List<BossTemplate>>();
            foreach (var item1 in GopetManager.HourDailyBoss)
            {
                foreach (var item2 in item1.HourSummon)
                {
                    if (keyValuePairs.TryGetValue(item2, out var list))
                    {
                        list.Add(item1);
                    }
                    else
                    {
                        keyValuePairs[item2] = new List<BossTemplate>(new BossTemplate[] { item1 });
                    }
                }
            }
            foreach (var item1 in keyValuePairs)
            {
                BossTimer[item1.Key] = item1.Value.ToArray();
            }
        }

        public override bool Condition
        {
            get
            {
                return BossTimer.TryGetValue(DateTime.Now.Hour, out BossTemplate[] bosss);
            }
        }

        public override void Update()
        {
            if (BossTimer.TryGetValue(DateTime.Now.Hour, out BossTemplate[] bosss))
            {
                foreach (var item1 in bosss)
                {
                    if (this.WasSummonBoss.Contains(item1))
                    {
                        continue;
                    }
                    int mapid = -1;
                    for (global::System.Int32 i = 0; i < item1.HourSummon.Length; i++)
                    {
                        if (item1.HourSummon[i] == DateTime.Now.Hour)
                        {
                            mapid = item1.BossMapSummon[i];
                            break;
                        }
                    }
                    if (mapid < 0)
                    {
                        continue;
                    }
                    Map.GopetMap gopetMap = MapManager.maps[mapid];
                    GopetPlace gopetPlace = (GopetPlace)Utilities.RandomArray(gopetMap.places);
                    var queryList = gopetPlace.newMob.Where(t => t.Value > Utilities.CurrentTimeMillis + (4000));
                    if (queryList.Any())
                    {
                        var list = queryList.First();
                        gopetPlace.newMob.Remove(list.Key, out long v);
                        Boss boss = new Boss(item1.bossId, list.Key);
                        gopetPlace.addNewMob(boss);
                        gopetPlace.sendMob(new Collections.JArrayList<Mob.Mob>(new Mob.Mob[] { boss }));
                        WasSummonBoss.Add(item1);
                        PlayerManager.showBanner((l) => string.Format(l.BannerLanguage[Language.LanguageData.BANNER_SHOW_BOSS_SUMMON], item1.name, gopetPlace.map.mapTemplate.name, gopetPlace.zoneID));
                    }
                    else
                    {
                        var queryNonBattleMob = gopetPlace.mobs.Where(t => !(t is Boss) && t.getPetBattle(null) == null);
                        if (queryNonBattleMob.Any())
                        {
                            var firstMob = queryNonBattleMob.First();
                            gopetPlace.mobs.remove(firstMob);
                            Boss boss = new Boss(item1.bossId, firstMob.getMobLocation());
                            boss.SetId(firstMob.GetId());
                            gopetPlace.mobs.Add(boss);
                            WasSummonBoss.Add(item1);
                            PlayerManager.showBanner((l) => string.Format(l.BannerLanguage[Language.LanguageData.BANNER_SHOW_BOSS_SUMMON], item1.name, gopetPlace.map.mapTemplate.name, gopetPlace.zoneID));
                        }
                    }
                }
            }
            foreach (var item1 in BossTimer.Where(t => t.Key != DateTime.Now.Hour))
            {
                foreach (var item2 in item1.Value.Where(x => !BossTimer.Where(t => t.Key == DateTime.Now.Hour).Any(m => m.Value.Contains(x))))
                {
                    WasSummonBoss.Remove(item2);
                }
            }
        }

        public override bool NeedRemove { get => false; }
    }
}
