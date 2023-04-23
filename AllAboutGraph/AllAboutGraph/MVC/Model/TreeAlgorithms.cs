using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class TreeAlgorithms<T>
    {
        MyTree<T> Tree { get; set; }

        public TreeAlgorithms(MyTree<T> tree) { Tree = tree; }
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
    }
}
