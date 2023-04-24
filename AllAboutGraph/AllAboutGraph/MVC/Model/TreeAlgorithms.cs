using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class TreeAlgorithms<T> where T : IComparable<T>
    {
        MyTree<T> Tree { get; set; }

        MyBinaryTree<T> BinaryTree { get; set; }

        public TreeAlgorithms(MyTree<T> tree, MyBinaryTree<T> binaryTree)
        {
            Tree = tree; 
            BinaryTree = binaryTree;
        }

        public List<Node<T>> PreOrder(Node<T> root)
        {
            List<Node<T>> order = new List<Node<T>>();
            if (root != null) {

                order.Add(root);

                order.AddRange(PreOrder(root.LeftChild));
                order.AddRange(PreOrder(root.RightSibling));
            }
            return order;
        }
        public List<Node<T>> InOrder(Node<T> root)
        {
            List<Node<T>> order = new List<Node<T>>();
            if (root != null)
            {
                if (root.LeftChild == null && root.RightSibling == null)
                {
                    order.Add(root);
                }
                else
                {
                    order.AddRange(InOrder(root.LeftChild));
                    order.Add(root);
                    order.AddRange(InOrder(root.RightSibling));
                }
            }

            return order;
        }
        public List<Node<T>> PostOrder(Node<T> root)
        {
            List<Node<T>> order = new List<Node<T>>();
            if (root != null)
            {
                order.AddRange(PreOrder(root.LeftChild));
                order.AddRange(PreOrder(root.RightSibling));

                order.Add(root);
            }
            return order;
        }

        public List<MyBinaryTree<T>> BinaryPreOrder(MyBinaryTree<T> root)
        {
            List<MyBinaryTree<T>> order = new List<MyBinaryTree<T>>();
            if (root != null)
            {
                order.Add(root);

                if(root.Left != null)
                {
                    order.AddRange(BinaryPreOrder(root.Left));
                }
                
                if(root.Right != null)
                {
                    order.AddRange(BinaryPreOrder(root.Right));
                }
                
            }
            return order;
        }

        public List<MyBinaryTree<T>> BinaryPreOrder_NotRecursive(MyBinaryTree<T> root)
        {
            List<MyBinaryTree<T>> order = new List<MyBinaryTree<T>>();
            
            Stack<MyBinaryTree<T>> stack = new Stack<MyBinaryTree<T>>();

            MyBinaryTree<T> currentNode = root;
            while (true)
            {
                if(currentNode != null)
                {
                    order.Add(currentNode);
                    stack.Push(currentNode);
                    currentNode = currentNode.Left;
                }
                else
                {
                    if(stack.Count == 0)
                    {
                        return order;
                    }
                    stack.Pop();
                    currentNode = currentNode.Right;
                }
            }
        }

        public int[] TournamentSort(int[] array)
        {
            int size = 128;

            int[] tree = new int[size * 2];
            int k;
            int i;

            tree = InitializeTree(array,tree,size);

            for (k = 17; k >= 2; k--)
            {
                i = tree[1]; 
                array[k] = tree[i]; 
                tree[i] = int.MinValue;

                tree = Readjust(tree, i);   
            }
            array[1] = tree[tree[1]];

            return array;
        }

        private int[] Readjust(int[] tree, int i)
        {
            int j;

            if ((i % 2) != 0) tree[i / 2] = i - 1;
            else tree[i / 2] = i + 1;

            i /= 2;
            while (i > 1)
            {
                if ((i % 2) != 0)
                {
                    j = i - 1;
                }
                else
                {
                    j = i + 1;
                }

                if (tree[tree[i]] > tree[tree[j]])
                {
                    tree[i / 2] = tree[i];
                }
                else
                {
                    tree[i / 2] = tree[j];
                }
                i /= 2;
            }

            return tree;
        }

        private int[] InitializeTree(int[] array, int[] tree, int size)
        {
            int j = 1, k;

            while (j < 18)
            {
                tree[size + j - 1] = array[j];
                j++;
            }

            for (j = size+18; j <= size-1; j++)
            {
                tree[j] = int.MinValue;
            }

            j = size;
            while (j <= 2 * size - 1)
            {
                if (tree[j] >= tree[j + 1])
                {
                    tree[j / 2] = j;
                }
                else 
                { 
                    tree[j / 2] = j + 1; 
                }
                j += 2;
            }

            k = size / 2;

            while (k > 1)
            {
                j = k;
                while (j <= 2 * k - 1)
                {
                    if (tree[tree[j]] >= tree[tree[j + 1]])
                        tree[j / 2] = tree[j];
                    else tree[j / 2] = tree[j + 1];
                    j += 2;
                }
                k /= 2;
            }

            return tree;
        }
    }
}
