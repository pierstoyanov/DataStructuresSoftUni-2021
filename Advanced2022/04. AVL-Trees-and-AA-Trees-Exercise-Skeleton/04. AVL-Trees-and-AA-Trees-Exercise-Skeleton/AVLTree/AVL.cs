namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }
        }

        public Node Root { get; private set; }

        public bool Contains(T element)
        {
            throw new InvalidOperationException();
        }

        public void Delete(T element)
        {
            throw new InvalidOperationException();
        }

        public void DeleteMin()
        {
            throw new InvalidOperationException();
        }

        public void Insert(T element)
        {
            throw new InvalidOperationException();
        }

        public void EachInOrder(Action<T> action)
        {
            throw new InvalidOperationException();
        }
    }
}
