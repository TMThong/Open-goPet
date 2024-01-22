package server.io;

import java.util.Vector;

public class MsgReader implements Runnable {

    protected Session session;
    public final Vector inputMessages = new Vector(10);
    private long idleTime = 0L;

    public MsgReader(Session session) {
        this.session = session;
    }

    protected bool isIdle() {
        if (this.idleTime > 0L) {
            long t = System.currentTimeMillis() - this.idleTime;
            if (t > 120000L) {
                return true;
            }
        }

        return false;
    }

    @Override
    public void run() {
        while (true) {
            try {
                if (this.session.isConnected) {
                    Message message = this.readMessage();
                    if (message != null) {
                        if (this.session.isSYNC) {

                            synchronized (this.inputMessages) {
                                this.inputMessages.addElement(message);
                                continue;
                            }
                        }
                        this.session.messageHandler.onMessage(message);
                        this.session.msgCount++;
                        Thread.sleep(10);
                        continue;
                    }
                }
            } catch (Exception var5) {
            }

            if (this.session.isConnected) {
                if (this.session.messageHandler != null) {
                    this.session.messageHandler.onDisconnected();
                }
                this.session.close();
            }
            return;
        }
    }

    private Message readMessage()   {
        int length = 0;
        int hi = this.session.dis.readInt();
        if (hi == -1) {
            return null;
        } else {
            length = hi - 1;
            byte isEncrypted = (byte) this.session.dis.read();
            byte[] data = new byte[length];
            int len = 0;
            int byteRead = 0;

            while (len != -1 && byteRead < length) {
                len = this.session.dis.read(data, byteRead, length - byteRead);
                if (len > 0) {
                    byteRead += len;
                }
            }

            if (length == 0) {
                return null;
            } else {
                Message msg;
                if (isEncrypted == 1) {
                    msg = new Message(this.session.tea.decrypt(data));
                } else {
                    msg = new Message(data);
                }

                if (this.session.dis.available() <= 0) {
                    this.idleTime = System.currentTimeMillis();
                } else {
                    this.idleTime = 0L;
                }

                Session var10000 = this.session;
                var10000.recvByteCount += 4 + length;
                return msg;
            }
        }
    }

}
