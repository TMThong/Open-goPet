using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.CommandLine
{
    internal class ZoneCommand : BaseCommand
    {
        public override string Description
        {
            get
            {
                return @"<zone> <mapId> <zoneId> <summonboss> để triệu hồi boss khu vực
                         <zone> <mapId> <zoneId> <okDialog> <text> để chat hộp thoại màu xanh dương
                        <zone> <mapId> <zoneId> <redDialog> <text> để chat hộp thoại màu đỏ";
            }
        }

        public override string CommandName => "zone";

        public override void Execute(params string[] args)
        {
            if (args.Length <= 1) return;
            int zoneId = int.Parse(args[1]);
            int mapId = int.Parse(args[0]);
            switch (args[2])
            {
                case "summonboss":
                    (MapManager.maps[mapId].places.Where(p => p.zoneID == zoneId).First() as GopetPlace).numMobDie[0] = int.MaxValue / 2;
                    GopetManager.ServerMonitor.LogWarning("Thao tác thành công bạn vui lòng giết 1 con quái để nó xuất hiện");
                    break;
                case "okDialog":
                    (MapManager.maps[mapId].places.Where(p => p.zoneID == zoneId).First() as GopetPlace).players.ToList().ForEach(p => p.okDialog(args[3]));
                    GopetManager.ServerMonitor.LogWarning("Thao tác thành công");
                    break;
                case "redDialog":
                    (MapManager.maps[mapId].places.Where(p => p.zoneID == zoneId).First() as GopetPlace).players.ToList().ForEach(p => p.redDialog(args[3]));
                    GopetManager.ServerMonitor.LogWarning("Thao tác thành công");
                    break; 
                case "set_challenge":
                    var m = (MapManager.maps[mapId].places.Where(p => p.zoneID == zoneId).First() as ChallengePlace);
                    m.isWait = false;
                    m.placeTime = 0;
                    m.mobs.Clear();
                    m.turn = int.Parse(args[3]);
                    m.isWaitForNewTurn = true;
                    GopetManager.ServerMonitor.LogWarning("Thao tác thành công");
                    break;
            }
        }
    }
}
