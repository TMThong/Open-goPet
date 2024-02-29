using Gopet.Data.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.map
{
    public class ArenaMap : GopetMap
    {
        public ArenaMap(int mapId_, bool canUpdate, MapTemplate mapTemplate) : base(mapId_, canUpdate, mapTemplate)
        {

        }

        public override void addRandom(Player player)
        {
             
        }

        public override void update()
        {
            base.update();
        }

        public override bool CanChangeZone  => false;
    }
}
