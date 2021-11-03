namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        public Node<T> Head { get; set; }

        public Node<T> End { get; set; }

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
            if (Count == 0)
                throw new InvalidOperationException();

            Node<T> result = Head;
            Head = Head.Next;
            result.Next = null;
            Count--;
            return result.Item;
        }

        public void Enqueue(T item)
        {
            Node<T> newNode = new Node<T>(item);

            if (Head == null)
            {
                Head = newNode;
                End = newNode;
            }
            else
            {
                Node<T> currentNode = Head;
                while (currentNode.Next != null)
                    currentNode = currentNode.Next;
                currentNode.Next = newNode;
                End = newNode;
            }

            Count++;
        }

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException();
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
    }
}