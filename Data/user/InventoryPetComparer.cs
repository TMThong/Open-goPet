using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    internal class InventoryPetComparer : IComparer<Pet>
    {
        public int Compare(Pet? obj1, Pet? obj2)
        {
            return obj1.petId - obj2.petId;
        }
    }
}
