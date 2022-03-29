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
        //private ILink current = null;

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
            var temp = history.First;
            history.RemoveFirst();
            return temp.Value;
        }

        public ILink DeleteLast()
        {
            var temp = history.Last;
            history.RemoveLast();
            return temp.Value;
        }

        public ILink GetByUrl(string url)
        {
            return history.Contains(l => l.Url == link);
        }

        public ILink LastVisited()
        {
            return history.Last.Value;
        }

        public void Open(ILink link)
        {
            history.AddLast(link);
        }

        public int RemoveLinks(string url)
        {
            var count = 0;

            foreach (ILink link in history)
            {
                if (link.Url == url)
                {
                    history.Remove(link);
                    count++;
                }
            }

            return count != 0 ? count : throw new InvalidOperationException();
        }

        public ILink[] ToArray()
        {
            return history.Reverse().ToArray();
        }

        public List<ILink> ToList()
        {
            return history.Reverse().ToList();
        }

        public string ViewHistory()
        {
            var result = new StringBuilder();

            foreach (var link in history)
            {
                result.Append(link.ToString());
                result.AppendLine();
            }

            return result.ToString().Trim();
        }
    }
}
