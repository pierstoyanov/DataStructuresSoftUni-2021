namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

        public T this[int index]
        {
            //INDEXER
            get
            {
                ValidateIndex(index);
                return _items[index];
            }
            set
            {
                ValidateIndex(index);
                this._items[index] = value;
            }
        }

        public List(int capacity = DEFAULT_CAPACITY)
        {
            if (capacity < 0)
                throw new IndexOutOfRangeException(nameof(capacity));
            this._items = new T[capacity];
        }

        public int Count { get; private set; }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index > Count - 1)
                throw new IndexOutOfRangeException(nameof(index));
        }

        private T[] DoubleArraySize(T[] ArrayToGrow)
        {
            /*Double the size of given array.*/
            T[] NewArray = new T[ArrayToGrow.Length * 2];
            for (int i = 0; i < ArrayToGrow.Length; i++)
            {
                NewArray[i] = ArrayToGrow[i];
            }
            return NewArray;
        }

        public void Add(T item)
        {
            if (Count == _items.Length)
                _items = DoubleArraySize(_items);
            _items[Count++] = item;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if ( object.Equals(_items[i], item) )
                    return true;
            }
            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if ( object.Equals(_items[i], item) )
                    return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            ValidateIndex(index);

            if (Count > _items.Length)
                DoubleArraySize(_items);
            for (int i = Count; i > index; i-- )
                _items[i] = _items[i-1];
            _items[index] = item;

            Count++;
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < Count - 1; i++)
            {
                if ( object.Equals(_items[i], item) )
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);
            for (int i = index; i < Count - 1; i++)
                _items[i] = _items[i + 1];
            _items[Count - 1] = default;
            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _items[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}