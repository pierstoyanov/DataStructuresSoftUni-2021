namespace _02.DOM
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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
            foreach (var child in current.Children)
            {
                GetElementsByType(type, child, result);
                
                if (child.Type == type)
                {
                    result.Add(child);
                } 
            }
          
            return result;
        }

        public bool Contains(IHtmlElement htmlElement)
        {
            return FindElement(htmlElement) != null;
        }

        public void InsertFirst(IHtmlElement parent, IHtmlElement child)
        {
            var p = FindElement(parent);
            ElementExists(p);
            p.Children.Insert(0, child);
            child.Parent = p;
        }

        public void InsertLast(IHtmlElement parent, IHtmlElement child)
        {
            var p = FindElement(parent);
            ElementExists(p);
            p.Children.Add(child);
            child.Parent = p;
        }

        public void Remove(IHtmlElement htmlElement)
        {
            var node = FindElement(htmlElement);
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
            var node = FindElement(htmlElement);

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
            var node = FindElement(htmlElement);
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

                if (current.Attributes.ContainsKey("id"))
                {
                    {
                        if (current.Attributes["id"] == idValue)
                        {
                            return current;
                        }
                    }
                }
            }
            return null;
        }

        override public string ToString()
        {
            StringBuilder r = new StringBuilder();
            return ToString(r, Root, 0).ToString();
        }

        public StringBuilder ToString(StringBuilder result, IHtmlElement currentNode, int indentation)
        {
            result.Append(new string(' ', indentation))
                .Append(currentNode.Type)
                .Append(Environment.NewLine);

            foreach (var child in currentNode.Children)
            {
                ToString(result, child, indentation + 2);
            }

            return result;
        }

        private void ElementExists(IHtmlElement parent)
        {
            if (parent == null)
                throw new InvalidOperationException();
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

        private IHtmlElement FindElement(IHtmlElement node)
        {

            var que = new Queue<IHtmlElement>();
            que.Enqueue(Root);

            while (que.Count != 0)
            {
                var current = que.Dequeue();

                if (current == node)
                {
                    return current;
                }

                foreach (var child in current.Children)
                {
                    que.Enqueue(child);
                }
            }

            return null;
        }
    }
}
