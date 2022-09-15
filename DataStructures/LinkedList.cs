using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LinkedLists
{
    public class LogicList<T> : IEnumerable<T> where T : IComparable<T>
    {
        Node first = null;
        Node last = null;
        public Node First { get => first; }
        public Node Last { get => last; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Node tmp = first;

            while (tmp != null)
            {
                sb.Append($"{tmp.Value} ");
                tmp = tmp.Next;
            }
            return sb.ToString();
        }
        public void AddFirst(T value) // O(1)
        {
            Node node = new Node(value);
            if (first == null)
            {
                first = node;
                last = node;
            }
            else
            {
                first.Previous = node;
                node.Next = first;
                first = node;
            }
        }
        private void AddLast(T value) // O(1)
        {
            if (first == null)
            {
                AddFirst(value);
                return;
            }
            Node n = new Node(value);
            last.Next = n;
            n.Previous = last;
            last = n;
        }
        private void RemoveFirst() // O(1)
        {
            if (first == null)
                return;
            first = first.Next;
            first.Previous = null;
            if (first == null)
            {
                last = null;
            }
        }
        public void RemoveLast()//O(1) 
        {
            if (this.first != null)
            {
                if (this.first == last)
                {
                    this.last = null;
                    this.first = null;
                }
                else
                {
                    last = last.Previous;
                    last.Next = null;
                }
            }
        }
        private bool GetAt(int index, out T value)
        {
            Node tmp = first;
            for (int i = 0; i <= index; i++)
            {
                if (i == index)
                {
                    value = tmp.Value;
                    return true;
                }
                tmp = tmp.Next;
                if (tmp == null)
                {
                    value = default;
                    return false;
                }
            }
            value = tmp.Value;
            return false;
        }
        private bool AddAt(int index, T change)
        {
            Node tmp = first;
            Node node = new Node(change);
            for (int i = 0; i <= index; i++)
            {
                if (i == index)
                {
                    node.Next = tmp.Next;
                    tmp.Next = node;
                    return true;
                }
                tmp = tmp.Next;
                if (tmp == null)
                {
                    return false;
                }
            }
            return true;
        }
        public void RellocateToStart(Node node)
        {
            if (node == first)
            {
                return;
            }
            if (node == last)
            {
                last = node.Previous;
                AddFirst(node.Value);
                return;
            }
            if (node == null) return;
            node.Next.Previous = node.Previous;
            node.Previous.Next = node.Next;
            AddFirst(node.Value);
            return;
        }
        public void DeleteNode(T key)
        {
            Node temp = first, prev = null;
            if (temp != null && temp.Value.CompareTo(key) == 0)
            {
                first = temp.Next;
                return;
            }
            while (temp != null && temp.Value.CompareTo(key) != 0)
            {
                prev = temp;
                temp = temp.Next;
            }
            if (temp == null)
                return;
            prev.Next = temp.Next;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var node = first;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public class Node
        {
            public T Value;
            public Node Next;
            public Node Previous;
            public Node(T value)
            {
                this.Value = value;
                Next = null;
                Previous = null;
            }
        }
    }
}
