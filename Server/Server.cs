

using Gopet.Data.Collections;
using System.Net.Sockets;
using Gopet.Util;
using Gopet.IO;
using Gopet.Server.IO;
using System.Net;
using System.Diagnostics;
namespace Gopet.MServer
{
    public class Server : IServerBase
    {
        private Socket _socket;

        public CopyOnWriteArrayList<Session> sessions { get; } = new();
        public bool IsRunning { get; set; } = false;

        private SocketAsyncEventArgs _event = new SocketAsyncEventArgs();

        public Server(int port)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, port);
            _socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(iPEndPoint);
            _event.Completed += OnCompleted;
        }

        private void OnCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (!IsRunning) 
                return;
            ProccessAcceptSocket(e);
        }


        public void StartServer()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _socket.Listen();
                StartAccept(_event);
            }
        }

        private void StartAccept(SocketAsyncEventArgs eventArgs)
        {
            eventArgs.AcceptSocket = null;
            if (!_socket.AcceptAsync(eventArgs))
                ProccessAcceptSocket(eventArgs);
        }

        private void ProccessAcceptSocket(SocketAsyncEventArgs eventArgs)
        {
            if (eventArgs.SocketError == SocketError.Success)
            {
                Session session = new Session(eventArgs.AcceptSocket);
                session.setHandler(new Player(session));
                session.run();
                sessions.Add(session);
                Session.socketCount++;
            }
            else
            {
                eventArgs.AcceptSocket?.Close();
            }

            if (IsRunning)
            {
                StartAccept(eventArgs);
            }
        }

        public void StopServer()
        {
            IsRunning = false;
        }
    }
}