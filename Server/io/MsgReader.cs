

using Gopet.IO;

public class MsgReader   {

    protected Session session;
    private long idleTime = 0L;

    public MsgReader(Session session) {
        this.session = session;
    }

 

    
    public void run() {
        while (true) {
            try {
                if (this.session.isConnected()) {
                    Message message = this.readMessage();
                    if (message != null) {
                         
                        this.session.messageHandler.onMessage(message);
                        this.session.msgCount++;
                        Thread.Sleep(10);
                        continue;
                    }
                }
            } catch (Exception var5) {
            }

            if (this.session.isConnected()) {
                if (this.session.messageHandler != null) {
                    this.session.messageHandler.onDisconnected();
                }
                this.session.close();
            }
            return;
        }
    }

    private Message readMessage()   {
        int Length = 0;
        int hi = this.session.dis.ReadJavaInt();
        if (hi == -1) {
            return null;
        } else {
            Length = hi - 1;
            sbyte isEncrypted =  this.session.dis.ReadSByte();
            byte[] data = new byte[Length];
            int len = 0;
            int sbyteRead = 0;

            while (len != -1 && sbyteRead < Length) {
                len = this.session.dis.Read(data, sbyteRead, Length - sbyteRead);
                if (len > 0) {
                    sbyteRead += len;
                }
            }

            if (Length == 0) {
                return null;
            } else {
                Message msg;
                if (isEncrypted == 1) {
                    msg = new Message(this.session.tea.decrypt(data.sbytes()));
                } else {
                    msg = new Message(data.sbytes());
                }
                Session var10000 = this.session;
                var10000.recvsbyteCount += 4 + Length;
                return msg;
            }
        }
    }

}
