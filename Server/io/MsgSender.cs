

using Gopet.IO;

public class MsgSender
{

    protected Session session;
    protected List<Message> sendingMessage = new();

    public MsgSender(Session session)
    {
        this.session = session;
    }

    public void addMessage(Message message)
    {
        lock (this.sendingMessage)
        {
            this.sendingMessage.Add(message);
            Monitor.PulseAll(this.sendingMessage);
        }
    }


    public void run()
    {
        while (true)
        {
            try
            {
                if (this.session.isConnected())
                {
                    lock (this.sendingMessage)
                    {
                        while (this.sendingMessage.Count != 0)
                        {
                            if (this.session.isConnected())
                            {
                                Message m = this.sendingMessage[0];
                                this.sendingMessage.RemoveAt(0);
                                this.doSendMessage(m);
                            }
                        }

                        Monitor.Wait(this.sendingMessage);
                        continue;
                    }
                }
            }
            catch (Exception var6)
            {
            }

            return;
        }
    }

    public void doSendMessage(Message m)
    {
        sbyte[] data = m.getBuffer();
        Session var10000;
        if (data != null)
        {
            if (m.isEncrypted)
            {
                data = this.session.tea.encrypt(data);
            }

            this.session.dos.WriteInt(data.Length + 1);
            this.session.dos.Write(((sbyte)(m.isEncrypted ? 1 : 0)).toByte());
            this.session.dos.Write(data);
            var10000 = this.session;
            var10000.sendsbyteCount += data.Length;
        }
        else
        {
            this.session.dos.WriteInt(0);
        }
        Console.WriteLine("data length " + data.Length);
        var10000 = this.session;
        var10000.sendsbyteCount += 4;
        this.session.dos.Flush();
    }

    public void stop()
    {
        lock (this.sendingMessage)
        {
            this.sendingMessage.Clear();
            Monitor.PulseAll(this.sendingMessage);
        }
    }
}
