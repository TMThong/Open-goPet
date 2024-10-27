using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Collections
{
    public class ConcurrentHashMap<TKey, TValue> : System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue>
    {
        public ConcurrentHashMap()
        {
        }

        public ConcurrentHashMap(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection)
        {
        }

        public ConcurrentHashMap(IEqualityComparer<TKey>? comparer) : base(comparer)
        {
        }

        public ConcurrentHashMap(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer) : base(collection, comparer)
        {
        }

        public ConcurrentHashMap(int concurrencyLevel, int capacity) : base(concurrencyLevel, capacity)
        {
        }

        public ConcurrentHashMap(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer) : base(concurrencyLevel, collection, comparer)
        {
        }

        public ConcurrentHashMap(int concurrencyLevel, int capacity, IEqualityComparer<TKey>? comparer) : base(concurrencyLevel, capacity, comparer)
        {
        }
        public void put(TKey key, TValue value)
        {
            base[key] = value;
        }

        public bool ContainsKey(TKey key)
        {
            return base.ContainsKey(key);
        }

        public TValue get(TKey key) { 
            if (!base.ContainsKey(key))
            {
                return default(TValue);
            }
            return base[key];
        }

        public TValue remove(TKey key)
        {
            TValue v;
            base.TryRemove(key, out v);
            return v;
        }
    }
}
