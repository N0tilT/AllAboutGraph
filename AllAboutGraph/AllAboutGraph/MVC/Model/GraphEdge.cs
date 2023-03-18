﻿using System;
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
        const float arrowSize = 20;

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

            float vertexBorderPointX = VertexIn.Radius * (float)Math.Cos(angle) + VertexIn.Center.X;
            float vertexBorderPointY = VertexIn.Radius * (float)Math.Sin(angle) + VertexIn.Center.Y;

            PointF vertexBorderPoint = new PointF(vertexBorderPointX, vertexBorderPointY);

            float leftX = (float)(
                vertexBorderPoint.X + arrowSize * Math.Cos(angle) + arrowSize / 2 * Math.Cos(angle + 1.57));

            float leftY = (float)(
                vertexBorderPoint.Y + arrowSize * Math.Sin(angle) + arrowSize / 2 * Math.Sin(angle + 1.57));


            PointF leftSidePoint = new PointF(leftX,leftY);

            float rightX = (float)(
                vertexBorderPoint.X + arrowSize * Math.Cos(angle) + arrowSize / 2 * Math.Cos(angle - 1.57));
            float rightY = (float)(
                vertexBorderPoint.Y + arrowSize * Math.Sin(angle) + arrowSize / 2 * Math.Sin(angle - 1.57));

            PointF rightSidePoint = new PointF(rightX, rightY);

            PointF[] trianglePoints = new PointF[] { vertexBorderPoint, leftSidePoint, rightSidePoint };

            graphics.DrawPolygon(pen,trianglePoints);
        }

        private double GetAngle(PointF vertexInPos, PointF vertexOutPos)
        {
            float y = vertexOutPos.Y - vertexInPos.Y;
            float x = vertexOutPos.X - vertexInPos.X;

            if(x == 0)
            {
                return GetAngleDegree((float)1.57,vertexOutPos);
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


        #endregion
    }
}
