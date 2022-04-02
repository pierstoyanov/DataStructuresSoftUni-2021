namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private LinkedList<ILink> history = new LinkedList<ILink>();

        public int Size => history.Count;

        public void Clear()
        {
            history = new LinkedList<ILink>();
        }

        public bool Contains(ILink link)
        {
            return history.Contains(link);
        }

        public ILink DeleteFirst()
        {
            if (history.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var temp = history.First;
            history.RemoveFirst();
            return temp.Value;
        }

        public ILink DeleteLast()
        {
            if (history.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var temp = history.Last;
            history.RemoveLast();
            return temp.Value;
        }

        public ILink GetByUrl(string url)
        {
            foreach (var link in history)
            {
                if (link.Url == url)
                {
                    return link;
                }
            }
            return null;
        }

        public ILink LastVisited()
        {
            if (history.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return history.Last.Value;
        }

        public void Open(ILink link)
        {
            history.AddLast(link);
        }

        public int RemoveLinks(string url)
        {
            var count = 0;

            var node = history.First;
            while (node != null)
            {
                var next = node.Next;
                if (node.Value.Url.ToLower().Contains(url.ToLower()))
                {
                    count++;
                    history.Remove(node);
                }
                node = next;
            }

            return count != 0 ? count : throw new InvalidOperationException();
        }

        public ILink[] ToArray()
        {
            if (history.Count == 0)
            {
                return new ILink[] { };
            }
            return history.Reverse().ToArray();
        }

        public List<ILink> ToList()
        {
            if (history.Count == 0)
            {
                return new List<ILink> { };
            }
            return history.Reverse().ToList();
        }

        public string ViewHistory()
        {
            if (history.Count == 0)
            {
                return "Browser history is empty!";
            }

            var result = new StringBuilder();

            foreach (var link in history.Reverse())
            {
                result.Append(link.ToString());
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }
    }
}
