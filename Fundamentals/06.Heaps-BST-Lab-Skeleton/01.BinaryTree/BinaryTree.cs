namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value,
            IAbstractBinaryTree<T> leftChild,
            IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }
        public IAbstractBinaryTree<T> LeftChild { get; private set; }
        public IAbstractBinaryTree<T> RightChild { get; private set; }
        public IAbstractBinaryTree<T> Root { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            return DFSPreOrder(this, indent);
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();
            return DFSInOrderList(this, result);
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();
            return DFSPostOrderList(this, result);
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();
            return DFSPreOrderList(this, result);
        }

        public void ForEachInOrder(Action<T> action)
        {
            if (LeftChild != null)
            {
                this.LeftChild.ForEachInOrder(action);
            }

            action.Invoke(this.Value);

            if (RightChild != null)
            {
                this.RightChild.ForEachInOrder(action);
            }
        }

        public StringBuilder PreOrderDFS (IAbstractBinaryTree<T> tree, StringBuilder result, int indent)
        {
            result.Append(new string(' ', indent))
                .Append(tree.Value)
                .Append(Environment.NewLine);

            if (tree.LeftChild != null)
                PreOrderDFS(tree.LeftChild, result, indent + 2);

            if (tree.RightChild != null)
                PreOrderDFS(tree.RightChild, result, indent + 2);

            return result;
        }
        public string DFSPreOrder(IAbstractBinaryTree<T> tree, int indent)
        {
            string result = $"{new string(' ', indent)}{tree.Value}\r\n";

            if (tree.LeftChild != null)
                result += DFSPreOrder(tree.LeftChild, indent + 2);

            if (tree.RightChild != null)
                result += DFSPreOrder(tree.RightChild, indent + 2);

            return result;
        }

        public string DFSInOrder(IAbstractBinaryTree<T> tree, int indent)
        {
            string result = "";

            if (tree.LeftChild != null)
                result += DFSInOrder(tree.LeftChild, indent + 2);

            result += $"{new string(' ', indent)}{tree.Value}\r\n";

            if (tree.RightChild != null)
                result += DFSInOrder(tree.RightChild, indent + 2);

            return result;
        }

        public string DFSPostOrder(IAbstractBinaryTree<T> tree, int indent)
        {
            string result = "";

            if (tree.LeftChild != null)
                result += DFSPostOrder(tree.LeftChild, indent + 2);

            if (tree.RightChild != null)
                result += DFSPostOrder(tree.RightChild, indent + 2);

            result += $"{new string(' ', indent)}{tree.Value}\r\n";
            
            return result;
        }

        public List<IAbstractBinaryTree<T>> DFSPreOrderList(IAbstractBinaryTree<T> tree, List<IAbstractBinaryTree<T>> result)
        {
            result.Add(tree);

            if (tree.LeftChild != null)
                DFSPreOrderList(tree.LeftChild, result);

            if (tree.RightChild != null)
                DFSPreOrderList(tree.RightChild,result);

            return result;
        }

        public List<IAbstractBinaryTree<T>> DFSInOrderList(IAbstractBinaryTree<T> tree, List<IAbstractBinaryTree<T>> result)
        {
            if (tree.LeftChild != null)
                DFSInOrderList(tree.LeftChild, result);

            result.Add(tree);

            if (tree.RightChild != null)
                DFSInOrderList(tree.RightChild, result);

            return result;
        }

        public List<IAbstractBinaryTree<T>> DFSPostOrderList(IAbstractBinaryTree<T> tree, List<IAbstractBinaryTree<T>> result)
        {
            if (tree.LeftChild != null)
                DFSPostOrderList(tree.LeftChild, result);

            if (tree.RightChild != null)
                DFSPostOrderList(tree.RightChild, result);

            result.Add(tree);

            return result;
        }

    }
}
