using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    public class MergeData
    {
        public CopyOnWriteArrayList<Item> Items = new CopyOnWriteArrayList<Item>();

        public CopyOnWriteArrayList<Pet> pets = new CopyOnWriteArrayList<Pet>();
    }
}
