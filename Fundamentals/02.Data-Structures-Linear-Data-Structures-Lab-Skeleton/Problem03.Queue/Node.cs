namespace Problem03.Queue
{
    public class Node<T>
    {
        public T Item { get; set; }
        public Node(T value) { Item = value; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }
    }
}