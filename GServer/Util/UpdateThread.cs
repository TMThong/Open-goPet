using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Util
{
    public class UpdateThread
    {
        public Thread UThread { get; }

        public AutoResetEvent AutoResetEvent { get; } = new AutoResetEvent(false);

        public TimeSpan? TimeSleep { get; protected set; }

        public bool Enable { get; private set; } = false;

        public UpdateThread(string name)
        {
            UThread = new Thread(Runner);
            UThread.IsBackground = true;
            UThread.Name = name;
        }


        public void Start()
        {
            if (Enable)
            {
                throw new System.Exception("Luồng này bật rồi");
            }
            Enable = true;
            UThread.Start();
        }

        public virtual void Runner()
        {
            while (Enable)
            {
                Update();
                if (TimeSleep.HasValue)
                {
                    AutoResetEvent.WaitOne((int)TimeSleep.Value.TotalMilliseconds);
                }
            }
        }

        public virtual void Update ()
        {

        }

        public virtual void ReleaseMutex()
        {
            AutoResetEvent.Set();
        }


        public void Stop()
        {
            if (!Enable) 
            { 
                throw new System.Exception("Luồng này tắt rồi mà");
            }
            Enable = false;
        }
    }
}
