using Gopet.App;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.CommandLine
{
    internal class ShutdownCommand : BaseCommand
    {
        public override string Description => "<shutdown> đóng máy chủ";

        public override string CommandName => "shutdown";

        public override void Execute(params string[] args)
        {
            GopetManager.ServerMonitor.LogWarning("Chuẩn bị đóng máy chủ, đang lưu lại thông tin");
            Main.shutdown();
            GopetManager.ServerMonitor.LogWarning("Lưu lại thông tin thành công");
            Process.GetCurrentProcess().Kill();
        }
    }
}
