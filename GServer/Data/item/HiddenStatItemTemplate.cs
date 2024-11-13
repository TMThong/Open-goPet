using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.item
{
    public class HiddenStatItemTemplate
    {
        public int Id { get; set; }

        public int? IdWeapon { get; set; }

        public int? IdArmour { get; set; }

        public int? IdHat { get; set; }
        public int? IdShoe { get; set; }
        public int? IdGlove { get; set; }

        public ItemInfo[] Data { get; set; } = new ItemInfo[0];

        public string Comment { get; set; } = string.Empty;
    }
}
