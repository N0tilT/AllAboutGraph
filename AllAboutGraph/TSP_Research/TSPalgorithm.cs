using AllAboutGraph.MVC.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace TSP_Research
{
    public class TSPalgorithm
    {
        #region Fields
        private MyGraph _graph;
        private float[,] _distanceTable;

        #region Results
        private List<int> _fullSearchResultPath;
        private List<int> _randomFullSearchresultPath;
        private List<int> _nearestNeighbourResultPath;
        private List<int> _improvedNearestNeighbourResultPath;
        private List<int> _simulatedAnnealingResultPath;
        private List<int> _branchesAndBoundariesResultPath;
        private List<int> _antColonyAlgorithmResultPath;

        private float _fullSearchResultPathLength;
        private float _randomFullSearchresultPathLength;
        private float _nearestNeighbourResultPathLength;
        private float _improvedNearestNeighbourResultPathLength;
        private float _simulatedAnnealingResultPathLength;
        private float _branchesAndBoundariesResultPathLength;
        private float _antColonyAlgorithmResultPathLength;
        #endregion

        #endregion

        #region Properties
        /// <summary>
        /// Доступ к графу
        /// </summary>
        public MyGraph Graph { get => _graph; set => _graph = value; }
        /// <summary>
        /// Доступ к таблице расстояний графа
        /// </summary>
        public float[,] DistanceTable { get => _distanceTable; set => _distanceTable = value; }

        #region Results
        //Результирующие пути алгоритмов
        public List<int> FullSearchResultPath { get => _fullSearchResultPath; set => _fullSearchResultPath = value; }
        public List<int> NearestNeighbourResultPath { get => _nearestNeighbourResultPath; set => _nearestNeighbourResultPath = value; }
        public List<int> ImprovedNearestNeighbourResultPath { get => _improvedNearestNeighbourResultPath; set => _improvedNearestNeighbourResultPath = value; }
        public List<int> SimulatedAnnealingResultPath { get => _simulatedAnnealingResultPath; set => _simulatedAnnealingResultPath = value; }
        public List<int> BranchesAndBoundariesResultPath { get => _branchesAndBoundariesResultPath; set => _branchesAndBoundariesResultPath = value; }
        public List<int> AntColonyAlgorithmResultPath { get => _antColonyAlgorithmResultPath; set => _antColonyAlgorithmResultPath = value; }
        //Результирующие длины путей алгоритмов
        public float FullSearchResultPathLength { get => _fullSearchResultPathLength; set => _fullSearchResultPathLength = value; }
        public float NearestNeighbourResultPathLength { get => _nearestNeighbourResultPathLength; set => _nearestNeighbourResultPathLength = value; }
        public float ImprovedNearestNeighbourResultPathLength { get => _improvedNearestNeighbourResultPathLength; set => _improvedNearestNeighbourResultPathLength = value; }
        public float SimulatedAnnealingResultPathLength { get => _simulatedAnnealingResultPathLength; set => _simulatedAnnealingResultPathLength = value; }
        public float BranchesAndBoundariesResultPathLength { get => _branchesAndBoundariesResultPathLength; set => _branchesAndBoundariesResultPathLength = value; }
        public float AntColonyAlgorithmResultPathLength { get => _antColonyAlgorithmResultPathLength; set => _antColonyAlgorithmResultPathLength = value; }
        #endregion

        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public TSPalgorithm() { }
        /// <summary>
        /// Конструктор по заданному графу
        /// </summary>
        /// <param name="graph"></param>
        public TSPalgorithm(MyGraph graph)
        {
            Graph = graph;
            DistanceTable = Graph.GetDistanceTable();
        }

        #endregion

        #region Methods

        #region Other
        /// <summary>
        /// Вычислить длину пути
        /// </summary>
        /// <param name="path">путь</param>
        /// <param name="distanceTable">таблица расстояний</param>
        /// <returns></returns>
        private float Distance(int[] path, float[,] distanceTable)
        {
            float distance = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                distance += distanceTable[path[i] - 1, path[i + 1] - 1];
            }
            return distance;
        }
        /// <summary>
        /// Найти минимальный путь в списке
        /// </summary>
        /// <param name="paths">список путей</param>
        /// <param name="distanceTable">таблица расстояний</param>
        /// <returns>минимальный путь - массив вершин</returns>
        private int[] FindMinPath(List<int[]> paths, float[,] distanceTable)
        {
            int[] minPath = paths[0];
            float minPathLength = Distance(paths[0], distanceTable);
            foreach (int[] path in paths)
            {
                float curPathLength = Distance(path, distanceTable);
                if (curPathLength < minPathLength)
                {
                    minPathLength = curPathLength;
                    minPath = path;
                }
            }
            return minPath;
        }

        /// <summary>
        /// Найти минимальный путь в списке
        /// </summary>
        /// <param name="paths">список путей</param>
        /// <param name="distanceTable">таблица расстояний</param>
        /// <returns>минимальный путь - список вершин</returns>
        private List<int> FindMinPath(List<List<int>> paths, float[,] distanceTable)
        {
            List<int> minPath = paths[0];
            float minPathLength = Distance(paths[0].ToArray(), distanceTable);
            foreach (List<int> path in paths)
            {
                float curPathLength = Distance(path.ToArray(), distanceTable);
                if (curPathLength < minPathLength)
                {
                    minPathLength = curPathLength;
                    minPath = path;
                }
            }
            return minPath;
        }
        #endregion

        #region FullSearch
        internal double FullSearchTimer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            FullSearchResultPath = FullSearch(Graph,DistanceTable);

            if (FullSearchResultPath.Count == 0) return 0;

            stopwatch.Stop();

            FullSearchResultPathLength = Distance(FullSearchResultPath.ToArray(), DistanceTable);
            return stopwatch.Elapsed.TotalSeconds/100;
        }

        private List<int> FullSearch(MyGraph graph, float[,] distanceTable)
        {
            if(graph.GraphVertices.Count >=12) return new List<int> { 0 };
            int[] path = GetMinimumPath(graph.GraphVertices,distanceTable);
            return new List<int>(path);
        }

        private static int[] minimumPermutation;
        private static float minPathLength;
        private int[] GetMinimumPath(List<GraphVertex> graphVertices, float[,] distanceTable)
        {
            minimumPermutation = new int[graphVertices.Count];
            int[] permutationRow = new int[graphVertices.Count];
            for (int i = 0; i < graphVertices.Count; i++)
            {
                permutationRow[i] = int.Parse(graphVertices[i].Name);
                minimumPermutation[i] = permutationRow[i];
            }
            minPathLength = Distance(minimumPermutation, distanceTable);

            Permutate(permutationRow, 0, distanceTable);

            return minimumPermutation;
        }

        private void Permutate(int[] row, int start, float[,] distanceTable)
        {
            if(start >= row.Length)
            {
                int[] nextPermutation = new int[row.Length+1];

                for (int i = 0; i < nextPermutation.Length-1; i++)
                {
                    nextPermutation[i] = row[i];
                }

                nextPermutation[nextPermutation.Length-1] = nextPermutation[0];

                float pathLength = Distance(nextPermutation, distanceTable);
                if (pathLength < minPathLength)
                {
                    minimumPermutation = nextPermutation;
                    minPathLength = pathLength;
                }
                return;
            }

            Permutate(row, start+1,distanceTable);
            for (int i = start+1; i < row.Length; i++)
            {
                Swap(row, start, i);
                Permutate(row, start+1, distanceTable);
                Swap(row, start, i);
            }

        }

        private void Swap(int[] row, int start, int i)
        {
            int tmp = row[i];
            row[i] = row[start];
            row[start] = tmp;
        }
        #endregion

        #region NearestNeighbour
        internal double NearestNeighbourTimer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            NearestNeighbourResultPath = NearestNeighbour(Graph, 0, DistanceTable);

            stopwatch.Stop();
            NearestNeighbourResultPathLength = Distance(NearestNeighbourResultPath.ToArray(),DistanceTable);
            return stopwatch.Elapsed.TotalSeconds*10000;
        }


        private List<int> NearestNeighbour(MyGraph graph, int startIndex, float[,] distanceTable)
        {
            int n = graph.GraphVertices.Count;
            bool[] visited = new bool[n];

            List<int> path = new List<int>();


            int nearestVertexIndex = startIndex;
            path.Add(nearestVertexIndex+1);
            while (visited.Contains(false))
            {
                if(!visited[nearestVertexIndex])
                {
                    visited[nearestVertexIndex] = true;
                    if (visited.Contains(false))
                    {
                        nearestVertexIndex = FindShortestEdge(distanceTable, nearestVertexIndex, visited);
                        path.Add(nearestVertexIndex + 1);
                    }
                }
            }
            path.Add(startIndex+1);
            return path;
        }

        private int FindShortestEdge(float[,] distanceTable, int currentVertexIndex, bool[] visited)
        {
            float shortestEdge = int.MaxValue;
            int nearest = currentVertexIndex;
            for (int neighbour = 0; neighbour < distanceTable.GetLength(0); neighbour++)
            {
                if (neighbour != currentVertexIndex && !visited[neighbour])
                {
                    if (distanceTable[currentVertexIndex, neighbour] < shortestEdge)
                    {
                        shortestEdge = distanceTable[currentVertexIndex, neighbour];
                        nearest = neighbour;
                    }
                }
            }

            return nearest;
        }
        #endregion

        #region ImprovedNearestNeighbour
        internal double ImprovedNearestNeighbourTimer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ImprovedNearestNeighbourResultPath = ImprovedNearestNeighbour(Graph, DistanceTable);

            stopwatch.Stop();
            ImprovedNearestNeighbourResultPathLength = Distance(ImprovedNearestNeighbourResultPath.ToArray(), DistanceTable);
            return stopwatch.Elapsed.TotalSeconds * 100;
        }

        private List<int> ImprovedNearestNeighbour(MyGraph graph, float[,] distanceTable)
        {
            List<int[]> possiblePaths = new List<int[]>();
            foreach(GraphVertex startVertex in graph.GraphVertices)
            {
                possiblePaths.Add(NearestNeighbour(graph,int.Parse(startVertex.Name)-1,distanceTable).ToArray());
            }
            return new List<int>(FindMinPath(possiblePaths, distanceTable));
        }
        #endregion

        #region SimulatedAnnealing
        internal double SimulatedAnnealingTimer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SimulatedAnnealingResultPath = SimulatedAnnealing(Graph, DistanceTable, 100,0.00001);

            stopwatch.Stop();
            SimulatedAnnealingResultPathLength = Distance(SimulatedAnnealingResultPath.ToArray(),DistanceTable);
            return stopwatch.Elapsed.TotalSeconds*10;
        }

        private List<int> SimulatedAnnealing(MyGraph graph, float[,] distanceTable, double initialTemperature, double endTemperature)
        {
            List<int> localBestPath = new List<int>();
            
            //Start path - vertices in order: 1234561 e.t.c
            foreach(GraphVertex vertex in graph.GraphVertices)
            {
                localBestPath.Add(int.Parse(vertex.Name));
            }
            localBestPath.Add(int.Parse(graph.GraphVertices[0].Name));

            double T = initialTemperature;
            float bestPathLength = Distance(localBestPath.ToArray(), distanceTable);

            int i = 0;
            while (T>endTemperature)
            {
                List<int> nextCandidate = GenerateCandidate(localBestPath);
                float candidatePathLength = Distance(nextCandidate.ToArray(),distanceTable);

                if (candidatePathLength < bestPathLength)
                {
                    bestPathLength = candidatePathLength;
                    localBestPath = nextCandidate;
                }
                else
                {
                    double p = GetTransitionProbability(candidatePathLength - bestPathLength,T);
                    Random random = new Random();
                    double nextrnd = random.NextDouble();

                    if (nextrnd <= p)
                    {
                        bestPathLength = candidatePathLength;
                        localBestPath = nextCandidate;
                    }

                }
                i++;
                T = initialTemperature*0.1/i;

            }

            return localBestPath;
        }

        private double GetTransitionProbability(float delta, double temperature)
        {
            return Math.Exp((-1) * delta/temperature);
        }

        private List<int> GenerateCandidate(List<int> localBestPath)
        {
            Random random = new Random();

            int firstReverseIndex = random.Next(1,localBestPath.Count - 1);    //any without first and last elements
            int secondReverseIndex = random.Next(1, localBestPath.Count - 1);

            if (firstReverseIndex < secondReverseIndex)
            {
                return new List<int>(ReverseArray(localBestPath.ToArray(), firstReverseIndex, secondReverseIndex));
            }
            else
            {
                return new List<int>(ReverseArray(localBestPath.ToArray(), secondReverseIndex, firstReverseIndex));
            }
        }

        private int[] ReverseArray(int[] array, int start, int end)
        {
            int[] answer = new int[array.Length];

            int[] segment = new int[end-start+1];

            int j = 0;
            for (int i = end; i >= start; i--)
            {
                segment[j] = array[i];
                j++;
            }

            j = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (i >= start && i <= end)
                {
                    answer[i] = segment[j];
                    j++;
                }
                else
                {
                    answer[i] = array[i];
                }
            }

            return answer;
        }
        #endregion

        #region AntColony
        internal double AntColonyAlgorithmTimer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            AntColonyAlgorithmResultPath = AntColonyAlgorithm(Graph, DistanceTable, 1000, 1, 1, 1, 1);

            stopwatch.Stop();
            AntColonyAlgorithmResultPathLength = Distance(AntColonyAlgorithmResultPath.ToArray(), DistanceTable);
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> AntColonyAlgorithm(MyGraph graph, float[,] distancetable, int itetationsCount, double alpha, double beta, double q, double oblivionNumber)
        {
            List<List<int>> Iterations = new List<List<int>>();

            int counter = itetationsCount;
            while (counter > 0)
            {
                int startIndex = 0;

                double[,] feromoneTable = InitializeFeromoneTable(graph.GraphVertices.Count);
                bool[] visited = InitializeVisited(graph.GraphVertices.Count);

                List<int> antPath = new List<int>();
                int i = startIndex;
                antPath.Add(i + 1);
                while (visited.Contains(false))
                {
                    visited[i] = true;

                    List<double> attractiveness = new List<double>();
                    List<double> roulette = new List<double>();
                    List<int> candidates = new List<int>();


                    roulette.Add(0);

                    int edgeIndex = 0;
                    double sumAttractiveness = 0;
                    for (int j = 0; j < graph.GraphVertices.Count; j++)
                    {
                        if (!visited[j])
                        {
                            attractiveness.Add(Attractiveness(i, j, distancetable, feromoneTable, alpha, beta));
                            sumAttractiveness = attractiveness.Sum();
                            candidates.Add(j);
                            edgeIndex++;
                        }
                    }

                    edgeIndex = 0;
                    for (int j = 0; j < graph.GraphVertices.Count; j++)
                    {
                        if (!visited[j])
                        {
                            double probability = attractiveness[edgeIndex] / sumAttractiveness;
                            double previous = roulette.Count > 1 ? roulette[edgeIndex] : 0;
                            roulette.Add(previous + probability);
                            edgeIndex++;
                        }
                    }

                    Random random = new Random();
                    double stepProbability = random.NextDouble();

                    for (int j = 0; j < roulette.Count - 1; j++)
                    {
                        if (stepProbability > roulette[j] && stepProbability < roulette[j + 1])
                        {
                            i = candidates[j];
                            antPath.Add(candidates[j] + 1);
                            break;
                        }
                    }
                }

                Iterations.Add(antPath);

                double deltaF = q / Distance(antPath.ToArray(), distancetable);

                for (int j = 0; j < antPath.Count - 1; j++)
                {
                    feromoneTable[antPath[j] - 1, antPath[j + 1] - 1] = (oblivionNumber) * feromoneTable[antPath[j] - 1, antPath[j + 1] - 1] + deltaF;
                }

                counter--;

            }

            return FindMinPath(Iterations, distancetable);

        }

        private double Attractiveness(int i, int j, float[,] distancetable, double[,] feromoneTable, double alpha, double beta)
        {
            return Math.Pow(feromoneTable[i, j], beta) / Math.Pow(distancetable[i, j], alpha);
        }

        private bool[] InitializeVisited(int verticesCount)
        {
            bool[] visited = new bool[verticesCount];
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = false;
            }
            return visited;
        }

        private double[,] InitializeFeromoneTable(int verticesCount)
        {
            double[,] table = new double[verticesCount, verticesCount];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        table[i, j] = 0;
                    }
                    else
                    {
                        table[i, j] = 1;
                    }
                }
            }
            return table;
        }
        #endregion

        #region BranchesAndBoundaries
        internal double BranchesAndBoundariesTimer()
        {
            float[,] distanceTableReorganized = ReorganizeDistanceTable(DistanceTable, int.MaxValue);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            BranchesAndBoundariesResultPath = BranchesAndBoundaries(Graph, distanceTableReorganized);

            stopwatch.Stop();

            BranchesAndBoundariesResultPathLength = Distance(BranchesAndBoundariesResultPath.ToArray(), DistanceTable);
            return stopwatch.ElapsedMilliseconds;
        }

        private class SolutionEdge<T>
        {
            /// <summary>
            /// Вес ребра решения
            /// </summary>
            public T Weight { get; set; }

            /// <summary>
            /// Исходящая вершина
            /// </summary>
            public int VertexIn { get; set; }

            /// <summary>
            /// Входящая вершина
            /// </summary>
            public int VertexOut { get; set; }

            public SolutionEdge() { }
            public SolutionEdge(T weight, int vertexOut, int vertexIn)
            {
                Weight = weight;
                VertexIn = vertexIn;
                VertexOut = vertexOut;
            }
        }

        private class SolutionNode<T>
        {
            /// <summary>
            /// Количество узлов
            /// </summary>
            public int Nodes { get; set; }

            /// <summary>
            /// Значение узла
            /// </summary>
            public T Value { get; set; }

            /// <summary>
            /// Является ли корнем
            /// </summary>
            public bool IsRoot { get; set; }

            /// <summary>
            /// Ветвь без ребра
            /// </summary>
            public SolutionNode<T> CutEdge { get; set; }

            /// <summary>
            /// Ветвь с ребром
            /// </summary>
            public SolutionNode<T> UncutEdge { get; set; }

            /// <summary>
            /// Родитель узла
            /// </summary>
            public SolutionNode<T> Parent { get; set; }

            /// <summary>
            /// Порядок матрицы расстояний
            /// </summary>
            public int NumberOfVertices { get => Matrix.GetLength(0); }

            /// <summary>
            /// Матрица расстояний
            /// </summary>
            public T[,] Matrix { get; set; }

            /// <summary>
            /// Доступные в строке матрицы расстояний вершины
            /// </summary>
            public List<int> RowVertices { get; set; }

            /// <summary>
            /// Доступные в столбце матрицы расстояний вершины
            /// </summary>
            public List<int> ColumnVertices { get; set; }

            /// <summary>
            /// Список вычеркнутых рёбер в узле
            /// </summary>
            public List<SolutionEdge<T>> CutEdgesList { get; set; }

            public SolutionNode() { }
            public SolutionNode(T item) { Value = item; }
            public SolutionNode(T item, T[,] matrix) : this(item) { Matrix = matrix; }
        }

        public List<int> BranchesAndBoundaries(MyGraph graph,float[,] distanceTable)
        {
            //Reduce Matrix
            float[] rowDeltas = GetRowDeltas(distanceTable);
            float[,] rowsReduced = ReduceMatrixRows(distanceTable, rowDeltas);

            float[] columnDeltas = GetColumnDeltas(rowsReduced);
            float[,] reduced = ReduceMatrixColumns(rowsReduced, columnDeltas);

            //Calc root score
            float rootScore = BottomScore(0, rowDeltas, columnDeltas);

            //Initialize solutionTree


            SolutionNode<float> solutionTree = new SolutionNode<float>(rootScore, reduced)
            {
                IsRoot = true,
                Nodes = 1,
            };

            List<int> vertices = new List<int>();
            for (int i = 0; i < graph.GraphVertices.Count; i++)
            {
                vertices.Add(i);
            }
            solutionTree.RowVertices = vertices;
            solutionTree.ColumnVertices = new List<int>(vertices);
            solutionTree.CutEdgesList = new List<SolutionEdge<float>>();

            SolutionNode<float> currentNode = solutionTree;
            float parentValue = rootScore;


            int counter = 0;

            while (reduced.GetLength(0) != 1)
            {
                //Reduce matrix
                rowDeltas = GetRowDeltas(reduced);
                rowsReduced = ReduceMatrixRows(reduced, rowDeltas);

                columnDeltas = GetColumnDeltas(rowsReduced);
                reduced = ReduceMatrixColumns(rowsReduced, columnDeltas);

                //Find maximum zero score
                List<List<float>> zeroScores = ZeroScores(reduced);
                List<float> maxZeroScore = FindMaxScore(zeroScores);

                //Create new branches - no way to max zero score vertex
                reduced[(int)maxZeroScore[1], (int)maxZeroScore[2]] = int.MaxValue;

                float[,] reducedWithoutEdge = CutRowAndColumnFromMatrix(reduced, currentNode, (int)maxZeroScore[1], (int)maxZeroScore[2]);
                
                //Reduce matrix without max zero score edge
                float[] cutRowDeltas = GetRowDeltas(reducedWithoutEdge);
                reducedWithoutEdge = ReduceMatrixRows(reducedWithoutEdge, cutRowDeltas);
                float[] cutColumnDeltas = GetColumnDeltas(reducedWithoutEdge);
                reducedWithoutEdge = ReduceMatrixColumns(reducedWithoutEdge, cutColumnDeltas);

                //Find scores
                float cutScore = BottomScore(parentValue, cutRowDeltas, cutColumnDeltas);
                currentNode.CutEdge = new SolutionNode<float>(cutScore, reducedWithoutEdge) 
                {
                    Parent = currentNode,
                };

                int cutRowVertexTrueIndex = currentNode.RowVertices[(int)maxZeroScore[1]];
                int cutColumnVertexTrueIndex = currentNode.ColumnVertices[(int)maxZeroScore[2]];

                currentNode.CutEdge.CutEdgesList = new List<SolutionEdge<float>>(currentNode.CutEdgesList)
                {
                    new SolutionEdge<float>(distanceTable[cutRowVertexTrueIndex, cutColumnVertexTrueIndex], cutRowVertexTrueIndex, cutColumnVertexTrueIndex)
                };

                List<int> cutRows = new List<int>(currentNode.RowVertices);
                cutRows.RemoveAt((int)maxZeroScore[1]);
                currentNode.CutEdge.RowVertices = cutRows;

                List<int> cutColumns = new List<int>(currentNode.ColumnVertices);
                cutColumns.RemoveAt((int)maxZeroScore[2]);
                currentNode.CutEdge.ColumnVertices = cutColumns;


                float uncutScore = parentValue + maxZeroScore[0];
                currentNode.UncutEdge = new SolutionNode<float>(uncutScore, reduced)
                {
                    Parent = currentNode,
                    CutEdgesList = new List<SolutionEdge<float>>(currentNode.CutEdgesList),
                    RowVertices = new List<int>(currentNode.RowVertices),
                    ColumnVertices = new List<int>(currentNode.ColumnVertices)
                };

                solutionTree.Nodes += 2;

                //Find minimumLeaf
                currentNode = FindMinLeaf(solutionTree);
                reduced = currentNode.Matrix;
                parentValue = currentNode.Value;
                counter++;
            }

            int lastRowVertex = currentNode.RowVertices[0];
            int lastColumnVertex = currentNode.ColumnVertices[0];

            currentNode.CutEdgesList.Add(new SolutionEdge<float>(distanceTable[lastRowVertex,lastColumnVertex],lastRowVertex,lastColumnVertex));

            return GetPath(currentNode.CutEdgesList);
        }

        private List<int> GetPath<T>(List<SolutionEdge<T>> cutEdgesList)
        {
            List<int> path = new List<int>();

            int startIndex = FindVertexOut(0,cutEdgesList);
            SolutionEdge<T> currentEdge = cutEdgesList[startIndex];
            path.Add(currentEdge.VertexOut+1);
            path.Add(currentEdge.VertexIn + 1);
            int nextVertexIndex = startIndex;

            while (currentEdge.VertexIn != cutEdgesList[startIndex].VertexOut)
            {
                nextVertexIndex = FindVertexOut(currentEdge.VertexIn, cutEdgesList);
                currentEdge = cutEdgesList[nextVertexIndex];
                path.Add(currentEdge.VertexIn+1);
            }

            return path;
        }

        private int FindVertexOut<T>(int v, List<SolutionEdge<T>> cutEdgesList)
        {
            for (int i = 0; i < cutEdgesList.Count; i++)
            {
                if (cutEdgesList[i].VertexOut == v)
                {
                    return i;
                }
            }
            return -1;
        }


        /// <summary>
        /// Put "infinity" in cells with zero - no path between vertices
        /// </summary>
        /// <param name="distanceTable"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private float[,] ReorganizeDistanceTable(float[,] distanceTable, int maxValue)
        {
            float[,] reorganized = new float[distanceTable.GetLength(0), distanceTable.GetLength(1)];

            for (int i = 0; i < reorganized.GetLength(0); i++)
            {
                for (int j = 0; j < reorganized.GetLength(1); j++)
                {
                    reorganized[i, j] = distanceTable[i, j] == 0 ? maxValue : distanceTable[i, j];
                    
                }
            }

            return reorganized;
        }

        private SolutionNode<T> FindMinLeaf<T>(SolutionNode<T> node) where T: IComparable<T>
        {
            List<SolutionNode<T>> leafs = new List<SolutionNode<T>>();
            leafs.AddRange(CollectLeafs(node));

            T minimum = leafs[0].Value;
            SolutionNode<T> minLeaf = leafs[0];

            foreach (SolutionNode<T> leaf in leafs)
            {
                if (leaf.Value.CompareTo(minimum)<0)
                {
                    minimum = leaf.Value;
                    minLeaf = leaf;
                }
                else if(leaf.Value.CompareTo(minimum) == 0)
                {
                    if(leaf.NumberOfVertices < minLeaf.NumberOfVertices)
                    {
                        minimum = leaf.Value;
                        minLeaf = leaf;
                    }
                }
            }

            return minLeaf;
        }

        private IEnumerable<SolutionNode<T>> CollectLeafs<T>(SolutionNode<T> node)
        {
            List<SolutionNode<T>> values = new List<SolutionNode<T>>();

            SolutionNode<T> currentNode = node;

            Queue<SolutionNode<T>> queue = new Queue<SolutionNode<T>>();
            do
            {
                if(currentNode.CutEdge == null && currentNode.UncutEdge == null)
                {
                    values.Add(currentNode);
                }

                if(currentNode.CutEdge!=null) queue.Enqueue(currentNode.CutEdge);
                if(currentNode.UncutEdge!=null) queue.Enqueue( currentNode.UncutEdge);
                if (queue.Count != 0)
                {
                    currentNode = queue.Dequeue();
                }
                else
                {
                    break;
                }


            } while (true);

            return values;
        }

        private List<float> FindMaxScore(List<List<float>> scores)
        {
            float max = scores[0][0];
            int maxIndex = 0;
            for (int i = 1; i < scores.Count; i++)
            {
                if (scores[i][0] > max)
                {
                    max = scores[i][0];
                    maxIndex = i;
                }
            }

            return scores[maxIndex];
        }

        /// <summary>
        /// Find smallest number in each row
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private float[] GetRowDeltas(float[,] matrix)
        {
            List<float> deltas = new List<float>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                deltas.Add(FindRowMinimum(matrix,i));
            }
            return deltas.ToArray();
        }
        /// <summary>
        /// Find smallest number in each column
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private float[] GetColumnDeltas(float[,] matrix)
        {
            List<float> deltas = new List<float>();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                deltas.Add(FindColumnMinimum(matrix, i));
            }
            return deltas.ToArray();
        }

        private List<List<float>> ZeroScores(float[,] matrix)
        {
            List<List<float>> scores = new List<List<float>>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j] == 0)
                    {
                        float score = FindRowMinimumZeroScore(matrix,i,j) + FindColumnMinimumZeroScore(matrix,i,j);
                        scores.Add(new List<float> { score,i,j});
                    }
                }
            }

            return scores;
        }

        private float FindColumnMinimumZeroScore(float[,] matrix, int row, int column)
        {
            float min = int.MaxValue;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i != row)
                {
                    if (matrix[i, column] < min && matrix[i, column] != int.MaxValue)
                    {
                        min = matrix[i, column];
                    } 
                }
            }

            return min;
        }

        private float FindRowMinimumZeroScore(float[,] matrix, int row, int column)
        {
            float min = int.MaxValue;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (i != column)
                {
                    if (matrix[row,i] < min && matrix[row,i] != int.MaxValue)
                    {
                        min = matrix[row,i];
                    }
                }
            }

            return min;
        }

        private float BottomScore(float rootScore, float[] rowDeltas, float[] columnDeltas)
        {
            float score = rootScore;

            for (int i = 0; i < rowDeltas.Length; i++)
            {
                score += rowDeltas[i] + columnDeltas[i];
            }

            return score;
        }

        /// <summary>
        /// Sustract column delta from each column item
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        private float[,] ReduceMatrixColumns(float[,] matrix, float[] delta)
        {
            float[,] reduced = new float[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < reduced.GetLength(0); i++)
            {
                for (int j = 0; j < reduced.GetLength(1); j++)
                {
                    if (matrix[i, j] != int.MaxValue)
                    {
                        reduced[i, j] = matrix[i, j] - delta[j];
                    }
                    else
                    {
                        reduced[i, j] = int.MaxValue;
                    }
                }
            }

            return reduced;
        }

        /// <summary>
        /// Substract row delta from each row item
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        private float[,] ReduceMatrixRows(float[,] matrix, float[] delta)
        {
            float[,] reduced = new float[matrix.GetLength(0),matrix.GetLength(1)];

            for (int i = 0; i < reduced.GetLength(0); i++)
            {
                for (int j = 0; j < reduced.GetLength(1); j++)
                {
                    if (matrix[i, j] != int.MaxValue)
                    {
                        reduced[i,j] = matrix[i, j]-delta[i];
                    }
                    else
                    {
                        reduced[i, j] = int.MaxValue;
                    }
                }
            }

            return reduced;
        }

        private float FindRowMinimum(float[,] matrix, int row)
        {
            float min = int.MaxValue;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[row, i] < min)
                {
                    min = matrix[row, i];
                }
            }
            return min;
        }

        private float FindColumnMinimum(float[,] matrix, int column)
        {
            float min = int.MaxValue;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i,column] < min)
                {
                    min = matrix[i,column];
                }
            }
            return min;
        }

        private float[,] CutRowAndColumnFromMatrix(float[,] matrix, SolutionNode<float> node, int row, int column)
        {
            float[,] prepared = PrepareMatrix(matrix, node, row, column);

            float[,] cut = new float[prepared.GetLength(0) - 1, prepared.GetLength(1) - 1];

            for (int i = 0; i < prepared.GetLength(0); i++)
            {
                if (i != row)
                {
                    for (int j = 0; j < prepared.GetLength(1); j++)
                    {
                        if (j != column)
                        {
                            int rowDelta = i > row ? 1 : 0;
                            int columnDelta = j > column ? 1 : 0;

                            cut[i - rowDelta, j - columnDelta] = prepared[i, j];

                        }
                    }
                }
            }

            return cut;
        }

        private float[,] PrepareMatrix(float[,] matrix, SolutionNode<float> node, int row, int column)
        {
            float[,] prepared = new float[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < prepared.GetLength(0); i++)
            {
                for (int j = 0; j < prepared.GetLength(1); j++)
                {
                    prepared[i, j] = matrix[i, j];
                }
            }

            int rowTrueIndex = node.RowVertices.FindIndex(x => x == node.ColumnVertices[column]);
            int columnTrueIndex = node.ColumnVertices.FindIndex(x => x== node.RowVertices[row]);

            if(rowTrueIndex != -1 && columnTrueIndex != -1)
            {
                prepared[rowTrueIndex, columnTrueIndex] = int.MaxValue;
            }
            return prepared;
        }
        #endregion

        #endregion


    }
}
