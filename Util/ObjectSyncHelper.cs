using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Util
{

    public class ObjectSyncHelper
    {


        public TimeSpan TimeOut { get; }

        protected Mutex mutex = new Mutex();

        public ObjectSyncHelper() : this(TimeSpan.FromMilliseconds(2000))
        {
        }

        public ObjectSyncHelper(TimeSpan timeOut)
        {
            TimeOut = timeOut;
        }

        public void Invoke(Action toDo, string debugMethod)
        {
            mutex.WaitOne();
            try
            {
                toDo();
            }
            catch (Exception ex) { }
            mutex.ReleaseMutex();
        }

        public T Invoke<T>(Func<T> toDo, string debugMethod)
        {
            mutex.WaitOne();
            try
            {
                return toDo();
            }
            catch (Exception ex) { }
            mutex.ReleaseMutex();
            return default(T);
        }
    }
}
