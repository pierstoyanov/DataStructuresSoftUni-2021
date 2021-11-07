using System;
using System.Collections.Generic;
using System.Text;

namespace _01.BinaryTree
{
    class Node<T>
    {
        public T Value { get; set; }
        public Node<T> LeftChild { get; set; }
        public Node<T> RightChild { get; set; }
    }
}
