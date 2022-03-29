namespace _01.Red_Black_Tree
{
    using System;
    using System.Collections.Generic;

    public class RedBlackTree<T> 
        : IBinarySearchTree<T> where T : IComparable
    {
        private Node root;

        public RedBlackTree()
        {
        }

        public int Count { get; }

        public void Insert(T element)
        {
            // TODO:
        }

        public T Select(int rank)
        {
            throw new NotImplementedException();
        }

        public int Rank(T element)
        {
            return 0;
        }

        public bool Contains(T element)
        {
            return false;
        }

        public IBinarySearchTree<T> Search(T element)
        {
            return null;
        }

        public void DeleteMin()
        {
        }

        public void DeleteMax()
        {
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            return null;
        }

        public  void Delete(T element)
        {
        }

        public T Ceiling(T element)
        {
            throw new NotImplementedException();
        }

        public T Floor(T element)
        {
            throw new NotImplementedException();
        }

        public void EachInOrder(Action<T> action)
        {
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Count { get; set; }
        }
    }
}