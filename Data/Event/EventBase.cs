using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event
{
    public abstract class EventBase
    {
        public virtual string Name { get; set; }
        public virtual bool Condition { get; set; }

        public virtual bool NeedRemove { get; set; }
        protected EventBase() { }

        public virtual void Update()
        {

        }
    }
}
