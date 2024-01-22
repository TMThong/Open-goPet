using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Collections
{
    [Serializable]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class CopyOnWriteArrayList<T> : IEnumerable<T>
    {

        private ImmutableList<T> values = ImmutableList<T>.Empty;

        public CopyOnWriteArrayList(ImmutableList<T> values)
        {
            this.values = values;
        }

        public CopyOnWriteArrayList(T[] values)
        {
            this.values = ImmutableList.CreateRange(values);
        }

        public CopyOnWriteArrayList(IEnumerable<T> values)
        {
            this.values = ImmutableList.CreateRange(values);
        }

        public CopyOnWriteArrayList()
        {
        }


        public void Add(T item)
        {
            lock (this)
            {
                this.values = this.values.Add(item);
            }
        }

        public bool Contains(T item)
        {
            lock (this)
            {
                return this.values.Contains(item);
            }
        }


        public void Remove(T item)
        {
            lock (this)
            {
                this.values = this.values.Remove(item);
            }
        }


        public int Count
        {
            get
            {
                return this.values.Count;
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
