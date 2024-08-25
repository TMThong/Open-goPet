using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.item
{
    public class TradeGiftTemplate
    {
        public int TradeId { get; private set; }
        public int ItemTemplateId { get; private set; }
        public int Count { get; private set; }
        public sbyte Type { get; private set; }
        public float Percent { get; private set; }

        public const sbyte TYPE_COIN = 0;
        public const sbyte TYPE_GOLD = 1;
        public const sbyte TYPE_LUA = 2;
    }
}
