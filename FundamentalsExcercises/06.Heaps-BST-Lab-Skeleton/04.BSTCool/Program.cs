namespace _04.BinarySearchTree
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            // Display the number of command line arguments.

            List<int> List = new List<int>();
            for (int i = 0; i < 50; i++)
            {
                List.Add(i);
            }

            BinarySearchTreeCool<int> bst = new BinarySearchTreeCool<int>();

            Insert(bst, 0, List.Count, List);

            Console.WriteLine(bst.DFSInOrder(bst.Root, 0));
        }

        private static void Insert(BinarySearchTreeCool<int> tree, int start, int end, List<int> list)
        {
            if (start >= end)
            {
                return;
            }

            var middle = (start + end) / 2;
            tree.InsertAt(list[middle], tree.Root);
            Insert(tree, start, middle - 1, list);
            Insert(tree, middle + 1, end, list);
        }

    }
}
