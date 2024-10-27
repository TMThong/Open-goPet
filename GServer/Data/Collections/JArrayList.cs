using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Collections
{
    public class JArrayList<T> : List<T>
    {
        public JArrayList()
        {
        }

        public JArrayList(IEnumerable<T> collection) : base(collection)
        {
        }

        public JArrayList(int capacity) : base(capacity)
        {
        }

        public void add(T item)
        {
            base.Add(item);
        }

        public bool Contains(T item)
        {
            return base.Contains(item);
        }

        public bool isEmpty()
        {
            return base.Count <= 0;
        }

        public T get(int index) => base[index];

        public void remove(T itemId)
        {
            base.Remove(itemId);
        }

        public void remove(Object itemId)
        {
            base.Remove((T)itemId);
        }
    }
}
