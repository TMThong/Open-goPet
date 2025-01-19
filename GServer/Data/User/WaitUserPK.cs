using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{

    public class WaitUserPK
    {
        public DateTime TimeInit { get; } = DateTime.Now;

        public GopetPlace Place { get; }

        public Player Active { get; }

        public Player Passive { get; }

        public WaitUserPK(GopetPlace place, Player active, Player passive)
        {
            Place = place;
            Active = active;
            Passive = passive;
        }
    }
}
