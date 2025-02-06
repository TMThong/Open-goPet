using Gopet.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace goPetHD
{
    public class GameClient
    {
        public readonly static GameClient client = new GameClient();

        public Session session;

        public void doConnect()
        {
            Thread thread = new Thread(new ThreadStart(this.connect));
            thread.IsBackground = true;
            thread.Start();
        }


        private void connect()
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("server1.gopettae.com", 19189);
            if (tcpClient.Connected)
            {
                Debug.Log("Connected to server");
                session = new Session(tcpClient.Client);
                session.run();
            }
            else
            {
                Debug.Log("Failed to connect to server");
                UICamera.ShowOkDialog("Kết nối tới máy chủ thất bại do hoặc do máy chủ đang bảo trì", UICamera.Instance.loginUI);
            }
        }
    }
}