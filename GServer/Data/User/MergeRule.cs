using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    public class MergeRule
    {
        public int PetId { get; set; } = -1;
        public int ItemId { get; set; } = -1;

        public int Count { get; set; } = 1;

        public int ItemType { get; set; } = -1;
    }
}
