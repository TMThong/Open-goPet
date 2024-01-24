using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public void add(T item)
        {
            this.Add(item);
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

        public T get(int index)
        {
            lock (this)
            {
                return this.values[index];
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

        public bool isEmpty()
        {
            return this.values.IsEmpty;
        }

        public void remove(T item)
        {
            this.Remove(item);
        }

        public void Clear()
        {
            lock (this)
            {
                this.values = ImmutableList<T>.Empty;
            }
        }

        public void AddRange(IEnumerable<T> datas)
        {
            lock (this)
            {
                this.values = this.values.AddRange(datas);
            }
        }

        public void Sort(IComparer<T>? comparer)
        {
            lock (this)
            {
                this.values = this.values.Sort(comparer);
            }
        }

        public void add(int index, T item)
        {
            lock (this)
            {
                this.values = this.values.Insert(index, item);
            }
        }

        public void Add(int index, T item)
        {
            lock (this)
            {
                this.values = this.values.Insert(index, item);
            }
        }

        public CopyOnWriteArrayList<T> clone()
        {
            return new CopyOnWriteArrayList<T>(this.values.ToArray());
        }

        public int indexOf(T data)
        {
            return this.values.IndexOf(data);
        }

        public void set(int indexSlot, T data)
        {
            lock (this)
            {
                this.values = this.values.SetItem(indexSlot, data);
            }
        }

        public void remove(int index)
        {
            lock (this)
            {
                this.values = this.values.RemoveAt(index);
            }
        }

        public void addIfAbsent(T data)
        {
            if(!this.values.Contains(data)) Add(data);
        }
    }
}
