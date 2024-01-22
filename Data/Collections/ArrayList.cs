using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Collections
{
    public class ArrayList<T> : List<T>
    {
        public ArrayList()
        {
        }

        public ArrayList(IEnumerable<T> collection) : base(collection)
        {
        }

        public ArrayList(int capacity) : base(capacity)
        {
        }

        public void add(T item)
        {
            base.Add(item);
        }

        public bool contains(T item)
        {
            return base.Contains(item);
        }

        public bool isEmpty()
        {
            return base.Count <= 0;
        }

        public T get(int index) => base[index];
    }
}
