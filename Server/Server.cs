

using Gopet.Data.Collections;
using System.Net.Sockets;
using Gopet.Util;
public class Server : IDisposable
{

    private TcpListener serverSc;
    public bool isRunning { get; private set; } = false;
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

        Console.WriteLine("Start Server " + this.serverSc.LocalEndpoint.ToString());
        ServerThread.Start();
    }

    public void run()
    {
        if (!isRunning)
        {
            isRunning = true;
            serverSc.Start();
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
                }
            }
            isRunning = false;
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
