using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public T Data { get; set; }

        public MyBinaryTree<T> Right { get; set; }
        public MyBinaryTree<T> Left { get; set; }


        public MyBinaryTree(T data)
        {
            Count = 0;
            Data = data;
        }

        public void Add(MyBinaryTree<T> tree)
        {
            if (tree.Data.CompareTo(Data)<0)
            {
                if (Left == null)
                {
                    Left = tree;
                }
                else
                {
                    Left.Add(tree);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = tree;
                }
                else
                {
                    Right.Add(tree);
                }
            }
        }

        private enum Direction
        {
            Left = 1,
            Right = 2,
        }

        public void Delete(BinaryNode<T> tree, BinaryNode<T> item)
        {
            if (tree.Left == null && tree.Right == null)
            {
                if (item.Data.CompareTo(item.Parent.Data) > 0)
                {
                    item.Parent.Right = null;
                }
                else
                {
                    item.Parent.Left = null;
                }
                item.Parent = null;
            }

            BinaryNode<T> currentNode = tree;
            Direction direction = Direction.Left;

            while ((item.Data.CompareTo(currentNode.Data)>0) && (currentNode.Right !=null) ||
                (item.Data.CompareTo(currentNode.Data)<0) && (currentNode.Left != null))
            {
                if (item.Data.CompareTo(currentNode.Data) > 0)
                {
                    currentNode = currentNode.Right;
                    direction = Direction.Right;
                }
                else
                {
                    currentNode = currentNode.Left;
                    direction = Direction.Left;
                }
            }

            if(currentNode != null)
            {
                if(currentNode.Left != null && currentNode.Right != null)
                {
                    BinaryNode<T> current2 = currentNode.Right;
                    while(current2.Left != null)
                    {
                        current2 = current2.Left;
                    }

                    currentNode.Data = current2.Data;
                    if(current2.Parent != currentNode)
                    {
                        current2.Parent.Left = current2.Right;
                    }
                    else
                    {
                        current2.Parent.Right = current2.Right;
                    }

                    current2 = null;
                }
                else
                {
                    if(currentNode.Left == null)
                    {
                        if(currentNode.Parent != null)
                        {
                            if(direction == Direction.Left)
                            {
                                currentNode.Parent.Left = currentNode.Left;
                            }
                            else
                            {
                                currentNode.Parent.Right = currentNode.Right;
                            }
                        }
                        else
                        {
                            Root = currentNode.Right;
                        }
                    }

                    if (currentNode.Right == null)
                    {
                        if (currentNode.Parent != null)
                        {
                            if (direction == Direction.Left)
                            {
                                currentNode.Parent.Left = currentNode.Left;
                            }
                            else
                            {
                                currentNode.Parent.Right = currentNode.Right;
                            }
                        }
                        else
                        {
                            Root = currentNode.Left;
                        }
                    }
                }
                currentNode = null;
            }
        }

        public void Clear(BinaryNode<T> root)
        {
            if (root != null)
            {
                Clear(root.Left);
                Clear(root.Right);
                root = null;
                Root = null;
            }
        }

        public BinaryNode<T> Find(BinaryNode<T> root,T item)
        {
            if(root != null)
            {
                BinaryNode<T> currentNode = root;
                if(currentNode.Data.Equals(item))
                {
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

        public T[] Transform(List<T> elements = null)
        {
            if(elements == null)
            {
                elements = new List<T> ();
            }

            if (Left != null)
            {
                Left.Transform(elements);
            }

            elements.Add(Data);

            if(Right != null)
            {
                Right.Transform(elements);
            }

            return elements.ToArray();
        }


    }
}
