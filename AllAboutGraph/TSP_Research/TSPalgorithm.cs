using AllAboutGraph.MVC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace TSP_Research
{
    internal class TSPalgorithm
    {
        #region Fields
        MyGraph _graph;

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
        public MyGraph Graph { get => _graph; set => _graph = value; }

        #region Results
        public List<int> FullSearchResultPath { get => _fullSearchResultPath; set => _fullSearchResultPath = value; }
        public List<int> RandomFullSearchResultPath { get => _randomFullSearchresultPath; set => _randomFullSearchresultPath = value; }
        public List<int> NearestNeighbourResultPath { get => _nearestNeighbourResultPath; set => _nearestNeighbourResultPath = value; }
        public List<int> ImprovedNearestNeighbourResultPath { get => _improvedNearestNeighbourResultPath; set => _improvedNearestNeighbourResultPath = value; }
        public List<int> SimulatedAnnealingResultPath { get => _simulatedAnnealingResultPath; set => _simulatedAnnealingResultPath = value; }
        public List<int> BranchesAndBoundariesResultPath { get => _branchesAndBoundariesResultPath; set => _branchesAndBoundariesResultPath = value; }
        public List<int> AntColonyAlgorithmResultPath { get => _antColonyAlgorithmResultPath; set => _antColonyAlgorithmResultPath = value; }
        public float FullSearchResultPathLength { get => _fullSearchResultPathLength; set => _fullSearchResultPathLength = value; }
        public float RandomFullSearchresultPathLength { get => _randomFullSearchresultPathLength; set => _randomFullSearchresultPathLength = value; }
        public float NearestNeighbourResultPathLength { get => _nearestNeighbourResultPathLength; set => _nearestNeighbourResultPathLength = value; }
        public float ImprovedNearestNeighbourResultPathLength { get => _improvedNearestNeighbourResultPathLength; set => _improvedNearestNeighbourResultPathLength = value; }
        public float SimulatedAnnealingResultPathLength { get => _simulatedAnnealingResultPathLength; set => _simulatedAnnealingResultPathLength = value; }
        public float BranchesAndBoundariesResultPathLength { get => _branchesAndBoundariesResultPathLength; set => _branchesAndBoundariesResultPathLength = value; }
        public float AntColonyAlgorithmResultPathLength { get => _antColonyAlgorithmResultPathLength; set => _antColonyAlgorithmResultPathLength = value; }
        #endregion

        #endregion

        #region Constructors
        public TSPalgorithm(MyGraph graph)
        {
            Graph = graph;
        }

        internal float FullSearchTimer()
        {
            float[,] distanceTable = Graph.GetDistanceTable();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            FullSearchResultPath = FullSearch(Graph,distanceTable);

            if (FullSearchResultPath.Count == 0) return 0;

            stopwatch.Stop();

            FullSearchResultPathLength = Distance(FullSearchResultPath.ToArray(),distanceTable);
            return (float)stopwatch.Elapsed.TotalSeconds;
        }

        private List<int> FullSearch(MyGraph graph, float[,] distanceTable)
        {
            if (graph.GraphVertices.Count >= 15) return new List<int>();
            List<int[]> paths = GetAllPossiblePaths(graph.GraphVertices);
            int[] minPath = FindMinPath(paths, distanceTable);
            return new List<int>(minPath);
        }

        static List<int[]> permutations = new List<int[]>();
        private List<int[]> GetAllPossiblePaths(List<GraphVertex> graphVertices)
        {
            permutations = new List<int[]>();
            int[] permutationRow = new int[graphVertices.Count];
            for (int i = 0; i < graphVertices.Count; i++)
            {
                permutationRow[i] = int.Parse(graphVertices[i].Name);
            }

            Permutate(permutationRow, 0);

            return permutations;
        }

        private void Permutate(int[] row, int start)
        {
            if(start >= row.Length)
            {
                int[] nextPermutation = new int[row.Length];

                for (int i = 0; i < nextPermutation.Length; i++)
                {
                    nextPermutation[i] = row[i];
                }

                permutations.Add(nextPermutation);
                return;
            }

            Permutate(row, start+1);
            for (int i = start+1; i < row.Length; i++)
            {
                Swap(row, start, i);
                Permutate(row, start+1);
                Swap(row, start, i);
            }

        }

        private void Swap(int[] row, int start, int i)
        {
            int tmp = row[i];
            row[i] = row[start];
            row[start] = tmp;
        }

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

        private float Distance(int[] path,float[,] distanceTable)
        {
            float distance = 0;
            for (int i = 0; i < path.Length-1; i++)
            {
                distance += distanceTable[path[i]-1,path[i+1]-1];
            }
            return distance;
        }

        internal float NearestNeighbourTimer()
        {
            float[,] distanceTable = Graph.GetDistanceTable();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            NearestNeighbourResultPath = NearestNeighbour(Graph, 0, distanceTable);

            stopwatch.Stop();
            NearestNeighbourResultPathLength = Distance(NearestNeighbourResultPath.ToArray(),distanceTable);
            return (float)stopwatch.Elapsed.TotalSeconds*1000;
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
            float shortestEdge = float.MaxValue;
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

        internal float ImprovedNearestNeighbourTimer()
        {
            float[,] distanceTable = Graph.GetDistanceTable();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ImprovedNearestNeighbourResultPath = ImprovedNearestNeighbour(Graph, distanceTable);

            stopwatch.Stop();
            ImprovedNearestNeighbourResultPathLength = Distance(ImprovedNearestNeighbourResultPath.ToArray(), distanceTable);
            return stopwatch.ElapsedMilliseconds;
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

        internal float SimulatedAnnealingTimer()
        {
            float[,] distanceTable = Graph.GetDistanceTable();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SimulatedAnnealingResultPath = SimulatedAnnealing(Graph, distanceTable, 10,0.00001);

            stopwatch.Stop();
            SimulatedAnnealingResultPathLength = Distance(SimulatedAnnealingResultPath.ToArray(),distanceTable);
            return stopwatch.ElapsedMilliseconds;
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

        internal float BranchesAndBoundariesTimer()
        {
            float[,] adjacencyMatrix = Graph.AdjacencyMatrix;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            BranchesAndBoundariesResultPath = BranchesAndBoundaries(Graph, adjacencyMatrix);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> BranchesAndBoundaries(MyGraph graph, float[,] adjacencyMatrix)
        {
            return new List<int>();
        }

        internal float AntColonyAlgorithmTimer()
        {
            float[,] adjacencyMatrix = Graph.AdjacencyMatrix;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            AntColonyAlgorithmResultPath = AntColonyAlgorithm(Graph, adjacencyMatrix);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> AntColonyAlgorithm(MyGraph graph, float[,] adjacencyMatrix)
        {
            return new List<int>();
        }
        #endregion

        #region Methods

        #endregion

    }
}
