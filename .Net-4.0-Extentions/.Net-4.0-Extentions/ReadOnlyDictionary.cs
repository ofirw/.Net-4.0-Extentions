namespace Net_4._0_Extentions
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    #endregion

    public sealed class ReadOnlyDictionary<TKey, TValue> : ReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        public ReadOnlyDictionary(IEnumerable<KeyValuePair<TKey, TValue>> items)
            : base(items.ToList())
        {
        }

        public TValue this[TKey key]
        {
            get
            {
                IEnumerable<KeyValuePair<TKey, TValue>> valueQuery = this.GetQuery(key).ToList();
                if (!valueQuery.Any()) throw new NullReferenceException("No value found for given key");

                return valueQuery.First().Value;
            }
        }

        public ReadOnlyCollection<TValue> Values
        {
            get
            {
                return new ReadOnlyCollection<TValue>(this.Items.Select(i => i.Value).ToList());
            }
        }

        public bool ContainsKey(TKey key)
        {
            return (this.GetQuery(key).Any());
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var toReturn = this.ContainsKey(key);
            value = toReturn ? this[key] : default(TValue);
            return toReturn;
        }

        private IEnumerable<KeyValuePair<TKey, TValue>> GetQuery(TKey key)
        {
            return (from t in this.Items where t.Key.Equals(key) select t);
        }
    }
}