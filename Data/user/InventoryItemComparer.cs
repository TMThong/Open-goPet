using Gopet.Data.GopetItem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    internal class InventoryItemComparer : IComparer<Item>
    {
        public int Compare(Item? obj1, Item? obj2)
        {
            return obj1.itemId - obj2.itemId;
        }
    }
}
