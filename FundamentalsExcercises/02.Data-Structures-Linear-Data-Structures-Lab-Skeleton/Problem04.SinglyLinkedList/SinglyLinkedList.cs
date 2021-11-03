namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> Head { get; set; }

        private Node<T> EndElement { get; set; }

        public int Count { get; private set; }

        private void CheckHeadNotNull()
        {
            if (Head == null)
                throw new InvalidOperationException();
        }

        private void CheckEndNotNull()
        {
            if (EndElement == null)
                throw new InvalidOperationException();
        }

        public void AddFirst(T item)
        {
            Node<T> newHead = new Node<T>(item);

            if (Head != null)
            {
                newHead.Next = Head;
                EndElement = newHead;
            }

            Head = newHead;
            Count++;
        }

        public void AddLast(T item)
        {
            Node<T> newNode = new Node<T>(item);

            if (Head == null)
            {
                Head = newNode;
                EndElement = newNode;
            }
            else
            {
                Node<T> current = Head;
                while (current.Next != null)
                    current = current.Next;
                current.Next = newNode;
                EndElement = newNode;
            }
            Count++;
        }

        public T GetFirst()
        {
            CheckHeadNotNull();
            return Head.Value;
        }

        public T GetLast()
        {
            CheckHeadNotNull();
            Node<T> current = Head;

            while (current.Next != null)
                current = current.Next;

            return current.Value;
        }

        public T RemoveFirst()
        {
            CheckHeadNotNull();
            CheckEndNotNull();
            if (Count == 1)
            {
                Node<T> result = Head;
                Head = null;
                Count--;
                return result.Value;
            }

            CheckHeadNotNull();
            var oldHead = Head;
            Head = Head.Next;
            Count--;
            return oldHead.Value;
        }

        public T RemoveLast()
        {
            CheckHeadNotNull();
            CheckEndNotNull();

            if (Count == 1)
            {
                Node<T> result = Head;
                Head = null;
                Count--;
                return result.Value;
            }

            Node<T> current = Head;

            while (current.Next.Next != null)
                current = current.Next;

            Node<T> last = current.Next;
            current.Next = null;
            EndElement = current;
            Count--;
            return last.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentElement = Head;
            while (currentElement != null)
            {
                yield return currentElement.Value;
                currentElement = currentElement.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}