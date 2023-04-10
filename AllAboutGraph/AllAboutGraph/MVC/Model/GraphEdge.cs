using System;
using System.Drawing;

namespace AllAboutGraph.MVC.Model
{
    public class GraphEdge
    {
        #region Constants
        const float arrowSize = 15;
        #endregion

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

        #region Contrusctors
        public GraphEdge()
        {

        }

        public GraphEdge(GraphVertex vertexOut, GraphVertex vertexIn, float weight, bool isDirected)
        {
            VertexOut = vertexOut;
            VertexIn = vertexIn;
            Weight = weight;
            Directed = isDirected;
        }

        #endregion

        #region Methods

        #region DrawMethods
        public void DrawEdge(Graphics graphics, Pen pen)
        {
            graphics.DrawLine(pen, VertexIn.Center, VertexOut.Center);

            double angle = GetAngle(VertexIn.Center, VertexOut.Center);

            if (Directed)
            {
                DrawArrowPointer(graphics, pen, angle);
            }

            //DrawWeight(graphics, pen, (float)angle);

        }

        private void DrawArrowPointer(Graphics graphics, Pen pen, double angle)
        {
            PointF vertexBorderPoint = GetVertexBorderArrowPoint(angle);
            PointF leftSidePoint = GetLeftSideArrowPoint(angle, ref vertexBorderPoint);
            PointF rightSidePoint = GetRightSideArrowPoint(angle, ref vertexBorderPoint);

            PointF[] trianglePoints = new PointF[] { vertexBorderPoint, leftSidePoint, rightSidePoint };

            graphics.DrawPolygon(pen, trianglePoints);
            graphics.FillPolygon(pen.Brush, trianglePoints);
        }

        private void DrawWeight(Graphics graphics, Pen pen, float angle)
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
        private double GetAngle(PointF vertexInPos, PointF vertexOutPos)
        {
            float oppositeSideLength = vertexOutPos.Y - vertexInPos.Y;
            float adjacentSideLength = vertexOutPos.X - vertexInPos.X;

            //end vertex is strictly above or below the start vertex
            if (adjacentSideLength == 0)
            {
                if (vertexOutPos.Y > vertexInPos.Y)
                {
                    //strictly below => -Pi/2
                    return GetAngleDegree((-1) * (float)Math.PI / 2, vertexOutPos);
                }
                else
                {
                    //strictly above => Pi/2
                    return GetAngleDegree((float)Math.PI / 2, vertexOutPos);
                }
            }

            //get degree representation of arctangent of angle between start and end vertices
            double angle = GetAngleDegree((float)Math.Atan((double)oppositeSideLength / adjacentSideLength), VertexOut.Center);
            return angle;
        }

        private float GetAngleDegree(float angle, PointF vertexOutPos)
        {
            //end vertex is below and to the right of the start
            if (VertexIn.Center.Y <= vertexOutPos.Y && VertexIn.Center.X >= vertexOutPos.X)
            {
                return angle + 1.57f * 2f;
            }

            //end vertex is above and to the right of the start
            if (VertexIn.Center.Y >= vertexOutPos.Y && VertexIn.Center.X >= vertexOutPos.X)
            {
                return angle + 1.57f * 2f;
            }

            return angle;
        }
        #endregion

        #region ArrowDrawing
        /// <summary>
        /// get the point of arrow, which lays on the border of end vertex
        /// </summary>
        /// <param name="angle">angle between start and end vertices</param>
        /// <returns>Point of the top of the arrow</returns>
        private PointF GetVertexBorderArrowPoint(double angle)
        {
            float vertexBorderPointX = VertexIn.Radius * (float)Math.Cos(angle) + VertexIn.Center.X;
            float vertexBorderPointY = VertexIn.Radius * (float)Math.Sin(angle) + VertexIn.Center.Y;

            PointF vertexBorderPoint = new PointF(vertexBorderPointX, vertexBorderPointY);
            return vertexBorderPoint;
        }

        /// <summary>
        /// get the right side arrow vertex
        /// </summary>
        /// <param name="angle">angle between start and end vertices</param>
        /// <param name="vertexBorderPoint">point of the top of the arrow</param>
        /// <returns>Point of the right vertex of arrow</returns>
        private PointF GetRightSideArrowPoint(double angle, ref PointF vertexBorderPoint)
        {
            float rightX = (float)(
                            vertexBorderPoint.X + arrowSize * Math.Cos(angle) + arrowSize / 3 * Math.Cos(angle - Math.PI / 2));
            float rightY = (float)(
                vertexBorderPoint.Y + arrowSize * Math.Sin(angle) + arrowSize / 3 * Math.Sin(angle - Math.PI / 2));

            PointF rightSidePoint = new PointF(rightX, rightY);
            return rightSidePoint;
        }

        /// <summary>
        /// get the left side arrow vertex
        /// </summary>
        /// <param name="angle">angle between start and end vertices</param>
        /// <param name="vertexBorderPoint">point of the top of the arrow</param>
        /// <returns>Point of the left vertex of arrow</returns>
        private PointF GetLeftSideArrowPoint(double angle, ref PointF vertexBorderPoint)
        {
            float leftX = (float)(
                            vertexBorderPoint.X + arrowSize * Math.Cos(angle) + arrowSize / 3 * Math.Cos(angle + Math.PI / 2));

            float leftY = (float)(
                vertexBorderPoint.Y + arrowSize * Math.Sin(angle) + arrowSize / 3 * Math.Sin(angle + Math.PI / 2));


            PointF leftSidePoint = new PointF(leftX, leftY);
            return leftSidePoint;
        }

        #endregion

        #region WeightPoints
        /// <summary>
        /// get mid point of the edge
        /// </summary>
        /// <returns>Point of the middle of the edge</returns>
        private PointF GetEdgeMiddle()
        {
            float x = (VertexIn.Center.X + VertexOut.Center.X) /2;
            float y = (VertexIn.Center.Y + VertexOut.Center.Y) / 2;

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
