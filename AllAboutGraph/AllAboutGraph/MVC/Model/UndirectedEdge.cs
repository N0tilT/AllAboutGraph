using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class UndirectedEdge : Edge
    {
        //Конструктор неориентированного ребра - используем конструктор базового класса
        public UndirectedEdge(Vertex firstVertex, Vertex secondVertex, float weight):base(firstVertex,secondVertex,weight)
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

            //подписываем вес ребра
            DrawWeight(graphics, pen, (float)angle);

        }

    }
}
