
using Dapper;
using Gopet.APIs;
using Gopet.Data.Collections;
using Gopet.Data.Event;
using Gopet.Data.GopetClan;
using Gopet.Data.GopetItem;
using Gopet.Manager;
using Gopet.Runtime;
using Gopet.Server;
using Gopet.Server.IO;
using Gopet.Shared.Helper;
using Gopet.Util;
using Newtonsoft.Json;
namespace Gopet.App
{
    public class Main
    {

        public static IServerBase server;
        public static int PORT_SERVER = ServerSetting.instance.portGopetServer;
        public static bool isNetBeans = true;
        public static int HTTP_PORT = ServerSetting.instance.portHttpServer;
        public static HttpServer APIServer;

        /**
         * hàm chính
         *
         * @param args
         * @ 
         */
        public static void StartServer(string[] args)
        {
            Thread.CurrentThread.Name = "MAIN THREAD (GOPET)";
            if (ServerSetting.instance.initLog)
            {
                initLog();
            }
            //        AutoMaintenance autoMaintenance = new AutoMaintenance();
            //        autoMaintenance.start(ServerSetting.instance.getHourMaintenance(), ServerSetting.instance.getMinMaintenance());
            GopetManager.init();
            HistoryManager.Instance.start();
            MapManager.init();
            ClanManager.init();
            GopetManager.loadMarket();
            BXHManager.instance.start();
            FieldManager.Init();
            initRuntime();
            RuntimeServer.instance.start();
            DailyBossEvent.Instance = new DailyBossEvent();
            //EventManager.AddEvent(DailyBossEvent.Instance);
            EventManager.Start();
            ScheduleManager.Instance.Start();
            PlayerManager.Instance.Start();
            APIServer = new HttpServer(HTTP_PORT);
            APIServer.Start();
            server = new Gopet.MServer.Server(PORT_SERVER);
            async void ScanData()
            {
                int[] keys = new int[] {
                    132,
                    133,
                    134,
                    135,
                    141,
                    154,
                    131

            };
                int[] itemError = new int[] {
                   832,
                   837,
                   840,
                   843
                };
                var tattoIds = GopetManager.tattos.Where(x => keys.Any(t => GopetManager.itemTemplate[t].itemOptionValue[0] == x.Key)).Select(m => m.Key);
                 
                using (var conn = MYSQLManager.create())
                {
                    var players = conn.Query<PlayerData>("SELECT * FROM `player`").Where(x => !x.isAdmin);
                    foreach (var item in players)
                    {

                        void ScanItem()
                        {
                            foreach (var item1 in item.items.Select(x => x.Value))
                            {
                                foreach (var item2 in item1.Where(x => x.count > 200 && x.itemTemplateId != 181 && x.itemTemplateId != 186 && x.itemTemplateId != 187))
                                {
                                    GopetManager.ServerMonitor.LogError($"Item {item2.Template.name} có sll {item2.count} của nhân vật {item.name}");
                                }
                            }
                        }

                        void ScanItem2()
                        {
                            foreach (var item1 in item.items.Select(x => x.Value))
                            {
                                foreach (var item2 in item1.Where(x => x.SourcesItem.Contains(Data.item.ItemSource.MUA_ĐỒ_SHOP_NPC)))
                                {
                                    GopetManager.ServerMonitor.LogError($"Item {item2.Template.name} có sll {item2.count} của nhân vật {item.name}");
                                }
                            }
                        }

                        void ScanWings()
                        {
                            foreach (var item1 in item.items.Select(x => x.Value))
                            {
                                foreach (var item2 in item1.Where(x => x.itemTemplateId >= 154111 && x.itemTemplateId <= 154131).GroupBy(m => m.itemTemplateId))
                                {
                                    GopetManager.ServerMonitor.LogError($"Item {item2.First().Template.name} có sll {item2.Count()} của nhân vật {item.name}");
                                }
                            }
                            if (item.wing != null && item.wing.itemTemplateId >= 154111 && item.wing.itemTemplateId <= 154131)
                            {
                                GopetManager.ServerMonitor.LogError($"Item {item.wing.Template.name} có sll {1} của nhân vật {item.name}");
                            }
                        }
                        void ScanPet()
                        {
                            foreach (var item1 in item.pets.Concat(new Pet[] { item.petSelected }))
                            {
                                if (item1 != null)
                                {
                                    int numTatto = item1.tatto.Where(x => tattoIds.Contains(x.tattooTemplateId)).Count();
                                    if (numTatto > 1)
                                    {
                                        GopetManager.ServerMonitor.LogError($"Pet {item1.name} có {numTatto} xăm {JsonConvert.SerializeObject(item1.tatto.Where(x => tattoIds.Contains(x.tattooTemplateId)).Select(m => m.Template.name))} liên quan thẻ xăm của nhân vật {item.name}");
                                    }
                                }
                            }
                        }

                        void ScanPet2()
                        {
                            foreach (var item1 in item.pets.Concat(new Pet[] { item.petSelected }))
                            {
                                if (item1 != null && item1.petIdTemplate == 3090)
                                {
                                    GopetManager.ServerMonitor.LogError($"Pet {item1.name} của nhân vật {item.name}");
                                }
                            }
                        }
                        void BuffHKL()
                        {
                            if (!item.pets.Concat(new Pet[] { item.petSelected }).Any(x => x != null && x.petIdTemplate == 89))
                            {
                                Pet pet = new Pet(89);
                                pet.Expire = DateTime.Now.AddDays(3);
                                item.addPet(pet, null);
                                item.save();
                            }
                        }

                        void FixDuplicatePet()
                        {
                            var petsDic = item.pets.DistinctBy(x => x.TimeDieZ).ToList();
                            item.pets = new CopyOnWriteArrayList<Pet>(petsDic);
                            item.save();
                        }


                        void ScanBuffItem()
                        {
                            foreach (var item1 in item.items.SelectMany(x => x.Value.Where(k => k.TimeCreate > new DateTime(2024, 12, 9) && k.SourcesItem.Contains(Data.item.ItemSource.BUFF_BẨN)).Select(m => m)))
                            {
                                GopetManager.ServerMonitor.LogWarning($"{item.name} có {item1.Template.name} x{item1.count} được buff");
                            }
                        }
                        void ScanItemById(string UUID)
                        {
                            foreach (var item1 in item.items.SelectMany(x => x.Value.Where(k => k.itemUID == UUID).Select(m => m)))
                            {
                                GopetManager.ServerMonitor.LogWarning($"{item.name} có {item1.Template.name}");
                            }
                        }

                        void ScanItemId()
                        {
                            foreach (var item1 in item.items.Select(x => x.Value))
                            {
                                foreach (var item2 in item1.Where(x => itemError.Contains(x.itemTemplateId)))
                                {
                                    GopetManager.ServerMonitor.LogError($"Item {item2.Template.name} có sll {item2.count} của nhân vật {item.name}");
                                    item2.itemTemplateId = GopetManager.ID_ITEM_PART_WING_TIER_1[0];
                                }
                            }
                            item.save();
                        }

                        //ScanItem();
                        //ScanPet();
                        //ScanWings();
                        //BuffHKL();
                        //ScanPet2();
                        //ScanBuffItem();
                        //ScanItemById("bc8fb3e0-bc56-4b72-8c33-ad10a904b576");
                        ScanItemId();
                    }
                    void ScanLetter()
                    {
                        foreach (var item1 in players.SelectMany(x => x.letters).GroupBy(x => x.ShortContent))
                        {
                            Console.WriteLine(item1.Key);
                        }
                    }

                }
            }
            //ScanData();
            server.StartServer();
        }

        public static void initRuntime()
        {
            RuntimeServer.instance.runtimes.add(new AutoSave());
            RuntimeServer.instance.runtimes.add(new DBBackup());
            RuntimeServer.instance.runtimes.add(Maintenance.gI());
        }


        public static void initLog()
        {

        }

        public static void shutdown()
        {
            MapManager.stopUpdate();
            GopetManager.saveMarket();
            server.StopServer();
            APIServer.Stop();
            RuntimeServer.isRunning = false;
            Thread.Sleep(1000);

            foreach (Player player in PlayerManager.players)
            {
                player.session.Close();
                player.onDisconnected();
                PlayerManager.players.remove(player);
            }

            foreach (Clan clan in ClanManager.clans)
            {
                try
                {
                    clan.save();
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
        }
    }

}