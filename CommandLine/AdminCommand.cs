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
                return "Công cụ quản lý cho người quản trị máy chủ"
                        + "\n<admin> <banner> <text> để chat thế giới" +
                        "\n<admin> <location> <playerName> để lấy vị trí cụ thể";
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
                        //PlayerManager.showBanner(args[1]);
                        GopetManager.ServerMonitor.LogWarning("Thao tác thành công");
                        break;
                    case "location":
                        Player p = PlayerManager.get(args[1]);
                        if (p != null)
                        {
                            GopetManager.ServerMonitor.LogWarning($"Map: {p.getPlace().map.mapTemplate.name} Zone {p.getPlace().zoneID} [{p.playerData.x},{p.playerData.y}]");
#if DEBUG
                            GopetManager.ServerMonitor.LogWarning($"INSERT INTO `gopet_mob_location` (`mapID`, `x`, `y`) VALUES ('{p.getPlace().map.mapID}', '{p.playerData.x}', '{p.playerData.y}');");
#endif
                        }
                        else
                        {
                            GopetManager.ServerMonitor.LogError("Người chơi đã offline");
                        }
                        break;
                }
            }
        }
    }
}
