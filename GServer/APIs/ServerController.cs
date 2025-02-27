using Gopet.App;
using Gopet.Data.Event;
using Gopet.Data.GopetItem;
using Gopet.Data.item;
using Gopet.Data.Mob;
using Gopet.IO;
using Gopet.Manager;
using Gopet.Shared.Helper;
using Gopet.Util;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gopet.APIs.GopetApiExtentsion;
namespace Gopet.APIs
{

    [Route("api/server")]
    [ApiController]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class ServerController : ControllerBase
    {
        /// <summary>
        /// Tắt máy chủ
        /// </summary>
        /// <returns>
        /// Trả về thành công thì server sẽ tắt sau 2 giây
        /// </returns>
        [HttpGet("shutdown")]
        public IActionResult ShutDown()
        {
            Thread Shutdown = new Thread(() =>
            {
                Thread.Sleep(2000);
                CommandManager.baseCommands.Where(c => c.CommandName == "shutdown").First().Execute();
            });
            Shutdown.IsBackground = true;
            Shutdown.Start();

            return Ok(OK_Repository);
        }

        [HttpGet("checksocket")]
        public IActionResult checksocket()
        {
            return Ok(GopetApiExtentsion.CreateOKRepository(Main.server.IsRunning));
        }
        [HttpGet("opensqlweb")]
        public IActionResult opensqlweb()
        {
            using (var conn = MYSQLManager.createWebMySqlConnection())
            {
                return Ok(GopetApiExtentsion.CreateOKRepository(Main.server.IsRunning));
            }
        }
        [HttpGet("opensqlgame")]
        public IActionResult opensqlgame()
        {
            using (var conn = MYSQLManager.create())
            {
                return Ok(GopetApiExtentsion.CreateOKRepository(Main.server.IsRunning));
            }
        }
        [HttpGet("ReleaseLock")]
        public IActionResult ReleaseLock()
        {
            foreach (var sender in MsgSender.msgSenders)
            {
                sender.Release();
            }
            return Ok(GopetApiExtentsion.CreateOKRepository(Main.server.IsRunning));
        }
        [HttpGet("gc")]
        public IActionResult gc()
        {
            System.GC.Collect();
            return Ok(GopetApiExtentsion.CreateOKRepository(Main.server.IsRunning));
        }
        [HttpGet("socketCount")]
        public IActionResult socketCount()
        {
            return Ok(GopetApiExtentsion.CreateOKRepository(Session.socketCount));
        }
        [HttpGet("Server.Exp.Percent")]
        public IActionResult ServerExpPercent()
        {
            return Ok(GopetApiExtentsion.CreateOKRepository(FieldManager.PERCENT_EXP));
        }
        [HttpGet("Server.GEM.Percent")]
        public IActionResult ServerGEMPercent()
        {
            return Ok(GopetApiExtentsion.CreateOKRepository(FieldManager.PERCENT_GEM));
        }
        [HttpGet("RefreshField")]
        public IActionResult RefreshField()
        {
            FieldManager.Init();
            return Ok(GopetApiExtentsion.CreateOKRepository(JsonConvert.SerializeObject(FieldManager.Fields, Formatting.Indented)));
        }

        [HttpGet("Threads")]
        public IActionResult Threads()
        {
            ProcessThreadCollection threads = Process.GetCurrentProcess().Threads;

            Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
            foreach (Thread item in ThreadManager.THREADS)
            {
                ProcessThread p = null;
                foreach (ProcessThread item1 in threads)
                {
                    if (item1.Id == item.ManagedThreadId)
                    {
                        p = item1;
                        break;
                    }
                }


                map[item.Name] = new
                {
                    IsAlive = item.IsAlive,
                    Id = item.ManagedThreadId,
                    Data = new
                    {
                        CPUTime = p?.TotalProcessorTime,
                        UserProcessorTime = p?.UserProcessorTime
                    },
                };

            }

            Dictionary<int, dynamic> keyValuePairs = new Dictionary<int, dynamic>();

            foreach (ProcessThread p in threads)
            {

                keyValuePairs[p.Id] = new
                {
                    CPUTime = p?.TotalProcessorTime,
                    StartTime = p?.StartTime,
                    UserProcessorTime = p?.UserProcessorTime
                };

            }
            return Ok(GopetApiExtentsion.CreateOKRepository(new { ThreadMa = map, ThProcess = keyValuePairs }));
        }


        [HttpGet("/api/maintenance/{min}")]
        public IActionResult maintenanceStart(int min)
        {
            Maintenance.gI().setMaintenanceTime(min);
            return Ok(GopetApiExtentsion.CreateOKRepository($" {min} phút nữa sẽ bảo trì"));
        }


        [HttpGet("/api/maintenance/reboot")]
        public IActionResult reboot()
        {
            Maintenance.gI().reboot();
            return Ok(GopetApiExtentsion.CreateOKRepository($"Thành công"));
        }

        [HttpGet("/api/dialog/okDialog/{text}")]
        public IActionResult okDialog(string text)
        {
            PlayerManager.okDialog(text);
            return Ok(GopetApiExtentsion.CreateOKRepository($"Thành công"));
        }

        [HttpGet("/api/BannerZ/{text}")]
        public IActionResult BannerZ(string text)
        {
            PlayerManager.showBannerZ(text);
            return Ok(GopetApiExtentsion.CreateOKRepository($"Thành công"));
        }

        [HttpGet("/api/test/set_boss/{mapid}/{place}/{bossId}")]
        public IActionResult TestBoss(int mapid, int place, int bossId)
        {
            GopetPlace gopetPlace = (GopetPlace)MapManager.maps[mapid].places[place];
            for (int i = 0; i < 1; i++)
            {
                Mob mob = gopetPlace.mobs[i];
                Boss boss = new Boss(bossId, mob.getMobLocation());
                boss.TimeOut = DateTime.Now.AddMinutes(15);
                gopetPlace.mobs[i] = boss;
                gopetPlace.mobs[i].SetId(mob.GetId());
            }
            return Ok(GopetApiExtentsion.CreateOKRepository($"Thành công"));
        }

        [HttpGet("/api/test/TestBossDaily")]
        public IActionResult TestBossDaily()
        {
            EventManager.AddEvent(DailyBossEvent.Instance);
            return Ok(GopetApiExtentsion.CreateOKRepository($"Thành công"));
        }

        [HttpGet("/api/addAutoBanner/{Text}/{secondTimeSpan}/{Min}/{Hours}/{Day}/{Month}")]
        public IActionResult addAutoBanner(string Text, int secondTimeSpan, int Min, int Hours, int Day, int Month)
        {
            BannerEvent.Instance.Banners.Add(new Tuple<string, TimeSpan, DateTime>(Text, TimeSpan.FromSeconds(secondTimeSpan), new DateTime(DateTime.Now.Year, Month, Day, Hours, Min, 0)));
            return Ok(GopetApiExtentsion.CreateOKRepository($"Thành công"));
        }

        public static void BuffItem(string name, int itemId, int LevelUpTier, int MaxTier, int count, bool MaxOption, int EndLevel, byte NumFusion = 0)
        {
            if (PlayerManager.player_name.TryGetValue(name, out var p))
            {
                for (int i = 0; i < count; i++)
                {
                    Item item = new Item(itemId, 1, MaxOption);
                    item.SourcesItem.Add(ItemSource.BUFF_BẨN);
                    if (MaxOption)
                    {
                        item.atk = Item.GetMaxOption(item.Template.atkRange);
                        item.def = Item.GetMaxOption(item.Template.defRange);
                        item.hp = Item.GetMaxOption(item.Template.hpRange);
                        item.mp = Item.GetMaxOption(item.Template.mpRange);
                    }
                    for (global::System.Int32 j = 0; j <= LevelUpTier; j++)
                    {
                        item.AddEnchantInfo();
                        item.lvl = j;
                        GopetManager.ServerMonitor.LogInfo($"BuffItem TIER {j} {Utilities.FormatNumber(Utilities.GetValueFromPercentL(item.getHp() + item.hp, GopetManager.PERCENT_ITEM_TIER_INFO))} HP");
                    }
                    item.NumFusion = NumFusion;
                    for (global::System.Int32 j = 0; j < MaxTier; j++)
                    {
#if DEBUG_LOG
                        GopetManager.ServerMonitor.LogInfo($"BuffItem TIER {j} {Utilities.FormatNumber(Utilities.GetValueFromPercentL(item.getHp() + item.hp, GopetManager.PERCENT_ITEM_TIER_INFO))} HP");
                        GopetManager.ServerMonitor.LogInfo($"BuffItem TIER {j} {Utilities.FormatNumber(Utilities.GetValueFromPercentL(item.getMp() + item.mp, GopetManager.PERCENT_ITEM_TIER_INFO))} MP");
                        GopetManager.ServerMonitor.LogInfo($"BuffItem TIER {j} {Utilities.FormatNumber(Utilities.GetValueFromPercentL(item.getAtk() + item.atk, GopetManager.PERCENT_ITEM_TIER_INFO))} ATK");
                        GopetManager.ServerMonitor.LogInfo($"BuffItem TIER {j} {Utilities.FormatNumber(Utilities.GetValueFromPercentL(item.getDef() + item.def, GopetManager.PERCENT_ITEM_TIER_INFO))} DEF");
#endif
                        item.hp = Utilities.round(Utilities.GetValueFromPercent(item.getHp() + item.hp, GopetManager.PERCENT_ITEM_TIER_INFO));
                        item.mp = (Utilities.round(Utilities.GetValueFromPercent(item.getMp() + item.mp, GopetManager.PERCENT_ITEM_TIER_INFO)));
                        item.atk = (Utilities.round(Utilities.GetValueFromPercent(item.getAtk() + item.atk, GopetManager.PERCENT_ITEM_TIER_INFO)));
                        item.def = (Utilities.round(Utilities.GetValueFromPercent(item.getDef() + item.def, GopetManager.PERCENT_ITEM_TIER_INFO)));
                        item.NumFusion = 0;
                        for (global::System.Int32 t = 0; t <= LevelUpTier; t++)
                        {
                            item.AddEnchantInfo();
                            item.lvl = t;
                        }

                        if (GopetManager.tierItem.TryGetValue(item.itemTemplateId, out var tierItem))
                        {
                            item.itemTemplateId = tierItem.itemTemplateIdTier2;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    for (global::System.Int32 j = 0; j <= EndLevel; j++)
                    {
                        item.AddEnchantInfo();
                        item.lvl = j;
                    }
                    p.addItemToInventory(item);
                    p.Popup($"Bạn được buff bẩn {item.getName(p)}");
                }
            }
        }

        [HttpGet("/api/BuffItem/{name}/{itemId}/{LevelUpTier}/{MaxTier}/{count}/{MaxOption}/{EndLevel}/{NumFusion}/{EndFusion}")]
        public IActionResult ApiBuffItem(string name, int itemId, int LevelUpTier, int MaxTier, int count, bool MaxOption, int EndLevel, byte NumFusion)
        {
            BuffItem(name, itemId, LevelUpTier, MaxTier, count, MaxOption, EndLevel, NumFusion);
            return Ok(GopetApiExtentsion.CreateOKRepository($"Thành công"));
        }
        [HttpGet("/api/ThreadCount")]
        public int ThreadCount()
        {
            return ThreadPool.ThreadCount;
        }

        [HttpGet("/api/PendingWorkItemCount")]
        public long PendingWorkItemCount()
        {
            return ThreadPool.PendingWorkItemCount;
        }

        [HttpGet("/api/CompletedWorkItemCount")]
        public long CompletedWorkItemCount()
        {
            return ThreadPool.CompletedWorkItemCount;
        }
        [HttpGet("/api/Ipv4Tracker")]
        public IActionResult Ipv4Tracker()
        {
            return Ok(GopetApiExtentsion.CreateOKRepository(PlayerManager.Ipv4Tracker.Tracks.Select(x => new object[] { x.Key, x.Value.Count })));
        }
        [HttpPost("/api/SendMail/{to}/{subject}/{body}/{type}")]
        public IActionResult SendMail(string to, string subject, string body, string type = "html")
        {
            GopetManager.EmailService.SendEmail(to, subject, GopetManager.EmailContent.Replace("{0}", body), type);
            return Ok(GopetApiExtentsion.CreateOKRepository($"Thành công"));
        }

        [HttpGet("/api/Hash/{text}")]
        public IActionResult Hash(string text)
        {
            return Ok(GopetHashHelper.ComputeHash(text));
        }

        [HttpGet("/api/NumPlayerOnline")]
        public IActionResult NumPlayerOnline()
        {
            return Ok(PlayerManager.players.Count);
        }


        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
