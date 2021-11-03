namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            foreach (var element in this)
            {
                if (element.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void StackIsNotEmpty()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
        }

        public T Peek()
        {
            StackIsNotEmpty();
            return _top.Item;
        }

        public T Pop()
        {
            StackIsNotEmpty();
            T topNodeItem = this._top.Item;
            //take current next node as newTop var
            Node<T> newTop = _top.Next;
            //change reffetence to top.Next;
            _top.Next = null;
            //change top to the next node;
            _top = newTop;
            Count--;

            return topNodeItem;
        }

        public void Push(T item)
        {
            var newNode = new Node<T>
            {
                Item = item,
                Next = _top
            };

            _top = newNode;
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _top;
            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}