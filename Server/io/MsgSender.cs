package server.io;

import java.io.IOException;
import java.util.Vector;

public class MsgSender implements Runnable {

    protected Session session;
    protected final Vector sendingMessage = new Vector();

    public MsgSender(Session session) {
        this.session = session;
    }

    public void addMessage(Message message) {
        synchronized (this.sendingMessage) {
            this.sendingMessage.addElement(message);
            this.sendingMessage.notifyAll();
        }
    }

    @Override
    public void run() {
        while (true) {
            try {
                if (this.session.isConnected) {
                    synchronized (this.sendingMessage) {
                        while (!this.sendingMessage.isEmpty()) {
                            if (this.session.isConnected) {
                                Message m = (Message) this.sendingMessage.elementAt(0);
                                this.sendingMessage.removeElementAt(0);
                                this.doSendMessage(m);
                            }
                        }
                        try {
                            this.sendingMessage.wait();
                        } catch (InterruptedException var4) {
                            var4.printStackTrace();
                        }
                        continue;
                    }
                }
            } catch (Exception var6) {
            }

            return;
        }
    }

    public void doSendMessage(Message m)   {
        byte[] data = m.getBuffer();
        Session var10000;
        if (data != null) {
            if (m.isEncrypted) {
                data = this.session.tea.encrypt(data);
            }

            this.session.dos.writeInt(data.length + 1);
            this.session.dos.writeByte(m.isEncrypted ? 1 : 0);
            this.session.dos.write(data);
            var10000 = this.session;
            var10000.sendByteCount += data.length;
        } else {
            this.session.dos.writeInt(0);
        }

        var10000 = this.session;
        var10000.sendByteCount += 4;
        this.session.dos.flush();
    }

    public void stop() {
        synchronized (this.sendingMessage) {
            this.sendingMessage.removeAllElements();
            this.sendingMessage.notifyAll();
        }
    }
}
