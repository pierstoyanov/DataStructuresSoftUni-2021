namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> Head;
        private Node<T> Tail;
        public int Count { get; private set; }

        public bool Contains(T item)
        {
            Node<T> current = Head;

            while (current != null)
            {
                if (current.Item.Equals(item))
                    return true;
                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            CheckNotEmpty();
            
            Node<T> currentHead = Head;

            if (currentHead == Tail)
                Tail = null;

            Head = Head.Next;
            currentHead.Next = null;
            Count--;
            return currentHead.Item;
        }

        public void Enqueue(T item)
        {            
            Node<T> newNode = new Node<T>
            {
                Item = item,
                Next = null
            };

            if (Count == 0)
            {
                Head = Tail = newNode;
                Count++;
                return;
            }
            Tail.Next = newNode;
            Tail = Tail.Next;
            Count++;
            return;
        }

        public T Peek()
        {
            CheckNotEmpty();
            return Head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void CheckNotEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();
        }
    }
}