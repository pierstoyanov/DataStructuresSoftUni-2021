namespace _01.RedBlackTree
{
    using System;

    public class RedBlackTree<T> where T : IComparable
    {
        private const bool Red = true;
        private const bool Black = false;

        public class Node
        {
            public Node(T value)
            {
                Value = value;
                Color = Red;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }
        }

        public Node root;

        public RedBlackTree()
        {

        }

        private RedBlackTree(Node node)
        {
            PreOrderCopy(node);
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            Insert(node.Value);
            PreOrderCopy(node.Left);
            PreOrderCopy(node.Right);
        }

        public void EachInOrder(Action<T> action)
        {
            EachInOrder(root, action);
        }

        public void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            EachInOrder(node.Left, action);
            action(node.Value);
            EachInOrder(node.Right, action);
        }

        public RedBlackTree<T> Search(T element)
        {
            Node current = FindNode(element);

            return new RedBlackTree<T>(current);
        }

        private Node FindNode(T element)
        {
            var current = root;

            while (current != null)
            {
                if (IsLesser(element, current.Value))
                {
                    current = current.Left;
                }
                else if (IsLesser(current.Value, element))
                {
                    current = current.Right;
                }
                else { break; }
            }

            return current;
        }

        public void Insert(T value)
        {
            root = Insert(root, value);
            root.Color = Black;
        }

        public Node Insert(Node node, T value)
        {
            //empty tree
            if (node == null)
            {
                return new Node(value);
            }
            //new node is lesser
            if (IsLesser(value, node.Value))
            {
                node.Left = Insert(node.Left, value);
            }
            else //new node is greater
            {
                node.Right = Insert(node.Right, value);
            }

            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
            }
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }
            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColor(node);
            }

            return node;
        }

        public void Delete(T key)
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }

            root = Delete(root, key);

            if (root != null)
            {
                root.Color = Black;
            }
        }

        private Node Delete(Node node, T key)
        {
            if (IsLesser(key, node.Value))
            {
                if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                {
                    node = MoveRedLeft(node);
                }

                node.Left = Delete(node.Left, key);
            }
            else
            {
                //go left
                if (IsRed(node.Left))
                {
                    node = RotateRight(node);
                }

                if (AreEqual(node.Value,key) && node.Right == null)
                {
                    return null;
                }

                if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                {
                    node = MoveRedRight(node);
                }

                if (AreEqual(key, node.Value))
                {
                    node.Value = FindMinimalValueInSubTree(node.Right);
                    node.Right = DeleteMin(node.Right);
                }

                //go right

                else
                {
                    node.Right = Delete(node.Right, key);
                }
            }

            return FixUp(node);
        }

        private T FindMinimalValueInSubTree(Node node)
        {
            if (node.Left == null)
            {
                return node.Value;
            }

            return FindMinimalValueInSubTree(node.Left);
        }

        public void DeleteMin()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }

            root = DeleteMin(root);

            if (root != null)
            {
                root.Color = Black; 
            }
        }


        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return null; 
            }

            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
            {
                node = MoveRedLeft(node);
            }

            node.Left = DeleteMin(node.Left);

            return FixUp(node);
        }

        public void DeleteMax()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }

            root = DeleteMax(root);

            if (root != null)
            {
                root.Color = Black;
            }
        }

        private Node DeleteMax(Node node)
        {
            if (IsRed(node.Left))
            {
                node = RotateRight(node);
            }

            if (node.Right == null)
            {
                return null;
            }

            if (!IsRed(node.Right) && !IsRed(node.Left))
            {
                node = MoveRedRight(node);
            }

            node.Right = DeleteMax(node.Right);

            return FixUp(node);
        }

        private Node MoveRedLeft(Node node)
        {
            FlipColor(node);

            if (IsRed(node.Right.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
                FlipColor(node);
            }

            return node;
        }

        private Node FixUp(Node node)
        {
            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
            }

            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }

            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColor(node);
            }

            return node;
        }

        private Node MoveRedRight(Node node)
        {
            FlipColor(node);

            if (IsRed(node.Left.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
                FlipColor(node);
            }

            return node;
        }

        public int Count()
        {
            return Count(root);
        }

        public int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + Count(node.Left) + Count(node.Right);
        }

        private Node RotateLeft(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            temp.Color = temp.Left.Color;
            temp.Left.Color = Red;

            return temp;
        }

        private Node RotateRight(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Color = temp.Right.Color;
            temp.Right.Color = Red;

            return temp;
        }

        private void FlipColor(Node node)
        {
            //call only when both children are red
            node.Color = !node.Color;
            node.Left.Color = !node.Left.Color;
            node.Right.Color = !node.Right.Color;
        }

        private bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }
            return node.Color == Red;
        }

        private bool IsLesser(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }

        private bool AreEqual(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }
    }
}