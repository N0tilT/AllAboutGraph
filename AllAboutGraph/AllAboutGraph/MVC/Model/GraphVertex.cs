using System;
using System.Collections.Generic;
using System.Drawing;
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

        private PointF _location;
        private SizeF _size = SizeF.Empty;
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
        public List<GraphEdge> InEdges
        {
            get { return _inEdges; }
        }

        /// <summary>
        /// Возвращает количетсво исходящих рёбер
        /// </summary>
        public int Degree
        {
            get { return OutEdges.Count; }
        }

        public PointF Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public SizeF Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public PointF Center 
        { 
            get 
            {
                RectangleF bound = new RectangleF(Location, Size);

                float x = bound.X + bound.Width / 2;
                float y = bound.Y + bound.Height / 2;

                return new PointF(x,y); 
            } 
        }
        #endregion

        #region Constructor
        public GraphVertex()
        {
            _inEdges= new List<GraphEdge>();
            _outEdges = new List<GraphEdge>();
        }

        public GraphVertex(string name)
        {
            Name = name;
            _inEdges = new List<GraphEdge>();
            _outEdges = new List<GraphEdge>();
        }

        public GraphVertex(string name, List<GraphEdge> inEdges, List<GraphEdge> outEdges)
        {
            Name = name;
            _inEdges = inEdges;
            _outEdges = outEdges;
        }
        #endregion

        #region Methods
        public List<GraphEdge> GetAllEdges()
        {
            List<GraphEdge> allVertexEdges = new List<GraphEdge>();

            foreach (GraphEdge edge in OutEdges)
            {
                allVertexEdges.Add(edge);
            }

            foreach (GraphEdge edge in InEdges)
            {
                allVertexEdges.Add(edge);
            }

            return allVertexEdges;
        }

        public void Draw(Graphics g, Pen pen, PointF location)
        {
            Location= location;
            g.DrawEllipse(pen, new RectangleF(Location,Size));
        }

        #endregion
    }
}
