using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.CommandLine
{
    internal class AdminCommand : BaseCommand
    {
        public override string Description
        {
            get
            {
                return @"Công cụ quản lý cho người quản trị máy chủ
                         <admin> <banner> <text> để chat thế giới";
            }
        }

        public override string CommandName => "admin";

        public override void Execute(params string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "banner":
                        PlayerManager.showBanner(args[1]);
                        GopetManager.ServerMonitor.LogWarning("Thao tác thành công");
                        break;
                }
            }
        }
    }
}
