

using Gopet.Data.Collections;
using System.Net.Sockets;
using Gopet.Util;
using Gopet.IO;
using Gopet.Server.IO;
using System.Net;
using System.Diagnostics;
using System;
using System.Collections.Concurrent;
namespace Gopet.MServer
{
    public class Server : IServerBase
    {
        private TcpListener _listener;

        public CopyOnWriteArrayList<Session> sessions { get; } = new();
        public bool IsRunning { get; set; } = false;

        private SocketAsyncEventArgs _event = new SocketAsyncEventArgs();

        private ConcurrentDictionary<string, DateTime> ConnectionWait = new();


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
                    TcpClient client = _listener.AcceptTcpClient();
                    string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    if (ConnectionWait.TryGetValue(clientIP, out var dateTime) && dateTime > DateTime.Now)
                    {
                        client.Close();
                        continue;
                    }
                    else ConnectionWait[clientIP] = DateTime.Now.AddSeconds(2);
                    this.CleanupConnectionWait();
                    ThreadPool.QueueUserWorkItem(setupClient, client);
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
        }
        private void setupClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            Session session = new Session(client.Client);
            session.setHandler(new Player(session));
            session.run();
            sessions.Add(session);
            Session.socketCount++;
        }

        private void CleanupConnectionWait()
        {
            foreach (var key in ConnectionWait.Keys)
            {
                if (ConnectionWait[key] <= DateTime.Now)
                {
                    ConnectionWait.TryRemove(key, out _);
                }
            }
        }

        public void StopServer()
        {
            IsRunning = false;
        }
    }
}