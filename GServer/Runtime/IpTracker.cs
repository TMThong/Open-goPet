using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Runtime
{
    public class IpTracker : IRuntime
    {
        public void Update()
        {
            PlayerManager.Ipv4Tracker.CleanOldTrack();
        }
    }
}
