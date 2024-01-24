

using Gopet.Data.Collections;
using System.Net.Sockets;
using Gopet.Util;
public class Server : IDisposable
{

    private TcpListener serverSc;
    public bool isRunning = false;
    public CopyOnWriteArrayList<Session> sessions { get; } = new();
    public Thread ServerThread { get; }
    public Server(int port)
    {
        serverSc = new TcpListener(port);
        ServerThread = new Thread(run);
        ServerThread.IsBackground = true;
        ServerThread.Name = "TCP Gopet";
    }

    public void start()
    {
        ServerThread.Start();
        Console.WriteLine("Start Server " + this.serverSc.LocalEndpoint.ToString());
    }

    public void run()
    {
        if (!isRunning)
        {
            isRunning = true;
            serverSc.Start(100);
            while (isRunning)
            {
                try
                {
                    Socket socket = serverSc.AcceptSocket();
                    Session session = new Session(socket);
                    session.setHandler(new Player(session));
                    session.run();
                    sessions.add(session);
                    Session.socketCount++;
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                    break;
                }
            }
        }
    }

    public void stopServer()
    {
        isRunning = false;
        serverSc.Stop();
    }


    public bool IsDisposed { get; private set; }

    public void Dispose()
    {
        if (!IsDisposed)
        {
            IsDisposed = true;
            stopServer();
        }
    }

    ~Server() => Dispose();
}
