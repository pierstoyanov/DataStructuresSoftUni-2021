namespace _03.PriorityQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> heap;

        public PriorityQueue()
        {
            heap = new List<T>();
        }

        public int Size { get { return heap.Count; } }

        public T Dequeue()
        {
            ValidateNotEmpty();
            //take first element
            T result = heap[0];
            //swap first and last element & remove last
            Swap(0, Size - 1);
            heap.RemoveAt(Size - 1);

            HeapifyDown(0);

            return result;
        }

        public void Add(T element)
        {
            heap.Add(element);
            Heapify(heap.Count - 1);
        }

        private void Heapify(int index)
        {
            if (index == 0)
                return;

            int parentIndex = (index - 1) / 2;
            // int parentIndex = GetParentIndex();

            if (heap[index].CompareTo(heap[parentIndex]) > 0)
            {
                // T temp = heap[index];
                // heap[index] = heap[parentIndex];
                // heap[parentIndex] = temp;
                Swap(index, parentIndex);
                Heapify(parentIndex);
            }
        }

        private void HeapifyDown(int index)
        {

            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int maxChildIndex = leftChildIndex;

            if (leftChildIndex >= heap.Count)
                return;

            if (rightChildIndex < heap.Count && heap[leftChildIndex].CompareTo(heap[rightChildIndex]) < 0)
            {
                maxChildIndex = rightChildIndex;
            }

            if (heap[index].CompareTo(heap[maxChildIndex]) < 0)
            {
                Swap(index, maxChildIndex);
                HeapifyDown(maxChildIndex);
            }
        }

        public T Peek()
        {
            ValidateNotEmpty();
            return heap[0];
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private void Swap(int indexOne, int indexTwo)
        {
            ///Swap items at index One and index Two
            ValidateIndex(indexOne, indexTwo);

            T temp = heap[indexOne];
            heap[indexOne] = heap[indexTwo];
            heap[indexTwo] = temp;
        }


        private void ValidateNotEmpty()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException();
        }

        private void ValidateIndex(params int[] indexes)
        {
            foreach (int index in indexes)
            {
                if (index < 0 || index > heap.Count - 1)
                    throw new InvalidOperationException();
            }
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