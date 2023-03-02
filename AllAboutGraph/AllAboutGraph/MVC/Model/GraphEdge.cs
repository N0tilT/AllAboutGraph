using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class GraphEdge
    {
        #region Fields
        private GraphVertex _vertexOut;
        private GraphVertex _vertexIn;

        private bool _directed;

        private float _weight;
        #endregion

        #region Properties

        public GraphVertex VertexOut
        {
            get { return _vertexOut; }
            set { _vertexOut = value; }
        }

        public GraphVertex VertexIn
        {
            get { return _vertexIn; }
            set { _vertexIn = value; }
        }

        public bool Directed
        {
            get { return _directed; }
            set { _directed = value; }
        }

        public bool Loop
        {
            get { return _vertexIn == _vertexOut; }
        }

        public float Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        #endregion

        public GraphEdge()
        {

        }

        public GraphEdge(GraphVertex vertexOut, GraphVertex vertexIn, float weight, bool isDirected)
        {

        }


    }
}
