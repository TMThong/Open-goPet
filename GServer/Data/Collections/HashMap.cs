using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Collections
{
    public class HashMap<TKey, Tvalue> : Dictionary<TKey, Tvalue>
    {
        public HashMap()
        {
        }

        public HashMap(IDictionary<TKey, Tvalue> dictionary) : base(dictionary)
        {
        }

        public HashMap(IEnumerable<KeyValuePair<TKey, Tvalue>> collection) : base(collection)
        {
        }

        public HashMap(IEqualityComparer<TKey>? comparer) : base(comparer)
        {
        }

        public HashMap(int capacity) : base(capacity)
        {
        }

        public HashMap(IDictionary<TKey, Tvalue> dictionary, IEqualityComparer<TKey>? comparer) : base(dictionary, comparer)
        {
        }

        public HashMap(IEnumerable<KeyValuePair<TKey, Tvalue>> collection, IEqualityComparer<TKey>? comparer) : base(collection, comparer)
        {
        }

        public HashMap(int capacity, IEqualityComparer<TKey>? comparer) : base(capacity, comparer)
        {
        }

        protected HashMap(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public void put(TKey key, Tvalue value)
        {
            base[key] = value;
        }


        public Tvalue get(TKey key)
        {
            if (!base.ContainsKey(key))
            {
                return default(Tvalue);
            }
            return base[key];
        }

        public void Clear()
        {
            base.Clear();
        }
    }
}
