using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class GraphEdge
    {
        const float arrowSize = 15;

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

        public void DrawEdge(Graphics graphics, Pen pen)
        {
            graphics.DrawLine(pen, VertexIn.Center, VertexOut.Center);

            double angle = GetAngle(VertexIn.Center, VertexOut.Center);

            if (Directed)
            {
                DrawArrowTop(graphics, pen, angle);
            }

            DrawWeight(graphics, pen, (float)angle);

        }
        private double GetAngle(PointF vertexInPos, PointF vertexOutPos)
        {
            float y = vertexOutPos.Y - vertexInPos.Y;
            float x = vertexOutPos.X - vertexInPos.X;

            if (x == 0)
            {
                if (vertexOutPos.Y > vertexInPos.Y)
                {
                    return GetAngleDegree((-1) * (float)Math.PI / 2, vertexOutPos);
                }
                else
                {
                    return GetAngleDegree((float)Math.PI / 2, vertexOutPos);
                }
            }

            double angle = GetAngleDegree((float)Math.Atan((double)y / x), VertexOut.Center);
            return angle;
        }

        private float GetAngleDegree(float angle, PointF vertexOutPos)
        {
            //Центр фигуры ниже и правее
            if (VertexIn.Center.Y <= vertexOutPos.Y && VertexIn.Center.X >= vertexOutPos.X)
            {
                return angle + 1.57f * 2f;
            }

            //Центр фигуры выше и правее
            if (VertexIn.Center.Y >= vertexOutPos.Y && VertexIn.Center.X >= vertexOutPos.X)
            {
                return angle + 1.57f * 2f;
            }

            return angle;
        }

        private void DrawArrowTop(Graphics graphics, Pen pen, double angle)
        {
            PointF vertexBorderPoint = GetVertexBorderArrowPoint(angle);
            PointF leftSidePoint = GetLeftSideArrowPoint(angle, ref vertexBorderPoint);
            PointF rightSidePoint = GetRightSideArrowPoint(angle, ref vertexBorderPoint);

            PointF[] trianglePoints = new PointF[] { vertexBorderPoint, leftSidePoint, rightSidePoint };

            graphics.DrawPolygon(pen, trianglePoints);
            graphics.FillPolygon(pen.Brush, trianglePoints);
        }

        private PointF GetRightSideArrowPoint(double angle, ref PointF vertexBorderPoint)
        {
            float rightX = (float)(
                            vertexBorderPoint.X + arrowSize * Math.Cos(angle) + arrowSize / 3 * Math.Cos(angle - Math.PI / 2));
            float rightY = (float)(
                vertexBorderPoint.Y + arrowSize * Math.Sin(angle) + arrowSize / 3 * Math.Sin(angle - Math.PI / 2));

            PointF rightSidePoint = new PointF(rightX, rightY);
            return rightSidePoint;
        }

        private PointF GetLeftSideArrowPoint(double angle, ref PointF vertexBorderPoint)
        {
            float leftX = (float)(
                            vertexBorderPoint.X + arrowSize * Math.Cos(angle) + arrowSize / 3 * Math.Cos(angle + Math.PI / 2));

            float leftY = (float)(
                vertexBorderPoint.Y + arrowSize * Math.Sin(angle) + arrowSize / 3 * Math.Sin(angle + Math.PI / 2));


            PointF leftSidePoint = new PointF(leftX, leftY);
            return leftSidePoint;
        }

        private PointF GetVertexBorderArrowPoint(double angle)
        {
            float vertexBorderPointX = VertexIn.Radius * (float)Math.Cos(angle) + VertexIn.Center.X;
            float vertexBorderPointY = VertexIn.Radius * (float)Math.Sin(angle) + VertexIn.Center.Y;

            PointF vertexBorderPoint = new PointF(vertexBorderPointX, vertexBorderPointY);
            return vertexBorderPoint;
        }


        private void DrawWeight(Graphics graphics, Pen pen, float angle)
        {
            Font font = new Font("Segoe UI", 14);
            StringFormat stringFormat= new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            PointF edgeMiddlePoint = GetEdgeMiddle();
            PointF textDrawPoint = GetTextDrawPoint(edgeMiddlePoint,angle);


            graphics.DrawString(Convert.ToString(Weight),font,pen.Brush, textDrawPoint.X, textDrawPoint.Y,stringFormat);
        }


        private PointF GetEdgeMiddle()
        {
            float x = (VertexIn.Center.X + VertexOut.Center.X) /2;
            float y = (VertexIn.Center.Y + VertexOut.Center.Y) / 2;

            PointF mid = new PointF(x,y);

            return mid;
        }

        private PointF GetTextDrawPoint(PointF edgeMiddlePoint,float angle)
        {
            float x = edgeMiddlePoint.X + 15* (float)Math.Cos(angle+Math.PI/2);
            float y = edgeMiddlePoint.Y + 15 * (float)Math.Sin(angle + Math.PI / 2);

            PointF drawPoint = new PointF(x,y);
            return drawPoint;
        }

        #endregion
    }
}
