namespace Gopet.IO
{
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
            lock (sendingMessage)
            {
                sendingMessage.Add(message);
                Monitor.PulseAll(sendingMessage);
            }
        }


        public void run()
        {
            while (true)
            {
                try
                {
                    if (session.isConnected())
                    {
                        lock (sendingMessage)
                        {
                            while (sendingMessage.Count != 0)
                            {
                                if (session.isConnected())
                                {
                                    Message m = sendingMessage[0];
                                    sendingMessage.RemoveAt(0);
                                    doSendMessage(m);
                                }
                            }

                            Monitor.Wait(sendingMessage);
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
                    data = session.tea.encrypt(data);
                }

                session.dos.WriteInt(data.Length + 1);
                session.dos.Write(((sbyte)(m.isEncrypted ? 1 : 0)).toByte());
                session.dos.Write(data);
                var10000 = session;
                var10000.sendsbyteCount += data.Length;
            }
            else
            {
                session.dos.WriteInt(0);
            }
            var10000 = session;
            var10000.sendsbyteCount += 4;
            session.dos.Flush();
        }

        public void stop()
        {
            lock (sendingMessage)
            {
                sendingMessage.Clear();
                Monitor.PulseAll(sendingMessage);
            }
        }
    }
}