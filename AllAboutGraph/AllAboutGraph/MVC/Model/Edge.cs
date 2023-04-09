using System;
using System.Drawing;

namespace AllAboutGraph.MVC.Model
{
    public abstract class Edge
    {
        #region Fields
        private Vertex _firstVertex;
        private Vertex _secondVertex;

        private float _weight;
        #endregion

        #region Properties

        /// <summary>
        /// Доступ к первой вершине
        /// </summary>
        public Vertex FirstVertex
        {
            get { return _firstVertex; }
            set { _firstVertex = value; }
        }

        /// <summary>
        /// Доступ ко второй вершине
        /// </summary>
        public Vertex SecondVertex
        {
            get { return _secondVertex; }
            set { _secondVertex = value; }
        }

        /// <summary>
        /// Наличие петли
        /// </summary>
        public bool Loop
        {
            get { return _secondVertex == _firstVertex; }
        }

        /// <summary>
        /// Доступ к весу ребра
        /// </summary>
        public float Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        #endregion

        #region Contrusctors
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Edge()
        {

        }
        /// <summary>
        /// Конструктор по заданным параметрам
        /// </summary>
        /// <param name="firstVertex"> первая инцидентная вершина</param>
        /// <param name="secondVertex"> вторая инцидентная вершина</param>
        /// <param name="weight"> вес ребра</param>
        public Edge(Vertex firstVertex, Vertex secondVertex, float weight)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            Weight = weight;
        }

        #endregion

        #region Methods

        #region DrawMethods

        public abstract void Draw(Graphics graphics, Pen pen);


        protected void DrawWeight(Graphics graphics, Pen pen, float angle)
        {
            Font font = new Font("Segoe UI", 14);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            PointF edgeMiddlePoint = GetEdgeMiddle();
            PointF textDrawPoint = GetTextDrawPoint(edgeMiddlePoint, angle);


            graphics.DrawString(Convert.ToString(Weight), font, pen.Brush, textDrawPoint.X, textDrawPoint.Y, stringFormat);
        }

        #endregion
        #region AngleCalc
        /// <summary>
        /// The angle between the vertex where the edge enters and the vertex from which the edge originates
        /// </summary>
        /// <param name="vertexInPos">start vertex point</param>
        /// <param name="vertexOutPos">end vertex point</param>
        /// <returns></returns>
        protected double GetAngle(PointF vertexInPos, PointF vertexOutPos)
        {
            float oppositeSideLength = vertexOutPos.Y - vertexInPos.Y;
            float adjacentSideLength = vertexOutPos.X - vertexInPos.X;

            //end vertex is strictly above or below the start vertex
            if (adjacentSideLength == 0)
            {
                if (vertexOutPos.Y > vertexInPos.Y)
                {
                    //strictly below => -Pi/2
                    return GetAngleDegree((-1) * (float)Math.PI / 2, vertexOutPos, vertexInPos);
                }
                else
                {
                    //strictly above => Pi/2
                    return GetAngleDegree((float)Math.PI / 2, vertexOutPos, vertexInPos);
                }
            }

            //get degree representation of arctangent of angle between start and end vertices
            double angle = GetAngleDegree((float)Math.Atan((double)oppositeSideLength / adjacentSideLength), vertexOutPos,vertexInPos);
            return angle;
        }

        private float GetAngleDegree(float angle, PointF vertexOutPos, PointF vertexInPos)
        {
            //end vertex is below and to the right of the start
            if (vertexInPos.Y <= vertexOutPos.Y && vertexInPos.X >= vertexOutPos.X)
            {
                return angle + 1.57f * 2f;
            }

            //end vertex is above and to the right of the start
            if (vertexInPos.Y >= vertexOutPos.Y && vertexInPos.X >= vertexOutPos.X)
            {
                return angle + 1.57f * 2f;
            }

            return angle;
        }
        #endregion


        #region WeightPoints
        /// <summary>
        /// get mid point of the edge
        /// </summary>
        /// <returns>Point of the middle of the edge</returns>
        private PointF GetEdgeMiddle()
        {
            float x = (SecondVertex.Center.X + FirstVertex.Center.X) /2;
            float y = (SecondVertex.Center.Y + FirstVertex.Center.Y) / 2;

            PointF mid = new PointF(x,y);

            return mid;
        }

        /// <summary>
        /// get the point where to draw text on the edge
        /// </summary>
        /// <param name="edgeMiddlePoint">mid point of the edge</param>
        /// <param name="angle">angle between start and end vertices</param>
        /// <returns>Point of text on the edge</returns>
        private PointF GetTextDrawPoint(PointF edgeMiddlePoint,float angle)
        {
            float x = edgeMiddlePoint.X + 15* (float)Math.Cos(angle+Math.PI/2);
            float y = edgeMiddlePoint.Y + 15 * (float)Math.Sin(angle + Math.PI / 2);

            PointF drawPoint = new PointF(x,y);
            return drawPoint;
        }
        #endregion

        #endregion
    }
}
