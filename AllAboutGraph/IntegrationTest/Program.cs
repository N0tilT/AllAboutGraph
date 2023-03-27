using System;
using System.Collections.Generic;
using AllAboutGraph;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllAboutGraph.MVC.Model;

namespace IntegrationTest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TestIncidenceMatrix();
        }

        private static void TestIncidenceMatrix()
        {
            //List<List<int>> list = new List<List<int>>{
            //    new List<int> { 2, 3, 5 },
            //    new List<int> { 1, 3 },
            //    new List<int> { 1 },
            //    new List<int> { 3, 5 },
            //    new List<int> { 1, 3, 4 } };
            //AdjacencyList adjList = new AdjacencyList(list);


            //IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjList);
            //PrintMatrix(incidenceMatrix.Matrix);

            int[,] matrix = new int[,]
            {
                {0,1,1,0,1 },
                {1,0,1,0,0 },
                {1,0,0,0,0 },
                {0,0,1,0,1 },
                {1,0,1,1,0 },
            };
            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(matrix);

            int[,] expectedMatrix = new int[,] {
                    {1, 1, 0, 0, 0 },
                    {1, 0, 1, 0 , 0 },
                    {1, 0, 0, 0, 1 },
                    {0, 1, -1, 0, 0 },
                    {0, 0, -1, 1, 0 },
                    {0, 0, 0, 1, 1 },
                    {0, 0, -1, 0, 1 } };

            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjacencyMatrix);
            PrintMatrix(incidenceMatrix.Matrix);

            Console.ReadKey();
        }

        private static void PrintAdjList(List<List<int>> list)
        {
            foreach (List<int> adj in list)
            {
                Console.WriteLine();
                foreach (int item in adj)
                {
                    Console.Write(item + " ");
                }
            }
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
            }
        }
    }
}
