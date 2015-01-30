namespace Genesys.Core.Framework.Collections
{
    #region Using

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    #endregion

    public class ConcurrentObservableList<T> : INotifyCollectionChanged, IList, IList<T>
    {
        private readonly List<T> theCollection = new List<T>();
        public object SyncRoot
        {
            get
            {
                return ((ICollection)this.theCollection).SyncRoot;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return ((ICollection)this.theCollection).IsSynchronized;
            }
        }

        int IList.Add(object value)
        {
            return this.Lock(
                () =>
                {
                    this.Add((T)value);
                    return this.Count - 1;
                });
        }

        bool IList.Contains(object value)
        {
            return this.Contains((T)value);
        }

        public void Clear()
        {
            IList<T> copyList = null;
            this.Lock(
                () =>
                {
                    copyList = this.CopyList();
                    this.theCollection.Clear();
                });

            this.CollectionChanged.NotifyRemoved(this, copyList);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            this.Remove((T)value);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo((T[])array, index);
        }

        public int Count
        {
            get
            {
                return this.Lock(() => this.theCollection.Count);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList)this.theCollection).IsReadOnly;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                return ((IList)this.theCollection).IsFixedSize;

            }
        }

        public void RemoveAt(int index)
        {
            T item = this.Lock(
                () =>
                {
                    var i = this.theCollection[index];
                    this.theCollection.RemoveAt(index);
                    return i;
                });

            this.CollectionChanged.NotifyRemoved(this, item);
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (T)value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var copyList = this.CopyList();

            return copyList.GetEnumerator();
        }

        public void Add(T item)
        {
            this.Lock(() => this.theCollection.Add(item));
            this.CollectionChanged.NotifyAdded(this, item);
        }

        public bool Contains(T item)
        {
            return this.Lock(() => this.theCollection.Contains(item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.Lock(
                () =>
                {
                    for (int i = arrayIndex; i < array.Count(); i++)
                    {
                        array[i] = this.theCollection[i];
                    }
                });
        }

        public bool Remove(T item)
        {
            bool result = this.Lock(() => this.theCollection.Remove(item));

            if (result)
            {
                this.CollectionChanged.NotifyRemoved(this, item);
            }
            return result;
        }

        public int IndexOf(T item)
        {
            return this.Lock(() => this.theCollection.IndexOf(item));
        }

        public void Insert(int index, T item)
        {
            this.Lock(() => this.theCollection.Insert(index, item));

            this.CollectionChanged.NotifyInsert(this, index, item);
        }

        public T this[int index]
        {
            get
            {
                return this.Lock(() => this.theCollection[index]);
            }
            set
            {
                T oldItem = this.Lock(
                    () =>
                    {
                        var t = this.theCollection[index];
                        this.theCollection[index] = value;
                        return t;
                    });

                this.CollectionChanged.NotifyReplaced(this, index, value, oldItem);
            }
        }

        private List<T> CopyList()
        {
            List<T> copyList = null;

            this.Lock(() => copyList = new List<T>(this.theCollection));
            return copyList;
        }

        private void Lock(Action action)
        {
            lock (this.SyncRoot)
            {
                action();
            }
        }

        private TResult Lock<TResult>(Func<TResult> function)
        {
            lock (this.SyncRoot)
            {
                return function();
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}