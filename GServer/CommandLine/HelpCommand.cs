using Gopet.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.CommandLine
{
    internal class HelpCommand : BaseCommand
    {

        public override string Description => "<help> <cmdname> để hiện hướng dẫn lệnh đó\r\n <help> thì sẽ hiện tất cả tên lệnh";

        public override string CommandName => "help";

        public override void Execute(params string[] args)
        {
            if (args.Length <= 0)
            {
                foreach (var arg in CommandManager.baseCommands)
                {
                    GopetManager.ServerMonitor.LogInfo("Lệnh: " + arg.CommandName);
                    GopetManager.ServerMonitor.LogInfo(arg.Description);
                    GopetManager.ServerMonitor.LogInfo("");
                }
            }
            else
            {
                foreach (var arg in CommandManager.baseCommands.Where(p=> p.CommandName.Contains(args[0])))
                {
                    GopetManager.ServerMonitor.LogInfo("Lệnh: " + arg.CommandName);
                    GopetManager.ServerMonitor.LogInfo(arg.Description);
                    GopetManager.ServerMonitor.LogInfo("");
                }
            }
        }
    }
}
