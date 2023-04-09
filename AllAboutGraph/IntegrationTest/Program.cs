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
            Task2();

            //TestIncidenceMatrix();
        }

        private static void Task2()
        {
            List<Vertex> inputVertices = new List<Vertex>()
            {
                new Vertex("1"),
                new Vertex("2"),
                new Vertex("3"),
                new Vertex("4"),
                new Vertex("5"),
                new Vertex("6"),
                new Vertex("7"),
                new Vertex("8"),
            };

            List<Edge> inputEdges = new List<Edge>()
            {
                new Edge(inputVertices[0], inputVertices[1], 1, true), //1->2
                new Edge(inputVertices[0], inputVertices[3], 1, true), //1->4
                new Edge(inputVertices[1], inputVertices[4], 1, true), //2->5
                new Edge(inputVertices[2], inputVertices[3], 1, true), //3->4
                new Edge(inputVertices[2], inputVertices[4], 1, true), //3->5
                new Edge(inputVertices[3], inputVertices[0], 1, true), //4->1
                new Edge(inputVertices[3], inputVertices[6], 1, true), //4->7
                new Edge(inputVertices[4], inputVertices[5], 1, true), //5->6
                new Edge(inputVertices[4], inputVertices[7], 1, true), //5->8
                new Edge(inputVertices[6], inputVertices[7], 1, true), //7->8
            };

            MyGraph graph = new MyGraph(inputVertices, inputEdges);

            Console.WriteLine("\nТаблица степеней вершин:");
            PrintMatrix(graph.DegreeTable);

            Console.WriteLine("\n\nМатрица смежности:");
            PrintMatrix(graph.AdjacencyMatrix);

            Console.WriteLine("\n\nМатрица инцидентности:");
            PrintMatrix(graph.IncidenceMatrix);

            int[,] distanceTable = GetDistanceTable(graph);
            Console.WriteLine("\n\nТаблица расстояний:");
            PrintDistanceTable(distanceTable);

            Console.WriteLine("\n\nКоличество маршрутов длины 2:");
            int twoDistRoutes = CountRoutesDistanceFixed(distanceTable, 2);
            Console.WriteLine(twoDistRoutes);

            Console.WriteLine("\n\nВсе маршруты с началом в вершине 3 длины 3:");
            List<List<int>> paths = GetPathsDistanceFixed(graph,3,3);
            PrintList(paths);

            Console.ReadKey();

        }

        

        private static int[,] GetDistanceTable(MyGraph graph)
        {
            int[,] distanceTable = new int[graph.GraphVertices.Count,graph.GraphVertices.Count];
            for (int startVertex = 0; startVertex < graph.GraphVertices.Count; startVertex++)
            {
                int[] distances = DijkstraAlgorithm(graph.AdjacencyMatrix, startVertex);
                for (int j = 0; j < graph.GraphVertices.Count; j++)
                {
                    distanceTable[startVertex,j] = distances[j];
                }
            }
            return distanceTable;
        }

        private static int[] DijkstraAlgorithm(int[,] adjacencyMatrix, int startVertex)
        {
            int numberOfVertices = adjacencyMatrix.GetLength(0);
            int[] distances = new int[numberOfVertices];
            bool[] inShortestPath = new bool[numberOfVertices];

            const int INFINITY = int.MaxValue;

            for (int i = 0; i < numberOfVertices; i++)
            {
                distances[i] = INFINITY;
                inShortestPath[i] = false;
            }

            distances[startVertex] = 0;

            for (int i = 0; i < numberOfVertices-1; i++)
            {
                int minVertex = MinDistance(distances, inShortestPath);

                inShortestPath[minVertex] = true;

                for (int adjacentVertex = 0; adjacentVertex < numberOfVertices; adjacentVertex++)
                {
                    if (!inShortestPath[adjacentVertex] 
                        && adjacencyMatrix[minVertex,adjacentVertex] != 0
                        && distances[minVertex] != INFINITY
                        && distances[minVertex] + adjacencyMatrix[minVertex,adjacentVertex] < distances[adjacentVertex])
                    {
                        distances[adjacentVertex] = distances[minVertex] + adjacencyMatrix[minVertex, adjacentVertex];
                    }
                }

            }

            return distances;
        }

        /// <summary>
        /// Найти вершину с минимальным путём до неё, которой ещё нет в кратчайшем пути
        /// </summary>
        /// <param name="distances"></param>
        /// <param name="inShortestPath"></param>
        /// <returns></returns>
        private static int MinDistance(int[] distances, bool[] inShortestPath)
        {
            int min = int.MaxValue;
            int minVertexIndex = -1;

            for (int v = 0; v < distances.Length; v++)
                if (inShortestPath[v] == false && distances[v] <= min)
                {
                    min = distances[v];
                    minVertexIndex = v;
                }

            return minVertexIndex;
        }

        private static int CountRoutesDistanceFixed(int[,] distanceTable, int distance)
        {
            int count = 0;

            for (int i = 0; i < distanceTable.GetLength(0); i++)
            {
                for (int j = 0; j < distanceTable.GetLength(1); j++)
                {
                    if (distanceTable[i, j] == distance)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        static List<List<int>> paths = new List<List<int>>();
        private static List<List<int>> GetPathsDistanceFixed(MyGraph graph,int startVertex, int distance)
        {
            for (int destination = 0; destination < graph.GraphVertices.Count; destination++)
            {
                bool[] isVisited = new bool[graph.GraphVertices.Count];

                List<int> path = new List<int>();
                path.Add(startVertex);
                GetAllPathsUtil(graph.AdjacencyList,startVertex, destination, isVisited, path);

                paths.Add(path);
            }

            List<List<int>> answerPaths = new List<List<int>>();
            foreach (List<int> path in paths)
            {
                if(path.Count == distance)
                {
                    answerPaths.Add(path);
                }
            }
            return answerPaths;
        }

        private static void GetAllPathsUtil(List<List<int>> adjacencyList,int startVertex, int destination, bool[] isVisited,List<int> path)
        {
            if (startVertex == destination)
            {
                if (!paths.Contains(path))
                {
                    paths.Add(CopyList(path));
                }
                return;
            }

            isVisited[startVertex] = true;

            foreach(int i in adjacencyList[startVertex])
            {
                if (!isVisited[i])
                {
                    path.Add(i);
                    GetAllPathsUtil(adjacencyList,i, destination, isVisited, path);
                    path.Remove(i);
                }
            }

            isVisited[startVertex] = false;
        }

        private static List<int> CopyList(List<int> list)
        {
            List<int> copyList = new List<int>();

            for (int i = 0; i < list.Count; i++)
            {
                copyList.Add(list[i]);
            }

            return copyList;


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

            //int[,] matrix = new int[,]
            //{
            //    {0,1,1,0,1 },
            //    {1,0,1,0,0 },
            //    {1,0,0,0,0 },
            //    {0,0,1,0,1 },
            //    {1,0,1,1,0 },
            //};
            //AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(matrix);

            //int[,] expectedMatrix = new int[,] {
            //        {1, 1, 0, 0, 0 },
            //        {1, 0, 1, 0 , 0 },
            //        {1, 0, 0, 0, 1 },
            //        {0, 1, -1, 0, 0 },
            //        {0, 0, -1, 1, 0 },
            //        {0, 0, 0, 1, 1 },
            //        {0, 0, -1, 0, 1 } };

            //IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjacencyMatrix);
            //PrintMatrix(incidenceMatrix.Matrix);

            int[,] incMatrix = new int[,] {
                    {1, 1, 0, 0, 0 },
                    {1, 0, 1, 0 , 0 },
                    {1, 0, 0, 0, 1 },
                    {0, 1, -1, 0, 0 },
                    {0, 0, -1, 1, 0 },
                    {0, 0, 0, 1, 1 },
                    {0, 0, -1, 0, 1 } };
            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(incMatrix);

            List<List<int>> expectedList = new List<List<int>>()
            {
                new List<int>(){2,3,5 },
                new List<int>(){1,3 },
                new List<int>(){1 },
                new List<int>(){3,5 },
                new List<int>(){1,3,4 },
            };

            AdjacencyList adjacencyList = new AdjacencyList(incidenceMatrix);

            PrintList(adjacencyList.List);

            Console.ReadKey();
        }

        private static void PrintList(List<List<int>> list)
        {
            foreach (List<int> row in list)
            {
                Console.WriteLine();
                foreach (int item in row)
                {
                    Console.Write(item + " ");
                }
            }
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
        private static void PrintDistanceTable(int[,] distanceTable)
        {
            for (int i = 0; i < distanceTable.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < distanceTable.GetLength(1); j++)
                {
                    if (distanceTable[i, j] == int.MaxValue)
                    {
                        Console.Write("inf ");
                    }
                    else
                    {
                        Console.Write(distanceTable[i, j] + " ");
                    }
                }
            }
        }
    }
}
