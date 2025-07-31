package vn.me.network;

import defpackage.Class161;
import defpackage.IMessageListener;
import vn.me.network.TEA;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import javax.microedition.io.Connector;
import javax.microedition.io.SocketConnection;

/* renamed from: Class160  reason: default package */
 /* loaded from: gopet_repackage.jar:Class160.class */
public final class MobileClient {
    public IMessageListener messageHandler;
    public DataOutputStream dos;
    public DataInputStream dis;
    private SocketConnection sc;
    public boolean isConnected;
    private MsgSender sender;
    private MsgReader reader;
    public int sendByteCount;
    public int recvByteCount;
    public TEA tea;
    public String currentIp;
    public int currentPort;
    public boolean isSYNC = true;

    public final boolean isConnected() {
        return this.isConnected;
    }

    public final void setHandler(IMessageListener class158) {
        this.messageHandler = class158;
    }

    public final void setReader(MsgReader class162) {
        this.reader = class162;
    }

    public final void setSender(MsgSender class163) {
        this.sender = class163;
    }

    public final void Method288(String str, int i) {
        new Thread(new Class161(this, str, i)).start();
    }

    public final void sendMessage(Message msg) {
        this.sender.addMessage(msg);
    }

    public final void processMessages() {
        this.reader.processMessage();
    }

    public final void close() {
        try {
            this.currentIp = null;
            this.currentPort = -1;
            this.isConnected = false;
            if (this.sender != null) {
                this.sender.stop();
            }
            if (this.dos != null) {
                this.dos.close();
                this.dos = null;
            }
            if (this.dis != null) {
                this.dis.close();
                this.dis = null;
            }
            if (this.sc != null) {
                this.sc.close();
                this.sc = null;
            }
            this.sendByteCount = 0;
            this.recvByteCount = 0;
        } catch (Exception unused) {
        }
    }

    public static void connect(MobileClient session, String str, int i) {
        try {
            session.sc = (SocketConnection) Connector.open(new StringBuffer("socket://").append(str).append(":").append(i).toString());
            session.dos = session.sc.openDataOutputStream();
            session.dis = session.sc.openDataInputStream();
            long currentTimeMillis = System.currentTimeMillis();
            session.sender.writeKey(currentTimeMillis);
            session.tea = new TEA(currentTimeMillis);
            session.isConnected = true;
            new Thread(session.sender).start();
            new Thread(session.reader).start();
            session.currentIp = str;
            session.currentPort = i;
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
