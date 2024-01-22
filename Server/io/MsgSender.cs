 

public class MsgSender   {

    protected Session session;
    protected List<Message> sendingMessage = new();

    public MsgSender(Session session) {
        this.session = session;
    }

    public void addMessage(Message message) {
        lock (this.sendingMessage) {
            this.sendingMessage.addElement(message);
            this.sendingMessage.notifyAll();
        }
    }

     
    public void run() {
        while (true) {
            try {
                if (this.session.isConnected) {
                    lock (this.sendingMessage) {
                        while (!this.sendingMessage.isEmpty()) {
                            if (this.session.isConnected) {
                                Message m = (Message) this.sendingMessage.elementAt(0);
                                this.sendingMessage.removeElementAt(0);
                                this.doSendMessage(m);
                            }
                        }
                        
                            this.sendingMessage.wait();
                         
                        continue;
                    }
                }
            } catch (Exception var6) {
            }

            return;
        }
    }

    public void doSendMessage(Message m)   {
        sbyte[] data = m.getBuffer();
        Session var10000;
        if (data != null) {
            if (m.isEncrypted) {
                data = this.session.tea.encrypt(data);
            }

            this.session.dos.writeInt(data.Length + 1);
            this.session.dos.writesbyte(m.isEncrypted ? 1 : 0);
            this.session.dos.write(data);
            var10000 = this.session;
            var10000.sendsbyteCount += data.Length;
        } else {
            this.session.dos.writeInt(0);
        }

        var10000 = this.session;
        var10000.sendsbyteCount += 4;
        this.session.dos.flush();
    }

    public void stop() {
        lock (this.sendingMessage) {
            this.sendingMessage.Clear();
            this.sendingMessage.notifyAll();
        }
    }
}
