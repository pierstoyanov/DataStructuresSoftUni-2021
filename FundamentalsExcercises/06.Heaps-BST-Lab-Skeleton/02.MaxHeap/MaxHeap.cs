namespace _02.MaxHeap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> heap;

        public MaxHeap()
        {
            heap = new List<T>();
        }

        public int Size { get { return heap.Count; } }

        public T GetMax()
        {
            return heap[0];
        }

        public void Add(T element)
        {
            heap.Add(element);
            Heapify(heap.Count - 1);
        }

        private void Heapify(int index)
        {
            //base case
            if (index == 0) return;

            int parentIndex = (index - 1) / 2;
            //swap index with parent index
            if (heap[index].CompareTo(heap[parentIndex]) > 0)
            {
                T temp = heap[index];
                heap[index] = heap[parentIndex];
                heap[parentIndex] = temp;
                Heapify(parentIndex);
            }
        }

        public T Peek()
        {
            VerifyNotEmpty();
            return GetMax();
        }

        private void VerifyNotEmpty()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException();
        }

        public string DFSInOrder(int index, int indent)
        {
            string result = "";
            int leftChild = 2 * index + 1;
            int rightChild = 2 * indent + 2;

            if (leftChild < heap.Count)
            {
                result += DFSInOrder(leftChild, indent + 2);
            }

            result += $"{new string(' ', indent)}{heap[index]}\r\n";

            if (rightChild < heap.Count)
            {
                result += DFSInOrder(rightChild, indent + 2);
            }

            return result;
        }
    }
}