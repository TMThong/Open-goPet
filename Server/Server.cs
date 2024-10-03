

using Gopet.Data.Collections;
using System.Net.Sockets;
using Gopet.Util;
using Gopet.IO;
using Gopet.Server.IO;
using System.Net;
using System.Diagnostics;
using System;
namespace Gopet.MServer
{
    public class Server : IServerBase
    {
        private TcpListener _listener;

        public CopyOnWriteArrayList<Session> sessions { get; } = new();
        public bool IsRunning { get; set; } = false;

        private SocketAsyncEventArgs _event = new SocketAsyncEventArgs();

        public Server(int port)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, port);
            _listener = new TcpListener(iPEndPoint);
        }

        public Thread RunnerThread { get; protected set; }



        public void StartServer()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _listener.Start();
                RunnerThread = new Thread(Runner);
                RunnerThread.Name = "Listener Thread";
                RunnerThread.IsBackground = true;
                RunnerThread.Start();
            }
        }

        private void Runner()
        {
            while (IsRunning)
            {
                try
                {
                    Session session = new Session(_listener.AcceptSocket());
                    session.setHandler(new Player(session));
                    session.run();
                    sessions.Add(session);
                    Session.socketCount++;
                }
                catch (Exception e)
                {

                    e.printStackTrace();
                }
            }
        }

        public void StopServer()
        {
            IsRunning = false;
        }
    }
}