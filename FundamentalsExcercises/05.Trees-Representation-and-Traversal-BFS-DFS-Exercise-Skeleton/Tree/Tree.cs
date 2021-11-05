namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.children = new List<Tree<T>>();
            foreach (var child in children)
            {
                this.AddChild(child);
                child.AddParent(this);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this.children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            return GetAsString(0).Trim();
        }

        public string GetAsString(int indentation = 0)
        {
            var result = new string(' ', indentation) + this.Key + "\r\n";
            
            foreach (var child in this.Children)
            {
                result += child.GetAsString(indentation + 2);
            }

            return result;
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            Tree<T> deepestLeftmostNode = this;

            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(deepestLeftmostNode);

            while (queue.Count > 0)
            {
                deepestLeftmostNode = queue.Dequeue();
                var list = deepestLeftmostNode.Children.ToList();

                for (int i = list.Count - 1; i >= 0; i--)
                {
                    queue.Enqueue(list[i]);
                }
            }

            return deepestLeftmostNode;
        }

        public List<T> GetLeafKeys()
        {
            var leafNodes = this.GetLeafNodes();

            return leafNodes.Select(x => x.Key).ToList();
        }

        public List<Tree<T>> GetLeafNodes()
        {
            var leafNodes = new List<Tree<T>>();

            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Children.Count == 0)
                {
                    leafNodes.Add(node);
                }

                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return leafNodes.OrderBy(x => x.Key).ToList();          
        }

        public List<T> GetMiddleKeys()
        {
            List<T> middleNotes = new List<T>();

            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count !=0)
            {
                var current = queue.Dequeue();
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }

                if (current.Parent != null && current.Children.Count >= 1)
                {
                    middleNotes.Add(current.Key);
                }
            }

            return middleNotes;
        }

        public List<T> GetLongestPath()
        {
            var currentNode = this.GetDeepestLeftomostNode();

            List<T> longestPath = new List<T>();

            while(currentNode.Parent != null)
            {
                longestPath.Add(currentNode.Key);
                currentNode = currentNode.Parent;
            }
            longestPath.Add(currentNode.Key);

            longestPath.Reverse();
            return longestPath;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<T>>();
            var currentSum = 0;
            this.PathsWithGivenSumDFS(this, ref currentSum, sum, result, new List<T>());

            return result;
        }

        public List<List<T>> PathsWithGivenSumFromLeafs(int sum)
        {
            var leafNodes = this.GetLeafNodes();
            var result = new List<List<T>>();

            foreach (var path in leafNodes)
            {
                var node = path;
                var currentSum = 0;
                var currentNodes = new List<T>();

                while (node != null)
                {
                    currentNodes.Add(node.Key);
                    currentSum += int.Parse(node.Key.ToString());
                    node = node.Parent;
                }

                if (currentSum == sum)
                {
                    currentNodes.Reverse();
                    result.Add(currentNodes);
                }
            }

            return result;
        }

        private void PathsWithGivenSumDFS(
            Tree<T> node,
            ref int currentSum,
            int targetSum,
            List<List<T>> allPaths,
            List<T> currentPathValues)
        {
            currentSum += Convert.ToInt32(node.Key);
            currentPathValues.Add(node.Key);
            foreach (var child in node.Children)
            {
                this.PathsWithGivenSumDFS(child, ref currentSum, targetSum, allPaths, currentPathValues);
            }

            if (currentSum == targetSum)
            {
                allPaths.Add(new List<T>(currentPathValues));
            }

            currentSum -= Convert.ToInt32(node.Key);
            currentPathValues.RemoveAt(currentPathValues.Count - 1);
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var roots = new List<Tree<T>>();

            SubTreeSumDFS(this, sum, roots);

            return roots;
        }

        private int SubTreeSumDFS(Tree<T> currentNode, int targetSum, List<Tree<T>> roots)
        {
            var currentSum = Convert.ToInt32(currentNode.Key);

            foreach (var child in currentNode.Children)
            {
                currentSum += SubTreeSumDFS(child, targetSum, roots);
            }

            if (currentSum == targetSum)
            {
                roots.Add(currentNode);
            }

            return currentSum;
        }
    }
}
