namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTreeCool<T> 
        where T : IComparable<T>
    {
        public BinarySearchTreeCool()
        {
        }

        public BinarySearchTreeCool(Node<T> root = null)
        {
            Root = root;
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            return ContainsFromNode(element, Root);
        }

        public bool ContainsFromNode(T element, Node<T> node)
        {
            if (node == null)
                return false;

            if (node.Value.CompareTo(element) == 0)
                return true;
      
            if (node.Value.CompareTo(element) > 0)
            {
                return false || ContainsFromNode(element, node.LeftChild);
            }
            else
            {
                return false || ContainsFromNode(element, node.RightChild);
            }
        }
        public void Insert(T element)
        {
            InsertAt(element, Root);
        }

        public void InsertAt(T element, Node<T> node)
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
                    node.LeftChild = new Node<T>(element);
                    return;
                }
                InsertAt(element, node.LeftChild);
            }
            else
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new Node<T>(element);
                    return;
                }
                InsertAt(element, node.RightChild);
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            throw new NotImplementedException();
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
