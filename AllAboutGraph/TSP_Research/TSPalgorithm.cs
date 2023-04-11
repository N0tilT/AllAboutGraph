using AllAboutGraph.MVC.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
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
        #endregion

        #endregion

        #region Constructors
        public TSPalgorithm(MyGraph graph)
        {
            Graph = graph;
        }

        internal float FullSearchTimer()
        {
            int[,] distanceTable = Graph.GetDistanceTable();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            FullSearchResultPath = FullSearch(Graph,distanceTable);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> FullSearch(MyGraph graph, int[,] distanceTable)
        {
            List<int[]> paths = GetAllPossiblePaths(graph.GraphVertices);
            int[] minPath = FindMinPath(paths, distanceTable);
            return new List<int>(minPath);
        }
        private List<int[]> GetAllPossiblePaths(List<GraphVertex> graphVertices)
        {
            throw new NotImplementedException();
        }

        private int[] FindMinPath(List<int[]> paths, int[,] distanceTable)
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

        private float Distance(int[] path,int[,] distanceTable)
        {
            float distance = 0;
            for (int i = 0; i < path.Length-1; i++)
            {
                distance += distanceTable[path[i],path[i+1]];
            }
            return distance;
        }

        internal float RandomFullSearchTimer()
        {
            int[,] adjacencyMatrix = Graph.AdjacencyMatrix;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            RandomFullSearchResultPath = RandomFullSearch(Graph, adjacencyMatrix);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> RandomFullSearch(MyGraph graph, int[,] adjacencyMatrix)
        {
            return new List<int>();
        }

        internal float NearestNeighbourTimer()
        {
            int[,] adjacencyMatrix = Graph.AdjacencyMatrix;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            NearestNeighbourResultPath = NearestNeighbour(Graph, adjacencyMatrix);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> NearestNeighbour(MyGraph graph, int[,] adjacencyMatrix)
        {
            return new List<int>();
        }

        internal float ImprovedNearestNeighbourTimer()
        {
            int[,] adjacencyMatrix = Graph.AdjacencyMatrix;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ImprovedNearestNeighbourResultPath = ImprovedNearestNeighbour(Graph, adjacencyMatrix);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> ImprovedNearestNeighbour(MyGraph graph, int[,] adjacencyMatrix)
        {
            return new List<int>();
        }

        internal float SimulatedAnnealingTimer()
        {
            int[,] adjacencyMatrix = Graph.AdjacencyMatrix;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SimulatedAnnealingResultPath = SimulatedAnnealing(Graph, adjacencyMatrix);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> SimulatedAnnealing(MyGraph graph, int[,] adjacencyMatrix)
        {
            return new List<int>();
        }

        internal float BranchesAndBoundariesTimer()
        {
            int[,] adjacencyMatrix = Graph.AdjacencyMatrix;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            BranchesAndBoundariesResultPath = BranchesAndBoundaries(Graph, adjacencyMatrix);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> BranchesAndBoundaries(MyGraph graph, int[,] adjacencyMatrix)
        {
            return new List<int>();
        }

        internal float AntColonyAlgorithmTimer()
        {
            int[,] adjacencyMatrix = Graph.AdjacencyMatrix;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            AntColonyAlgorithmResultPath = AntColonyAlgorithm(Graph, adjacencyMatrix);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> AntColonyAlgorithm(MyGraph graph, int[,] adjacencyMatrix)
        {
            return new List<int>();
        }
        #endregion

        #region Methods

        #endregion

    }
}
