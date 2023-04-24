using AllAboutGraph.MVC.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
        public MyGraph Graph { get => _graph; set => _graph = value; }
        public float[,] DistanceTable { get => _distanceTable; set => _distanceTable = value; }

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
            DistanceTable = Graph.GetDistanceTable();
        }

        #endregion

        #region Methods

        #region Other
        private float Distance(int[] path, float[,] distanceTable)
        {
            float distance = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                distance += distanceTable[path[i] - 1, path[i + 1] - 1];
            }
            return distance;
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
            if (graph.GraphVertices.Count >= 11) return new List<int>();
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

        #region ImprovaedNearestNeighbour
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

            SimulatedAnnealingResultPath = SimulatedAnnealing(Graph, DistanceTable, 10,0.00001);

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

        #region BranchesAndBoundaries
        internal double BranchesAndBoundariesTimer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            BranchesAndBoundariesResultPath = BranchesAndBoundaries(Graph, DistanceTable);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> BranchesAndBoundaries(MyGraph graph, float[,] distanceTable)
        {
            float[,] distanceTableReorganized = ReorganizeDistanceTable(distanceTable,int.MaxValue);

            float[] rowDeltas = GetRowDeltas(distanceTableReorganized);
            float[,] rowsReduced = ReduceMatrixRows(distanceTableReorganized, rowDeltas);

            float[] columnDeltas = GetColumnDeltas(rowsReduced);
            float[,] reduced = ReduceMatrixColumns(rowsReduced, columnDeltas);

            float rootScore = BottomScore(0, rowDeltas, columnDeltas);
            MyGraph decisionTree = new MyGraph();
            int parentVertexIndex = 0; 
            
            decisionTree.AddVertex(new GraphVertex("init"));
            decisionTree.AddVertex(new GraphVertex("root"));
            decisionTree.AddEdge(new GraphEdge(decisionTree.GraphVertices[0], decisionTree.GraphVertices[1], rootScore, true));
            parentVertexIndex++;
            decisionTree.LinkVertices(parentVertexIndex - 1, parentVertexIndex, parentVertexIndex - 1);

            List<float[,]> reducedMatrices = new List<float[,]>();
            reducedMatrices.Add(distanceTableReorganized);
            reducedMatrices.Add(distanceTableReorganized);
            bool needReduce = false;

            List<List<int>> trees = new List<List<int>>();
            trees.Add();

            //не переключается на новую ветку в ветке "uncut" вероятно из-за того, что нолик не пропадает
            //Возврат к первому разветвлению на "uncut" ведёт к забыванию того, что мы тут были уже

            while (reduced.GetLength(0) != 0)
            {
                //Reduce if uncut chosen
                if (needReduce)
                {
                    rowDeltas = GetRowDeltas(reduced);
                    rowsReduced = ReduceMatrixRows(reduced, rowDeltas);

                    columnDeltas = GetColumnDeltas(rowsReduced);
                    reduced = ReduceMatrixColumns(rowsReduced, columnDeltas);
                }

                List<List<float>> zeroScores = ZeroScores(reduced);
                List<float> maxZeroScore = FindMaxScore(zeroScores);

                reduced[(int)maxZeroScore[1], (int)maxZeroScore[2]] = int.MaxValue;

                float[,] reducedWithoutEdge = CutRowAndColumnFromMatrix(reduced, (int)maxZeroScore[1], (int)maxZeroScore[2]);
                
                float[] cutRowDeltas = GetRowDeltas(reducedWithoutEdge);
                reducedWithoutEdge = ReduceMatrixRows(reducedWithoutEdge, cutRowDeltas);
                float[] cutColumnDeltas = GetColumnDeltas(reducedWithoutEdge);
                reducedWithoutEdge = ReduceMatrixColumns(reducedWithoutEdge, cutColumnDeltas);

                float cutScore = BottomScore(decisionTree.GraphEdges[parentVertexIndex-1].Weight, cutRowDeltas, cutColumnDeltas);
                float uncutScore = decisionTree.GraphEdges[parentVertexIndex-1].Weight + maxZeroScore[0];


                reducedMatrices.Add(reducedWithoutEdge);
                reducedMatrices.Add(reduced);

                decisionTree.AddVertex(new GraphVertex("cut " + graph.GraphVertices[(int)maxZeroScore[1]].Name + "-" + graph.GraphVertices[(int)maxZeroScore[2]].Name));
                decisionTree.AddVertex(new GraphVertex("uncut " + graph.GraphVertices[(int)maxZeroScore[1]].Name + "-" + graph.GraphVertices[(int)maxZeroScore[2]].Name));

                decisionTree.AddEdge(new GraphEdge(decisionTree.GraphVertices[parentVertexIndex], decisionTree.GraphVertices[decisionTree.GraphVertices.Count-2],cutScore,true));
                decisionTree.AddEdge(new GraphEdge(decisionTree.GraphVertices[parentVertexIndex], decisionTree.GraphVertices[decisionTree.GraphVertices.Count-1], uncutScore, true));

                decisionTree.LinkVertices(parentVertexIndex, decisionTree.GraphVertices.Count - 2, decisionTree.GraphEdges.Count - 2);
                decisionTree.LinkVertices(parentVertexIndex,decisionTree.GraphVertices.Count-1, decisionTree.GraphEdges.Count - 1);

                int minLeafIndex = FindMinLeaf(decisionTree);

                parentVertexIndex = minLeafIndex;
                float[,] previousReduced = reduced;
                reduced = reducedMatrices[parentVertexIndex];

                //Uncut branch chosen
                if(reduced.GetLength(0) == previousReduced.GetLength(0))
                {
                    needReduce = true;
                }
                else
                {
                    needReduce = false;
                }
            }
            
            

            return new List<int>();
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

        private int FindMinLeaf(MyGraph decisionTree)
        {
            float min = int.MaxValue;
            int minIndex = 0;

            int i = 0;
            foreach(GraphVertex vertex in decisionTree.GraphVertices)
            {
                if (vertex.OutEdges.Count == 0)
                {
                    if (vertex.InEdges[0].Weight <= min)
                    {
                        min = vertex.InEdges[0].Weight;
                        minIndex = i;
                    }
                }
                i++;
            }

            return minIndex;
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

        private float[,] CutRowAndColumnFromMatrix(float[,] matrix,int row,int column)
        {
            float[,] prepared = PrepareMatrix(matrix, row, column);

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

        private float[,] PrepareMatrix(float[,] matrix, int row, int column)
        {
            float[,] prepared = new float[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < prepared.GetLength(0); i++)
            {
                for (int j = 0; j < prepared.GetLength(1); j++)
                {
                    prepared[i, j] = matrix[i, j];
                }
            }

            prepared[column, row] = int.MaxValue;
            return prepared;
        }
        #endregion

        #region AntColony
        internal double AntColonyAlgorithmTimer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            AntColonyAlgorithmResultPath = AntColonyAlgorithm(Graph, DistanceTable,10,1,1,1,1);

            stopwatch.Stop();
            AntColonyAlgorithmResultPathLength = Distance(AntColonyAlgorithmResultPath.ToArray(), DistanceTable);
            return stopwatch.ElapsedMilliseconds;
        }

        private List<int> AntColonyAlgorithm(MyGraph graph, float[,] distancetable, int itetationsCount, double alpha,double beta, double q,double oblivionNumber)
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
                antPath.Add(i+1);
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
                            attractiveness.Add(Attractiveness(i,j,distancetable,feromoneTable,alpha,beta));
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

                    for (int j = 0; j < roulette.Count-1; j++)
                    {
                        if (stepProbability > roulette[j] && stepProbability < roulette[j+1])
                        {
                            i = candidates[j];
                            antPath.Add(candidates[j]+1);
                            break;
                        }
                    }
                }

                Iterations.Add(antPath);

                double deltaF = q / Distance(antPath.ToArray(), distancetable);

                for (int j = 0; j < antPath.Count-1; j++)
                {
                    feromoneTable[antPath[j]-1, antPath[j + 1]-1] = (oblivionNumber) * feromoneTable[antPath[j] - 1, antPath[j + 1] - 1] + deltaF;
                }

                counter--;

            }

            return FindMinPath(Iterations,distancetable);

        }

        private double Attractiveness(int i, int j, float[,] distancetable, double[,] feromoneTable,double alpha, double beta)
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
            double[,] table = new double[verticesCount,verticesCount];
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
        #endregion


    }
}
