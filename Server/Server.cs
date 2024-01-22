

using Gopet.Data.Collections;
using System.Net.Sockets;

public class Server  {

    private  TcpListener serverSc;
    public bool isRunning = false;
    public CopyOnWriteArrayList<Session> sessions { get; } = new();

    public Server(int port)   {
        serverSc = new TcpListener(port);
    }
    
   
    public void run() {
        if (!isRunning) {
            isRunning = true;
             
            while (isRunning && !serverSc.Server.IsBound) {
                try {
                    Socket socket = serverSc.AcceptSocket();
                    Session session = new Session(socket);
                    session.setHandler(new Player(session));
                    session.start();
                    sessions.add(session);
                    Session.socketCount++;
                } catch (Exception e) {
                    e.printStackTrace();
                    break;
                }
            }
        }
    }

    public void stopServer()   {
        isRunning = false;
        serverSc.close();
    }
}
