package vn.me.network;

import java.util.Vector;

/* renamed from: Class162  reason: default package */
 /* loaded from: gopet_repackage.jar:Class162.class */
public final class MsgReader implements Runnable {

    protected MobileClient session;
    public final Vector inputMessages = new Vector(10);
    private long idleTime = 0L;

    public MsgReader(MobileClient session) {
        this.session = session;
    }

    protected boolean isIdle() {
        if (this.idleTime > 0L) {
            long t = System.currentTimeMillis() - this.idleTime;
            if (t > 120000L) {
                return true;
            }
        }

        return false;
    }

    ///////@Override
    public void run() {
        while (true) {
            try {
                if (this.session.isConnected) {
                    Message message = this.readMessage();
                    if (message != null) {
                        if (this.session.isSYNC) {
                            //System.out.println("add message to quue");
                            synchronized (this.inputMessages) {
                                this.inputMessages.addElement(message);
                                continue;
                            }
                        }
                        this.session.messageHandler.onMessage(message);
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

    public void processMessage() {
        synchronized (this.inputMessages) {
            int messageNum = this.inputMessages.size();
            for (int i = 0; i < messageNum; ++i) {
                this.session.messageHandler.onMessage((Message) this.inputMessages.elementAt(i));
            }
            this.inputMessages.removeAllElements();
        }
    }

    private Message readMessage() throws Exception {
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

                MobileClient var10000 = this.session;
                var10000.recvByteCount += 4 + length;
                return msg;
            }
        }
    }
}
