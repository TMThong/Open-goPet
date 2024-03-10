

using Gopet.Data.Collections;
using System.Net.Sockets;
using Gopet.Util;
namespace Gopet.MServer
{
    public class Server
    {

        private TcpListener serverSc;
        public bool isRunning { get; private set; } = false;
        public CopyOnWriteArrayList<Session> sessions { get; } = new();
        public Server(int port)
        {
            serverSc = new TcpListener(port);
        }

        public void start()
        {
            if (!isRunning)
            {
                isRunning = true;
                serverSc.Start();
                for (global::System.Int32 i = 0; i < 20; i++)
                {
                    serverSc.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), serverSc);
                }
            }
        }


        void AcceptCallback(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            try
            {
                if (!listener.Server.IsBound) return;
                TcpClient client = listener.EndAcceptTcpClient(ar);
                Session session = new Session(client.Client);
                session.setHandler(new Player(session));
                session.run();
                sessions.add(session);
                Session.socketCount++;
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
            listener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), listener);
        }


        public void stopServer()
        {
            isRunning = false;
            serverSc.Stop();
        }
    }
}