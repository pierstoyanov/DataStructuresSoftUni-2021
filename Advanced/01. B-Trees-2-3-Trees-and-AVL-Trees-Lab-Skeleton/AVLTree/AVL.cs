namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        private Node<T> root { get; set; }

        public Node<T> Root { get { return this.root; } set { this.root = value; } }

        public bool Contains(T item)
        {
            var node = this.Search(this.Root, item);
            return node != null;
        }

        public void Insert(T item)
        {
            this.Root = this.Insert(this.Root, item);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
        }

        private Node<T> Insert(Node<T> node, T item)
        {
            if (node == null)
            {
                return new Node<T>(item);
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                node.Left = this.Insert(node.Left, item);
            }
            else if (cmp > 0)
            {
                node.Right = this.Insert(node.Right, item);
            }

            return node;
        }

        private Node<T> Search(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                return Search(node.Left, item);
            }
            else if (cmp > 0)
            {
                return Search(node.Right, item);
            }

            return node;
        }

        private void EachInOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private static int Height(Node<T> node)
        {
            if (node == null)
                return 0;
            return node.Height;
        }

        private static void UpdateHeight(Node<T> node)
        {
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        private static Node<T> RotateLeft(Node<T> node)
        {
            //store node to be movied up
            var temp = node.Right;
            //exchange RN with RN's left child
            node.Right = node.Right.Left;
            //make initial node left child 
            temp.Left = node;

            UpdateHeight(node);
            //UpdateHeight(temp);

            return temp;
        }

        private static Node<T> RotateRight(Node<T> node)
        {
            //store node to be movied up
            var temp = node.Left;
            //exchange LN with LN's right child
            node.Left = node.Left.Right;
            //make initial node right child 
            temp.Right = node;

            UpdateHeight(node);
            //UpdateHeight(temp);

            return temp;
        }

        private static Node<T> Balance(Node<T> node)
        {
            int BF = Height(node.Left) - Height(node.Right);

            if (BF < -1)
            {
                BF = Height(node.Right.Left) - Height(node.Right.Right);
                if (BF <= 0)
                    return RotateLeft(node);
                else
                {
                    node.Right = RotateRight(node.Right);
                    return RotateLeft(node);
                }
            }
            else if (BF > 1)
            {
                BF = Height(node.Left.Left) - Height(node.Left.Right);
                if (BF >= 0)
                    return RotateRight(node);
                else
                {
                    node.Left = RotateLeft(node.Left);
                    return RotateRight(node);
                }
            }

            return node;
        }

    }
}