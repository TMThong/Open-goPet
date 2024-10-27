using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.GopetClan
{
    internal sealed class ClanRequestJoinComparer : IComparer<ClanRequestJoin>
    {
        public int Compare(ClanRequestJoin? o1, ClanRequestJoin? o2)
        {
            return o1.user_id - o2.user_id;
        }
    }
}
