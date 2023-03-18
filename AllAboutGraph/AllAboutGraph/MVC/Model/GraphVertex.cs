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
        private float _radius;
        private SizeF _size;
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

        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        #endregion

        #region Constructor
        public GraphVertex()
        {
            _inEdges= new List<GraphEdge>();
            _outEdges = new List<GraphEdge>();
            Radius = 25;
            Size = new SizeF(Radius*2, Radius * 2);
        }

        public GraphVertex(string name)
        {
            Name = name;
            _inEdges = new List<GraphEdge>();
            _outEdges = new List<GraphEdge>();
            Radius = 25;
            Size = new SizeF(Radius * 2, Radius * 2);
        }

        public GraphVertex(string name, List<GraphEdge> inEdges, List<GraphEdge> outEdges, float radius)
        {
            Name = name;
            _inEdges = inEdges;
            _outEdges = outEdges;
            Radius = radius;
            Size = new SizeF(Radius * 2, Radius * 2);

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

        public void DrawVertex(Graphics g, Pen pen, Brush backgroundBrush, Brush fontBrush, Font font,StringFormat format)
        {
            g.FillEllipse(backgroundBrush, new RectangleF(Location, Size));
            g.DrawEllipse(pen, new RectangleF(Location, Size));
            g.DrawString(Name, font, fontBrush, Center.X,Center.Y,format);
        }

        #endregion
    }
}
