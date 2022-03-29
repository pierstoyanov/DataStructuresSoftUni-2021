namespace _02.DOM
{
    using System;
    using System.Collections.Generic;
    using _02.DOM.Interfaces;
    using _02.DOM.Models;

    public class DocumentObjectModel : IDocument
    {
        public DocumentObjectModel(IHtmlElement root)
        {
            this.Root = root;
        }

        public DocumentObjectModel()
        {
            Root = new HtmlElement(
                ElementType.Document,
                    new HtmlElement(
                        ElementType.Html,
                            new HtmlElement(ElementType.Head),
                            new HtmlElement(ElementType.Body)
                ));
        }

        public IHtmlElement Root { get; private set; }

        public IHtmlElement GetElementByType(ElementType type)
        {
            var que = new Queue<IHtmlElement>();
            que.Enqueue(Root);

            while (que.Count != 0)
            {
                var current = que.Dequeue();

                if (current.Type == type)
                {
                    return current;
                }
                else
                {
                    foreach (var child in current.Children)
                    {
                        que.Enqueue(child);
                    }
                }
            }
            return null;
        }

        public List<IHtmlElement> GetElementsByType(ElementType type)
        {
            return GetElementsByType(type, Root, new List<IHtmlElement>());
        }

        public List<IHtmlElement> GetElementsByType(ElementType type, IHtmlElement current, List<IHtmlElement> result)
        {
            if (current.Children == null)
            {
                if (current.Type == type)
                {
                    result.Add(current);
                }
                return result;
            }

            foreach (var child in current.Children)
            {
                if (child.Type == type)
                {
                    result.Add(child);
                }
                GetElementsByType(type, child, result);
            }
            return result;
        }

        public bool Contains(IHtmlElement htmlElement)
        {
            var element = FindElement(htmlElement, Root);
            ElementExists(element);
            return element == null;
        }

        public void InsertFirst(IHtmlElement parent, IHtmlElement child)
        {
            var node = FindElement(parent, Root);

            node.Children.Insert(0, child);
        }

        public void InsertLast(IHtmlElement parent, IHtmlElement child)
        {
            var node = FindElement(parent, Root);
            ElementExists(node);
            node.Children.Add(child);
        }

        public void Remove(IHtmlElement htmlElement)
        {
            var node = FindElement(htmlElement, Root);
            ElementExists(node);
            var parent = node.Parent;
            parent.Children.Remove(node);
        }

        public void RemoveAll(ElementType elementType)
        {
            var que = new Queue<IHtmlElement>();
            que.Enqueue(Root);

            while (que.Count != 0)
            {
                var current = que.Dequeue();

                if (current.Type == elementType)
                {
                    Remove(current);
                }
                else
                {
                    foreach (var child in current.Children)
                    {
                        que.Enqueue(child);
                    }
                }
            }
        }

        public bool AddAttribute(string attrKey, string attrValue, IHtmlElement htmlElement)
        {
            var node = FindElement(htmlElement, Root);
            ElementExists(node);
            if (!node.Attributes.ContainsKey(attrKey))
            {
                node.Attributes.Add(attrKey, attrValue);
                return true;
            }

            return false;
        }

        public bool RemoveAttribute(string attrKey, IHtmlElement htmlElement)
        {
            var node = FindElement(htmlElement, Root);
            ElementExists(node);
            if (node.Attributes.ContainsKey(attrKey))
            {
                node.Attributes.Remove(attrKey);
                return true;
            }

            return false;
        }

        public IHtmlElement GetElementById(string idValue)
        {
            var que = new Queue<IHtmlElement>();
            que.Enqueue(Root);

            while (que.Count != 0)
            {
                var current = que.Dequeue();
                foreach (var child in current.Children)
                {
                    que.Enqueue(child);
                }
                if (current.Attributes["id"] == idValue)
                {
                    return current;
                }
            }
            return null;
        }

        private void ElementExists(IHtmlElement parent)
        {
            if (parent == null)
            {
                throw new InvalidOperationException();
            }
        }

        private IHtmlElement FindElement(IHtmlElement node, IHtmlElement current)
        {

            if (node == current)
            {
                return node;
            }
            foreach (var child in current.Children)
            {
                return FindElement(node, child);
            }
            return null;
        }

        private void DFS(IHtmlElement el)
        {
            if (el.Children == null)
            {
                return;
            }
            foreach (var ch in el.Children)
            {
                DFS(ch);
            }
        }

        private void BFS(IHtmlElement el)
        {
            var que = new Queue<IHtmlElement>();
            que.Enqueue(el);

            while (que.Count != 0)
            {
                var current = que.Dequeue();
                foreach (var child in current.Children)
                {
                    que.Enqueue(child);
                }
            }
        }
    }
}
