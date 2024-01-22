

using System.Net.Sockets;

public class Session {

    public static ThreadPoolExecutor executorCloseThread = (ThreadPoolExecutor) Executors.newFixedThreadPool(20);
    public const ThreadGroup THREAD_GROUP = new ThreadGroup("session");
    public IHandleMessage messageHandler;
    public DataOutputStream dos;
    public DataInputStream dis;
    public Socket sc;
    public bool isConnected;
    private MsgSender sender;
    private MsgReader reader;
    public int sendsbyteCount;
    public int recvsbyteCount;
    public TEA tea;
    public String currentIp;
    public int currentPort;
    public bool clientOK = false;
    public long msgCount = 0;

    public Session(Socket socket) {
        sc = socket;
    }

    public void setClientOK(bool ok)   {
        Message ms = new Message((sbyte) -36);
        ms.writer().writesbyte(ok ? 1 : 0);
        ms.writer().flush();
        sendMessage(ms);
        clientOK = true;
    }

    public bool isConnected() {
        return this.isConnected;
    }

    
    public void run() {
        try {
            isConnected = true;
            dis = new DataInputStream(sc.getInputStream());
            dos = new DataOutputStream(sc.getOutputStream());
            readKey();
            setSender(new MsgSender(this));
            setReader(new MsgReader(this));
            Thread sendThread = new Thread(THREAD_GROUP, sender);
            sendThread.setName("Send Msg thread");
            sendThread.start();
            Thread readThread = new Thread(THREAD_GROUP, reader);
            readThread.setName("Read Msg thread");
            readThread.start();
        } catch (Exception e) {
            close();
        }
    }

    public void readKey()   {
        sbyte[] keys = new sbyte[9];
        dis.read(keys, 0, 9);
        long key = readKey(keys);
        tea = new TEA(key);
    }

    private long readKey(sbyte[] var10000) {
        long time = 0;
        time ^= var10000[1] & 255;
        time <<= 8;
        time ^= var10000[2] & 255;
        time <<= 8;
        time ^= var10000[3] & 255;
        time <<= 8;
        time ^= var10000[4] & 255;
        time <<= 8;
        time ^= var10000[5] & 255;
        time <<= 8;
        time ^= var10000[6] & 255;
        time <<= 8;
        time ^= var10000[7] & 255;
        time <<= 8;
        time ^= var10000[8] & 255;
        return time;
    }

    public void setHandler(IHandleMessage messageHandler) {
        this.messageHandler = messageHandler;
    }

    public void setSender(MsgSender s) {
        this.sender = s;
    }

    public void setReader(MsgReader r) {
        this.reader = r;
    }

    protected void setKey(long key) {
        this.tea = new TEA(key);
    }

    public void sendMessage(Message message) {
        this.sender.addMessage(message);
    }

    public static int socketCount = 0;

    public void close() {
        executorCloseThread.execute(new CloseSessionTask());
    }

    class CloseSessionTask implements Runnable {

        @Override
        public void run() {
            try {
                currentIp = null;
                currentPort = -1;
                isConnected = false;
                if (sender != null) {
                    sender.stop();
                }

                if (dos != null) {
                    dos.close();
                    dos = null;
                }

                if (dis != null) {
                    dis.close();
                    dis = null;
                }

                if (sc != null) {
                    sc.close();
                    sc = null;
                    socketCount--;
                }

                sendsbyteCount = 0;
                recvsbyteCount = 0;

                if (messageHandler != null) {
                    messageHandler.onDisconnected();
                }

            } catch (Exception var2) {

            }
        }
    }
}
