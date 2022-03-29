namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
	    private Node<T> tail;

        public int Count { get; private set; }

        private void CheckListNotEmpty()
        {
            if (Count == 0)
                throw new InvalidOperationException();
        }


        public void AddFirst(T item)
        {
            var newHead = new Node<T>(item, null, null);

            if (Count == 0)
            {
                head = tail = newHead;
                Count++;
                return;
            }

            newHead.Next = head;
            head.Previous = newHead;
            head = newHead;
            Count++;
        }

        public void AddLast(T item)
        {
            var newEnd = new Node<T>(item, null, null);

            if (Count == 0)
            {
                head = tail = newEnd;
                Count++;
                return;
            }

            Node<T> oldTail = tail;
            oldTail.Next = newEnd;

            tail = newEnd;
            tail.Previous = oldTail;
            Count++;
        }

        public T GetFirst()
        {
            CheckListNotEmpty();
            return head.Item;
        }

        public T GetLast()
        {
            CheckListNotEmpty();
            return tail.Item;
        }

        public T RemoveFirst()
        {
            CheckListNotEmpty();
            Node<T> result = head;

            if (Count == 1)
            {
                head = tail = null;
                Count--;
                return result.Item;
            }

            head = head.Next;
            Count--;
            return result.Item;
        }

        public T RemoveLast()
        {
            CheckListNotEmpty();
            if (Count == 1)
            {
                Node<T> result = head;
                head = null;
                tail = null;
                Count--;
                return result.Item;
            }

            Node<T> oldTail = tail;
            tail = tail.Previous;
            Count--;
            return oldTail.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentElement = head;
            while (currentElement != null)
            {
                yield return currentElement.Item;
                currentElement = currentElement.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
		    => this.GetEnumerator();
    }
}