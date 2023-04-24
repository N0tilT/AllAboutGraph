using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Parent { get; set; }
        public Node<T> LeftChild { get; set; }
        public Node<T> RightSibling { get; set;}

        public Node() { }

        public Node(T data)
        {
            Data = data;
        }

        public Node(T data, Node<T> parent) : this(data)
        {
            Parent = parent;
        }

        public Node(T data,Node<T> parent,Node<T> leftChild) : this(data, parent)
        {
            LeftChild = leftChild;
        }

        public Node(T data, Node<T> parent, Node<T> leftChild, Node<T> rightSibling) : this(data, parent, leftChild)
        {
            RightSibling = rightSibling;
        }

        public override string ToString()
        {
            string node = "";
            node += Data + " " + RightSibling?.ToString() + "\n" + LeftChild?.ToString();
            return node;
        }

    }

    public class MyTree<T>
    {
        public Node<T> Root { get; set; }
        public int Count { get; set; }
        
        public MyTree() 
        {
            Count = 0;
        }

        public void Add(T item, Node<T> tree, bool isChild) 
        {
            if(tree.Parent == null && Root==null)
            {
                Root = new Node<T>(item);
                Count++;
                return;
            }

            if (isChild)
            {
                tree.LeftChild = new Node<T>(item)
                {
                    Parent = tree
                };
                Count++;
                return;
            }
            else if (!isChild)
            {
                tree.RightSibling = new Node<T>(item)
                {
                    Parent = tree.Parent
                };
                Count++;
                return;
            }


        }

        public override string ToString()
        {
            string tree = "";
            if (Root != null)
            {
                tree += "\n" + Root.Data.ToString() + "\n";

                tree += "\n" + Root.LeftChild.ToString();
            }
            return tree;
        }

    }
}
