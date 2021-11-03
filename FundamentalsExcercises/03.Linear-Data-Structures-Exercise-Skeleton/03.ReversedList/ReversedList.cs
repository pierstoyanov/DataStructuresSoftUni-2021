namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return items[Count - index - 1];
            }
            set
            {
                ValidateIndex(index);
                items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (Count + 1 == items.Length)
                Grow();

            items[Count++] = item;
        }

        public bool Contains(T item)
        {
            foreach (T element in items)
            {
                if (element.Equals(item))
                    return true;
            }
            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (items[i].Equals(item))
                    return Count - 1 - i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            ValidateIndex(index);
            if (Count == items.Length)
                Grow();

            for (int i = Count; i >= Count - index; i--)
            {
                items[i] = items[i - 1];
            }

            items[Count - index] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            var indexOfItem = Array.IndexOf(items, item);
            //return if item not found
            if (indexOfItem == -1)
                return false;

            for (int i = indexOfItem; i <= Count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            //clear last item and subract count
            items[Count - 1] = default(T);
            Count--;

            return true;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            for (int i = Count - index - 1; i < Count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            //clear last item and subtract count
            items[Count - 1] = default(T);
            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return items[i];
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
        }

        private void Grow()
        {
            ///doubles the size of items array
            var temp = new T[items.Length * 2];
            Array.Copy(items, temp, Count);
            items = temp;
        }
    }
}