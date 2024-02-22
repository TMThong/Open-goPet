using Gopet.App;
using Gopet.Data.GopetItem;
using Gopet.IO;
using Gopet.Manager;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        /// Trả về thành công thì server chưa tắt :))
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
            return Ok(GopetApiExtentsion.CreateOKRepository(Main.server.isRunning));
        }
        [HttpGet("opensqlweb")]
        public IActionResult opensqlweb()
        {
            using (var conn = MYSQLManager.createWebMySqlConnection())
            {
                return Ok(GopetApiExtentsion.CreateOKRepository(Main.server.isRunning));
            }
        }
        [HttpGet("opensqlgame")]
        public IActionResult opensqlgame()
        {
            using (var conn = MYSQLManager.create())
            {
                return Ok(GopetApiExtentsion.CreateOKRepository(Main.server.isRunning));
            }
        }
        [HttpGet("ReleaseLock")]
        public IActionResult ReleaseLock()
        {
            foreach (var sender in MsgSender.msgSenders)
            {
                sender.Release();
            }
            return Ok(GopetApiExtentsion.CreateOKRepository(Main.server.isRunning));
        }
        [HttpGet("gc")]
        public IActionResult gc()
        {
            System.GC.Collect();
            return Ok(GopetApiExtentsion.CreateOKRepository(Main.server.isRunning));
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

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
