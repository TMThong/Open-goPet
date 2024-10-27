using Gopet.Data.Collections;
using Gopet.Util;
using System.Collections.Concurrent;

namespace Gopet.IO
{
    public class MsgSender
    {

        protected Session session;
        protected ConcurrentQueue<Message> sendingMessage = new ConcurrentQueue<Message>();
        protected AutoResetEvent messageEvent = new AutoResetEvent(false);
        public static CopyOnWriteArrayList<MsgSender> msgSenders = new CopyOnWriteArrayList<MsgSender>();
        public static readonly Random random = new Random();
        private bool isClose = false;

        public MsgSender(Session session)
        {
            this.session = session;
            if (session == null)
            {
                throw new ArgumentNullException();
            }
            msgSenders.Add(this);
        }

        public void addMessage(Message message)
        {
            sendingMessage.Enqueue(message);
            messageEvent.Set();
        }


        public void run()
        {
            while (true)
            {
                try
                {
                    if (session.isConnected() && !isClose)
                    {
                        while (sendingMessage.TryDequeue(out Message message))
                        {
                            if (message != null)
                            {
                                doSendMessage(message);
                            }
                        }
                        messageEvent.WaitOne(random.Next(5000, 30000));
                        continue;
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
            sendingMessage.Clear();
            msgSenders.Remove(this);
            messageEvent.Set();
        }

        public void Release()
        {
            messageEvent.Set();
        }

        ~MsgSender()
        {
            stop();
        }
    }
}