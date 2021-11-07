namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree() { }

        public BinarySearchTree(Node<T> root)
        {
            Root = root;
            LeftChild = root.LeftChild;
            RightChild = root.RightChild;
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            return Contains(element, Root);
        }

        public bool Contains(T element, Node<T> node)
        {
            //exit recursion
            if (node == null)
                return false;

            if (node.Value.CompareTo(element) == 0)
                return true;
            // if left/right > element retrun false or evaluate
            if (node.Value.CompareTo(element) > 0)
            {
                return false || Contains(element, node.LeftChild);
            }
            else
            {
                return false || Contains(element, node.RightChild);
            }
        }

        public void Insert(T value)
        {
            Insert(value, this.Root);
        }

        public void Insert(T element, Node<T> node)
        {
            if (node == null)
            {
                node = new Node<T>(element);
                Root = node;
                return;
            }

            if (node.Value.CompareTo(element) > 0)
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = new Node<T>(element, null, null);
                    return;
                }
                Insert(element, node.LeftChild);
            }
            else
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new Node<T>(element, null, null);
                    return;
                }
                Insert(element, node.RightChild);
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            return Search(element, this.Root);
        }

        public IAbstractBinarySearchTree<T> Search(T element, Node<T> node)
        {
            if (node == null)
                return null;

            if (node.Value.CompareTo(element) == 0)
                return new BinarySearchTree<T>(node);
            // if left/right > element retrun false or evaluate
            if (node.Value.CompareTo(element) > 0)
            {
                return Search(element, node.LeftChild);
            }
            else
            {
                return Search(element, node.RightChild);
            }
        }

        public string DFSInOrder(Node<T> node, int indent)
        {
            string result = "";

            if (node.LeftChild != null)
            {
                result += DFSInOrder(node.LeftChild, indent + 2);
            }

            result += $"{new string(' ', indent)}{node.Value}\r\n";

            if (node.RightChild != null)
            {
                result += DFSInOrder(node.RightChild, indent + 2);
            }

            return result;
        }
    }
}
