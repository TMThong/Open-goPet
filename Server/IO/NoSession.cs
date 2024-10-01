using Gopet.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Server.IO
{
    internal class NoSession : ISession
    {
        public bool clientOK { get; set; } = true;
        public Socket CSocket { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Close()
        {

        }

        public void Exit(object state)
        {

        }

        public bool isConnected()
        {
            return false;
        }

        public void readKey()
        {

        }

        public void run()
        {

        }

        public void sendMessage(Message message)
        {

        }

        public void setClientOK(bool ok)
        {

        }

        public void setHandler(IHandleMessage messageHandler)
        {

        }

        public void setReader(MsgReader r)
        {

        }

        public void setSender(MsgSender s)
        {

        }
    }
}
