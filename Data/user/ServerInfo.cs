using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.user
{
    public class ServerInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string IpAddress { get; set; }

        public int Port { get; set; }

        public bool NeedAdmin { get; set; }
        public Version GreaterThanEquals { get; set; }
        public Version LessThan { get; set; }
    }
}
