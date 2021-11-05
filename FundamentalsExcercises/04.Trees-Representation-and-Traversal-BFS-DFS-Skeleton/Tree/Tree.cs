namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> children;

        private bool IsRootDeleted { get; set; }


        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this.children = new List<Tree<T>>();
            this.IsRootDeleted = false; 
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (Tree<T> child in children)
            {
                child.Parent = this;
                this.children.Add(child);
            }
        }

        public T Value { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => this.children.AsReadOnly();

        public ICollection<T> OrderBfs()
        {
            List<T> result = new List<T>();

            if (IsRootDeleted)
                return result;

            Queue<Tree<T>> queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> node = queue.Dequeue();
                result.Add(node.Value);
                foreach (Tree<T> child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            List<T> result = new List<T>();

            if (IsRootDeleted)
                return result;

            this.Dfs(this, result);

            return result;
        }

        private void Dfs(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree.Children)
                this.Dfs(child, result);

            result.Add(tree.Value);
        }

        public ICollection<T> DfsIterative()
        {
            List<T> result = new List<T>();
            if (IsRootDeleted)
                return result;

            //create stack & add current node to stack 
            Stack<Tree<T>> stack = new Stack<Tree<T>>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                Tree<T> node = stack.Pop();
                foreach (Tree<T> child in node.Children)
                {
                    stack.Push(child);
                }
                result.Add(node.Value);
            }

            return result;
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            if (FindNode(this, parentKey) == null)
                throw new ArgumentNullException(); 

            var node = FindNode(this, parentKey);
            node.children.Add(child);
        }

        private Tree<T> FindNode(Tree<T> root, T searched)
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Value.Equals(searched))
                    return node;

                foreach (Tree<T> child in node.Children)
                    queue.Enqueue(child);

            }
            return null;
        }

        public void RemoveNode(T nodeKey)
        {
            var node = FindNode(this, nodeKey);
            if (node == null)
                throw new ArgumentNullException();

            foreach (var item in node.children)
            {
                item.Parent = null;
            }

            node.children.Clear();
            node.Value = default(T);

            Tree<T> searchedParent = node.Parent;

            if (searchedParent == null)
                IsRootDeleted = true;
            else
                searchedParent.children.Remove(node);

        }

        public void Swap(T firstKey, T secondKey)
        {
            var nodeFirst = FindNode(this, firstKey);
            var nodeSecond = FindNode(this, secondKey);
            if (nodeFirst == null || nodeSecond == null)
                throw new ArgumentNullException();

            var firstParent = nodeFirst.Parent;
            var secondParent = nodeSecond.Parent;

            if (firstParent == null)
            {
                var tempVal = nodeFirst.Value;
                var tempChildren = nodeFirst.Children;
                nodeFirst.Value = nodeSecond.Value;
                nodeSecond.Value = tempVal;

                nodeFirst.children.Clear();
                foreach (var child in nodeSecond.children)
                {
                    nodeFirst.AddChild(secondKey, child);
                }

                nodeSecond.children.Clear();
                foreach (var child in tempChildren)
                {
                    nodeSecond.AddChild(tempVal, child);
                }
            }

            else if (secondParent == null)
            {
                var tempVal = nodeSecond.Value;
                var tempChildren = nodeSecond.Children;
                nodeSecond.Value = nodeFirst.Value;
                nodeFirst.Value = tempVal;

                nodeSecond.children.Clear();
                foreach (var child in nodeFirst.children)
                {
                    nodeSecond.AddChild(firstKey, child);
                }

                nodeFirst.children.Clear();
                foreach (var child in tempChildren)
                {
                    nodeFirst.AddChild(secondKey, child);
                }
            }

            else
            {
                //swap parents
                nodeFirst.Parent = secondParent;
                nodeSecond.Parent = firstParent;

                //swap children
                int firstIndex = firstParent.children.IndexOf(nodeFirst);
                int secondIndex = secondParent.children.IndexOf(nodeSecond);
                firstParent.children[firstIndex] = nodeSecond;
                secondParent.children[secondIndex] = nodeFirst;
            }
        }
    }
}
