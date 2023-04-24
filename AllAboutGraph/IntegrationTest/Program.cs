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
            BinaryTreeSort();

            TestTrees();

            //Task2();

            //TestIncidenceMatrix();
            Console.ReadKey();
        }

        private static void BinaryTreeSort()
        {
            int[] array = new int[10];
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0, 100);
            }

            Console.WriteLine("Random Array: {0}", string.Join(" ", array));

            Console.WriteLine("Sorted Array: {0}", string.Join(" ", TreeSort(array)));

        }

        private static int[] TreeSort(int[] array)
        {
            MyBinaryTree<int> tree = new MyBinaryTree<int>(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                tree.Add(new MyBinaryTree<int>(array[i]));
            }

            return tree.Transform();
        }

        private static void TestTrees()
        {
            MyTree<int> tree = new MyTree<int>();
            tree.Add(10, new Node<int>(), true);    //10
            tree.Add(20, tree.Root, true);  //10->20
            Node<int> currentNode = tree.Root.LeftChild;
            for (int i = 0; i < 3; i++)
            {
                tree.Add(i, currentNode, false);
                tree.Add((i+1)*2, currentNode.RightSibling, false);
                tree.Add((i + 1) * 3, currentNode.RightSibling, true);
                currentNode = currentNode.RightSibling.LeftChild;
            }

            TreeAlgorithms<int> treeAlgorithms = new TreeAlgorithms<int>(tree, new MyBinaryTree<int>(10));

            List<Node<int>> preOrder = treeAlgorithms.PreOrder(tree.Root);
            List<Node<int>> inOrder = treeAlgorithms.InOrder(tree.Root);
            List<Node<int>> postOrder = treeAlgorithms.PostOrder(tree.Root);

            Console.WriteLine(tree.ToString());

            PrintNodesList(preOrder);
            PrintNodesList(inOrder);
            PrintNodesList(postOrder);

        }

        private static void PrintNodesList<T>(List<Node<T>> order)
        {
            foreach(Node<T> node in order)
            {
                Console.Write(node.Data + " ");
            }
            Console.WriteLine();
        }

        private static void Task2()
        {
            List<GraphVertex> inputVertices = new List<GraphVertex>()
            {
                new GraphVertex("1"),
                new GraphVertex("2"),
                new GraphVertex("3"),
                new GraphVertex("4"),
                new GraphVertex("5"),
                new GraphVertex("6"),
                new GraphVertex("7"),
                new GraphVertex("8"),
            };

            List<GraphEdge> inputEdges = new List<GraphEdge>()
            {
                new GraphEdge(inputVertices[0], inputVertices[1], 1, true), //1->2
                new GraphEdge(inputVertices[0], inputVertices[3], 1, true), //1->4
                new GraphEdge(inputVertices[1], inputVertices[4], 1, true), //2->5
                new GraphEdge(inputVertices[2], inputVertices[3], 1, true), //3->4
                new GraphEdge(inputVertices[2], inputVertices[4], 1, true), //3->5
                new GraphEdge(inputVertices[3], inputVertices[0], 1, true), //4->1
                new GraphEdge(inputVertices[3], inputVertices[6], 1, true), //4->7
                new GraphEdge(inputVertices[4], inputVertices[5], 1, true), //5->6
                new GraphEdge(inputVertices[4], inputVertices[7], 1, true), //5->8
                new GraphEdge(inputVertices[6], inputVertices[7], 1, true), //7->8
            };

            MyGraph graph = new MyGraph(inputVertices, inputEdges);

            Console.WriteLine("\nТаблица степеней вершин:");
            PrintIntMatrix(graph.DegreeTable);

            Console.WriteLine("\n\nМатрица смежности:");
            PrintMatrix(graph.AdjacencyMatrix);

            Console.WriteLine("\n\nМатрица инцидентности:");
            PrintIntMatrix(graph.IncidenceMatrix);

            float[,] distanceTable = GetDistanceTable(graph);
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

        

        private static float[,] GetDistanceTable(MyGraph graph)
        {
            float[,] distanceTable = new float[graph.GraphVertices.Count,graph.GraphVertices.Count];
            for (int startVertex = 0; startVertex < graph.GraphVertices.Count; startVertex++)
            {
                float[] distances = DijkstraAlgorithm(graph.AdjacencyMatrix, startVertex);
                for (int j = 0; j < graph.GraphVertices.Count; j++)
                {
                    distanceTable[startVertex,j] = distances[j];
                }
            }
            return distanceTable;
        }

        private static float[] DijkstraAlgorithm(float[,] adjacencyMatrix, int startVertex)
        {
            int numberOfVertices = adjacencyMatrix.GetLength(0);
            float[] distances = new float[numberOfVertices];
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
        private static int MinDistance(float[] distances, bool[] inShortestPath)
        {
            float min = int.MaxValue;
            int minVertexIndex = -1;

            for (int v = 0; v < distances.Length; v++)
                if (inShortestPath[v] == false && distances[v] <= min)
                {
                    min = distances[v];
                    minVertexIndex = v;
                }

            return minVertexIndex;
        }

        private static int CountRoutesDistanceFixed(float[,] distanceTable, int distance)
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

            foreach(int neighbour in adjacencyList[startVertex])
            {
                if (!isVisited[neighbour])
                {
                    path.Add(neighbour);
                    GetAllPathsUtil(adjacencyList,neighbour, destination, isVisited, path);
                    path.Remove(neighbour);
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

        private static void PrintMatrix(float[,] matrix)
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
        private static void PrintIntMatrix(int[,] matrix)
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
        private static void PrintDistanceTable(float[,] distanceTable)
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
