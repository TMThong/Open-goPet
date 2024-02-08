using Gopet.Data.Collections;
using Gopet.Util;

namespace Gopet.IO
{
    public class MsgSender
    {

        protected Session session;
        protected Queue<Message> sendingMessage = new Queue<Message>();

        public static CopyOnWriteArrayList<MsgSender> msgSenders = new CopyOnWriteArrayList<MsgSender>();

        private bool isClose = false;

        public MsgSender(Session session)
        {
            this.session = session;
            if (session == null)
            {
                throw new ArgumentNullException();
            }
            msgSenders.add(this);
        }

        public void addMessage(Message message)
        {
            lock (sendingMessage)
            {
                sendingMessage.Enqueue(message);
                Monitor.PulseAll(sendingMessage);
            }
        }


        public void run()
        {
            while (true)
            {
                try
                {
                    if (session.isConnected() && !isClose)
                    {
                        lock (sendingMessage)
                        {
                            try
                            {
                                while (sendingMessage.Count != 0)
                                {
                                    if (session.isConnected())
                                    {
                                        Message m = sendingMessage.Dequeue();
                                        doSendMessage(m);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                e.printStackTrace();
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
            isClose = true;
            lock (sendingMessage)
            {
                sendingMessage.Clear();
                Monitor.PulseAll(sendingMessage);
            }
            msgSenders.Remove(this);
        }

        public void Release()
        {
            lock (sendingMessage)
            {
                Monitor.PulseAll(sendingMessage);
            }
        }
    }
}