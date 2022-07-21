namespace _02._AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        public int CountNodes()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Insert(T element)
        {
            throw new NotImplementedException();
        }

        public bool Search(T element)
        {
            throw new NotImplementedException();
        }

        public void InOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void PreOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void PostOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}