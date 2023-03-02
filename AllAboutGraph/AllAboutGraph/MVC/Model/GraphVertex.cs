using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class GraphVertex
    {
        #region Fields
        private string _name;

        private List<GraphEdge> _inEdges;
        private List<GraphEdge> _outEdges;
        #endregion

        #region Properties

        public string Name 
        { 
            get { return _name; } 
            set { _name = value; }
        }

        public List<GraphEdge> OutEdges
        {
            get { return _outEdges; }
        }
        public List<GraphEdge> InVertices
        {
            get { return _inEdges; }
        }

        private bool isOriented 
        {
            get { return OutEdges.Count == InVertices.Count; }
        }

        public int Degree
        {
            get 
            {
                if (isOriented)
                {
                    return OutEdges.Count;
                }
                else 
                { 
                    return OutEdges.Count + InVertices.Count; 
                }
            }
        }

        #endregion

        public GraphVertex()
        {

        }

        public GraphVertex(string name, List<GraphEdge> inEdges, List<GraphEdge> outEdges)
        {
            Name = name;
            InEdges = inEdges;
            OutEdges = outEdges;
        }

        public List<GraphEdge> GetAllEdges()
        {
            List<GraphEdge> allVertexEdges = new List<GraphEdge>();

            foreach (GraphEdge edge in OutEdges)
            {
                allVertexEdges.Add(edge);
            }

            foreach (GraphEdge edge in InVertices)
            {
                allVertexEdges.Add(edge);
            }

            return allVertexEdges;
        }
    }
}
