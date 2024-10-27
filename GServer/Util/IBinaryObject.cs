using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Util
{
    public interface IBinaryObject<T> 
    {
        int GetId();

        void SetId(int id);


        T Instance { get; }
    }
}
