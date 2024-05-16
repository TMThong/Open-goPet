

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
                for (global::System.Int32 i = 0; i < 40; i++)
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
            try
            {
                listener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), listener);
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
        }


        public void stopServer()
        {
            isRunning = false;
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                serverSc.Stop();
            });
        }
    }
}