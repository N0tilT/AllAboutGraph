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

            NearestNeighbourResultPath = NearestNeighbour(Graph, distanceTable);

            stopwatch.Stop();
            NearestNeighbourResultPathLength = Distance(NearestNeighbourResultPath.ToArray(),distanceTable);
            return (float)stopwatch.Elapsed.TotalSeconds*1000;
        }


        private List<int> NearestNeighbour(MyGraph graph, float[,] distanceTable)
        {
            int n = graph.GraphVertices.Count;
            bool[] visited = new bool[n];

            List<int> path = new List<int>();


            int nearestVertexIndex = 0;
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
            float[,] adjacencyMatrix = Graph.AdjacencyMatrix;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ImprovedNearestNeighbourResultPath = ImprovedNearestNeighbour(Graph, adjacencyMatrix);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> ImprovedNearestNeighbour(MyGraph graph, float[,] adjacencyMatrix)
        {
            return new List<int>();
        }

        internal float SimulatedAnnealingTimer()
        {
            float[,] adjacencyMatrix = Graph.AdjacencyMatrix;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SimulatedAnnealingResultPath = SimulatedAnnealing(Graph, adjacencyMatrix);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> SimulatedAnnealing(MyGraph graph, float[,] adjacencyMatrix)
        {
            return new List<int>();
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
