using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    /// <summary>
    /// Класс вершины графа
    /// </summary>
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

        /// <summary>
        /// Имя вершины
        /// </summary>
        public string Name 
        { 
            get { return _name; } 
            set { _name = value; }
        }

        /// <summary>
        /// Исходящие рёбра
        /// </summary>
        public List<GraphEdge> OutEdges
        {
            get { return _outEdges; }
        }
        /// <summary>
        /// Входящиеи рёбра
        /// </summary>
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

        /// <summary>
        /// Позиция вершины на холсте
        /// </summary>
        public PointF Location
        {
            get { return _location; }
            set { _location = value; }
        }

        /// <summary>
        /// Размер вершины
        /// </summary>
        public SizeF Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Центр вершины
        /// </summary>
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

        /// <summary>
        /// Радиус вершины
        /// </summary>
        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public GraphVertex()
        {
            _inEdges= new List<GraphEdge>();
            _outEdges = new List<GraphEdge>();
            Radius = 25;
            Size = new SizeF(Radius*2, Radius * 2);
        }

        /// <summary>
        /// Конструктор по имени вершины
        /// </summary>
        /// <param name="name"></param>
        public GraphVertex(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Конструктор по имени, позиции и радиусу
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="radius"></param>
        public GraphVertex(string name, PointF location, float radius)
        {
            Name = name;
            Location = location;
            Radius = radius;
            Size = new SizeF(Radius * 2, Radius * 2);
        }

        /// <summary>
        /// Конструктор по имени, входящим, исходящим рёбрам и радиусу
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inEdges"></param>
        /// <param name="outEdges"></param>
        /// <param name="radius"></param>
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
        /// <summary>
        /// Получить список входящих и исодящих рёбер
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Изобразить вершину
        /// </summary>
        /// <param name="g">объект графики</param>
        /// <param name="pen">цвет обводки вершины</param>
        /// <param name="backgroundBrush">цвет заливки вершины</param>
        /// <param name="fontBrush">цвет текста</param>
        /// <param name="font">шрифт текста</param>
        /// <param name="format">формат текста</param>
        public void DrawVertex(Graphics g, Pen pen, Brush backgroundBrush, Brush fontBrush, Font font,StringFormat format)
        {
            g.FillEllipse(backgroundBrush, new RectangleF(Location, Size));
            g.DrawEllipse(pen, new RectangleF(Location, Size));
            g.DrawString(Name, font, fontBrush, Center.X,Center.Y,format);
        }

        #endregion
    }
}
