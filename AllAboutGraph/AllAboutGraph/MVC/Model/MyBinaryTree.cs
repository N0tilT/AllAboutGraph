using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class BinaryNode<T>
    {
        public T Data { get; set; }
        public BinaryNode<T> Left { get; set; }
        public BinaryNode<T> Right { get; set; }
        public BinaryNode<T> Parent { get; set; }

        public BinaryNode()
        {
        }

        public BinaryNode(T data)
        {
            Data = data;
        }

        public BinaryNode(T data, BinaryNode<T> parent) : this(data)
        {
            Parent = parent;
        }

        public BinaryNode(T data, BinaryNode<T> parent, BinaryNode<T> left) : this(data, parent)
        {
            Left = left;
        }

        public BinaryNode(T data, BinaryNode<T> parent, BinaryNode<T> left, BinaryNode<T> right) : this(data, parent, left)
        {
            Right = right;
        }

        public override string ToString()
        {
            string node = "";
            node += Data + " " + "\n" + Left?.ToString() + " - " + Right?.ToString();
            return node;
        }



    }
    public class MyBinaryTree<T> where T:IComparable<T>
    {
        public BinaryNode<T> Root { get; set; }
        public int Count { get; set; }

        public MyBinaryTree()
        {
            Count = 0;
        }

        public void Add(BinaryNode<T> item)
        {
            Count++;

        }

        public BinaryNode<T> Find(BinaryNode<T> root,T item)
        {
            bool flag = false;
            if(root != null)
            {
                BinaryNode<T> currentNode = root;
                if(currentNode.Data.Equals(item))
                {
                    flag = true;
                    return currentNode;
                }
                else
                {
                    if(currentNode.Data.CompareTo(item) > 0)
                    {
                        return Find(currentNode.Left,item);
                    }
                    else
                    {
                        return Find(currentNode.Right, item);
                    }
                }
            }
            return null;
        }

        public BinaryNode<T> Min(BinaryNode<T> root)
        {
            BinaryNode<T> currentNode = root;
            while(currentNode.Left != null) 
            {
                currentNode = currentNode.Left;
            }
            return currentNode;
        }

        public BinaryNode<T> Max(BinaryNode<T> root)
        {
            BinaryNode<T> currentNode = root;
            while (currentNode.Right != null)
            {
                currentNode = currentNode.Right;
            }
            return currentNode;
        }

    }
}
