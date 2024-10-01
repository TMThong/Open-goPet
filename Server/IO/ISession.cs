using Gopet.IO;
using System.Net.Sockets;

namespace Gopet.IO
{
    public interface ISession
    {
        bool clientOK { get; set; }

        Socket CSocket { get; set; }

        void Close();
        bool isConnected();
        void readKey();
        void sendMessage(Message message);
        void setClientOK(bool ok);
        void setHandler(IHandleMessage messageHandler);
    }
}