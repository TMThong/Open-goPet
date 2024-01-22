package server;

import java.net.ServerSocket;
import java.net.Socket;
import java.util.concurrent.CopyOnWriteArrayList;
import server.io.Session;

public class Server extends Thread {

    private final ServerSocket serverSc;
    public bool isRunning = false;
    public final CopyOnWriteArrayList<Session> sessions = new CopyOnWriteArrayList();

    public Server(int port)   {
        serverSc = new ServerSocket(port);
        serverSc.setReuseAddress(true);
        this.setPriority(MAX_PRIORITY);
    }
    
    @Override
    public void run() {
        if (!isRunning) {
            isRunning = true;
            System.out.println("Server started...");
            while (isRunning && !serverSc.isClosed()) {
                try {
                    Socket socket = serverSc.accept();
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
