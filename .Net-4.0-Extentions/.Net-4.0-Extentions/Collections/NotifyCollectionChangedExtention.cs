namespace Genesys.Core.Framework.Collections
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public static class NotifyCollectionChangedExtention
    {
        public static void NotifyRemoved<T>(this NotifyCollectionChangedEventHandler handler, object sender, IList<T> removedItems)
        {
            OnCollectionChanged(handler, sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (IList)removedItems));
        }

        public static void NotifyRemoved<T>(this NotifyCollectionChangedEventHandler handler, object sender, T item)
        {
            OnCollectionChanged(handler, sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }

        public static void NotifyAdded<T>(this NotifyCollectionChangedEventHandler handler, object sender, T item)
        {
            OnCollectionChanged(handler, sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public static void NotifyInsert<T>(this NotifyCollectionChangedEventHandler handler, object sender, int index, T item)
        {
            OnCollectionChanged(handler, sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public static void NotifyReplaced<T>(this NotifyCollectionChangedEventHandler handler, object sender, int index, T value, T oldItem)
        {
            OnCollectionChanged(handler, sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
        }

        private static void OnCollectionChanged(this NotifyCollectionChangedEventHandler handler, object sender,  NotifyCollectionChangedEventArgs e)
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}