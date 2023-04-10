using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class DirectedEdge : Edge
    {

        #region Constants
        const float arrowSize = 15;

        #endregion

        //Конструктор ориентированного ребра - используем конструктор базового класса
        public DirectedEdge(Vertex outVertex, Vertex inVertex, float weight) : base(outVertex, inVertex, weight)
        {

        }

        /// <summary>
        /// Метод рисования неориентированного ребра на экране
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pen"></param>
        public override void Draw(Graphics graphics, Pen pen)
        {
            //рисуем линию между двумя вершинами - ребро
            graphics.DrawLine(pen, FirstVertex.Center, SecondVertex.Center);

            //получаем угол в градусах между вершинами
            double angle = GetAngle(FirstVertex.Center, SecondVertex.Center);

            DrawArrowPointer(graphics, pen, angle);

            //подписываем вес ребра
            DrawWeight(graphics, pen, (float)angle);

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

        #region ArrowDrawing
        /// <summary>
        /// get the point of arrow, which lays on the border of end vertex
        /// </summary>
        /// <param name="angle">angle between start and end vertices</param>
        /// <returns>Point of the top of the arrow</returns>
        private PointF GetVertexBorderArrowPoint(double angle)
        {
            float vertexBorderPointX = SecondVertex.Radius * (float)Math.Cos(angle) + SecondVertex.Center.X;
            float vertexBorderPointY = SecondVertex.Radius * (float)Math.Sin(angle) + SecondVertex.Center.Y;

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
    }
}
