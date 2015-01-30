namespace Net_4._0_Extentions
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public static class LinqExtensions
    {
        public static TSource FirstNonDefaultOrDefault<TSource>(this IEnumerable<TSource> source)
        {
                return source.FirstOrDefault(t => !EqualityComparer<TSource>.Default.Equals(t,default(TSource)));
        }

        public static TSource ClosestGreaterOrMax<TSource>(this SortedSet<TSource> sortedSet, TSource item)
            where TSource : IComparable
        {
            TSource foundItem = sortedSet.FirstOrDefault(span => span.CompareTo(item) > 0);
            if (EqualityComparer<TSource>.Default.Equals(foundItem, default(TSource)))
            {
                foundItem = sortedSet.Last();
            }
            return foundItem;
        }

        public static TSource ClosestLesserOrMin<TSource>(this SortedSet<TSource> sortedSet, TSource item)
            where TSource : IComparable
        {
            TSource foundItem = sortedSet.LastOrDefault(span => span.CompareTo(item) < 0);
            if (EqualityComparer<TSource>.Default.Equals(foundItem, default(TSource)))
            {
                foundItem = sortedSet.First();
            }
            return foundItem;
        }


        public static IEnumerable<int> SelectIndices<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> selector)
        {
            return
                source.Select((c, i) => new KeyValuePair<int, TSource>(i, c))
                    .Where(pair => selector(pair.Value))
                    .Select(pair => pair.Key);
        }

        public static void AddOrUpdateIf<TK, TV>(this IDictionary<TK, TV> dictionary, TK key, TV value, Predicate<TV> condition)
        {
            TV value1;
            if (dictionary.TryGetValue(key, out value1))
            {
                if (condition(value1)) dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static void MergeOverwrite<TK, TV>(this IDictionary<TK, TV> dictionary, IDictionary<TK, TV> dictionaryToAdd)
        {
            foreach (var pair in dictionaryToAdd)
            {
                dictionary.AddOrUpdateIf(pair.Key, pair.Value, v => true);
            }
        }
    }
}