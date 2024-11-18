using Gopet.Util;
using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Gopet.Data.Collections
{
    [Serializable]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class CopyOnWriteArrayList<T> : IEnumerable<T>
    {

        private ImmutableList<T> values = ImmutableList<T>.Empty;

        private Mutex mutex = new Mutex();


        public CopyOnWriteArrayList(ImmutableList<T> values)
        {
            this.values = values;
        }


        public CopyOnWriteArrayList(params T[] values)
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


        public T this[int index]
        {
            get
            {
                return get(index);
            }
            set
            {
                Set(index, value);
            }
        }
        public void Add(T item)
        {
            mutex.WaitOne();
            try
            {
                this.values = values.Add(item);
            }
            catch (Exception e) { e.printStackTrace(); }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public bool Contains(T item)
        {
            return this.values.Contains(item);
        }


        public void Remove(T item)
        {
            mutex.WaitOne();
            try
            {
                this.values = values.Remove(item);
            }
            catch (Exception e) { e.printStackTrace(); }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public T get(int index)
        {
            return this.values[index];
        }


        public int Count
        {
            get
            {
                return this.values.Count;
            }
        }


        public bool IsEmpty
        {
            get
            {
                return this.values.IsEmpty;
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

        public void remove(T item)
        {
            this.Remove(item);
        }

        public void Clear()
        {
            mutex.WaitOne();
            try
            {
                this.values = values.Clear();
            }
            catch (Exception e) { e.printStackTrace(); }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public void AddRange(IEnumerable<T> datas)
        {
            mutex.WaitOne();
            try
            {
                this.values = values.AddRange(datas);
            }
            catch (Exception e) { e.printStackTrace(); }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public void Sort(IComparer<T>? comparer)
        {
            mutex.WaitOne();
            try
            {
                this.values = values.Sort(comparer);
            }
            catch (Exception e) { e.printStackTrace(); }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public void add(int index, T item)
        {
            mutex.WaitOne();
            try
            {
                this.values = this.values.Insert(index, item);
            }
            catch (Exception e) { e.printStackTrace(); }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public void Add(int index, T item)
        {
            add(index, item);
        }

        public CopyOnWriteArrayList<T> clone()
        {
            return new CopyOnWriteArrayList<T>(this.values.ToArray());
        }

        public int IndexOf(T data)
        {
            return this.values.IndexOf(data);
        }

        public void Set(int indexSlot, T data)
        {
            mutex.WaitOne();
            try
            {
                this.values = this.values.SetItem(indexSlot, data);
            }
            catch (Exception e) { e.printStackTrace(); }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public void removeAt(int index)
        {
            mutex.WaitOne();
            try
            {
                this.values = this.values.RemoveAt(index);
            }
            catch (Exception e) { e.printStackTrace(); }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public void addIfAbsent(T data)
        {
            if (!this.values.Contains(data)) Add(data);
        }
    }
}
