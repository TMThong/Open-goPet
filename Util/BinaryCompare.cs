using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Util
{
    internal sealed class BinaryCompare<T> : IComparer<IBinaryObject<T>>
    {
        public int Compare(IBinaryObject<T>? obj1, IBinaryObject<T>? obj2)
        {
            return obj1.GetId() - obj2.GetId();
        }
    }
}
