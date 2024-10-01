using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Server.IO
{
    public interface IServerBase
    {

        public bool IsRunning { get; protected set; }
        public void StartServer();

        public void StopServer();
    }
}
