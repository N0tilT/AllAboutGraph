using AllAboutGraph.MVC.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllAboutGraph.MVC.Controller
{
    public class Controller
    {
        #region Constants
        private const string matrixMethod = "Adjacency matrix";
        private const string listMethod = "Adjacency list";
        #endregion

        #region Fields

        private MainView _view;


        #region ModelFields

        private MyGraph _graph;
        private int _numberOfVertices;

        private bool dataGotSuccessfully;

        private int[,] _testAdjMatrix = new int[,] {
                { 0, 1, 1, 1, 1, 0},
                { 1, 0, 0, 1, 0, 0},
                { 1, 0, 0, 0, 1, 0},
                { 1, 0, 0, 0, 1, 0},
                { 1, 0, 1, 0, 0, 1},
                { 0, 0, 0, 0, 0, 0}
            };

        #endregion

        #endregion

        #region Properties

        public MainView View
        {
            get { return _view; }
        }

        public MyGraph Graph
        {
            get { return _graph; }
            set { _graph = value; }
        }

        public int NumberOfVertices
        {
            get { return _numberOfVertices; }
            set { _numberOfVertices = value; }
        }

        public void SetNumberOfVertices(string userInput)
        {
            try
            {
                int tmp = int.Parse(userInput);
                if (tmp < 0)
                {
                    throw new Exception();
                }
                NumberOfVertices = tmp;
            }
            catch
            {
                MessageBox.Show("Неверное число вершин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public AdjacencyMatrix TestAdjMatrix
        {
            get { return new AdjacencyMatrix(_testAdjMatrix); }
        }

        public object Fleury { get; internal set; }
        #endregion

        #region Initialization

        public Controller(MainView view)
        {
            this._view = view;
            view.SetController(this);

            Graph = new MyGraph();
        }

        #endregion

        #region Methods

        #region CreateGraph

        public void CreateGraph(string representation, string creationMethod)
        {
            dataGotSuccessfully = true;

            if (creationMethod == matrixMethod)
            {
                AdjacencyMatrix matrix = GetAdjacencyMatrixFromUser(representation);
                if (dataGotSuccessfully)
                {
                    Graph = new MyGraph(matrix);
                }
            }
            else if (creationMethod == listMethod)
            {
                AdjacencyList list = GetAdjacencyListFromUser(representation);
                if (dataGotSuccessfully)
                {
                    Graph = new MyGraph(list);
                }
            }
            else
            {
                Graph = new MyGraph(TestAdjMatrix);
            }
        }

        private AdjacencyMatrix GetAdjacencyMatrixFromUser(string representation)
        {
            int[,] matrix = new int[0, 0];

            try
            {
                matrix = GetMatrixRepresentation(representation);
                if (matrix.GetLength(0) != matrix.GetLength(1))
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Неверно указана матрица смежности", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGotSuccessfully = false;
                return new AdjacencyMatrix();
            }

            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(matrix);

            return adjacencyMatrix;
        }

        private AdjacencyList GetAdjacencyListFromUser(string representation)
        {
            List<List<int>> list;

            try
            {
                list = GetListRepresentation(representation);
            }
            catch
            {
                MessageBox.Show("Неверно указана матрица смежности", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGotSuccessfully = false;
                return new AdjacencyList();
            }

            AdjacencyList adjacencyList = new AdjacencyList(list);

            return adjacencyList;
        }

        private int[,] GetMatrixRepresentation(string userInput)
        {
            List<int[]> listMatrix = new List<int[]>();

            string[] matrixRows = GetRepresentationRows(userInput);

            foreach (string row in matrixRows)
            {
                listMatrix.Add(GetIntRowRepresentation(row).ToArray());
            }

            int[,] arrayMatrix = ConvertListMatrixToArrayMatrix(listMatrix);

            return arrayMatrix;
        }


        private List<List<int>> GetListRepresentation(string userInput)
        {
            List<List<int>> list = new List<List<int>>();

            string[] listRows = GetRepresentationRows(userInput);

            foreach (string row in listRows)
            {
                List<int> intRow = GetIntRowRepresentation(row);
                if (intRow.Count == 0)
                {
                    dataGotSuccessfully = false;
                    return new List<List<int>>();
                }
                list.Add(intRow);
            }

            return list;
        }
        #region RepresentationConvert
        private string[] GetRepresentationRows(string userInput)
        {
            string[] tmp = new string[0];

            try
            {
                tmp = userInput.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (tmp.Length != NumberOfVertices)
                {
                    throw new Exception();
                }

                return tmp;
            }
            catch
            {
                MessageBox.Show("Нарушено количество строк представления графа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGotSuccessfully = false;
                return new string[0];
            }

        }

        private List<int> GetIntRowRepresentation(string row)
        {
            List<int> intRow = new List<int>();

            string[] splitedRow = row.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in splitedRow)
            {
                try
                {
                    intRow.Add(int.Parse(item));
                }
                catch
                {
                    MessageBox.Show("Неверно указан элемент представления графа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGotSuccessfully = false;
                    return new List<int>();
                }
            }
            return intRow;
        }
        #endregion

        #endregion

        #region GraphAlgorithms

        public async void BreadthFirstSearch(int startIndex, Graphics g, Pen highlightingPen)
        {
            List<int> visitedVertices = new List<int>
            {
                startIndex
            };

            Queue<int> verticesQueue = new Queue<int>();
            verticesQueue.Enqueue(startIndex);

            while (verticesQueue.Count != 0)
            {
                int curVertex = verticesQueue.Dequeue();
                foreach (int neighbour in Graph.AdjacencyList[curVertex])
                {
                    if (!visitedVertices.Contains(neighbour))
                    {
                        HighlightEdge(g, highlightingPen, curVertex, neighbour);

                        visitedVertices.Add(neighbour);
                        verticesQueue.Enqueue(neighbour);

                        await MakePause();
                    }
                }
            }
        }

        public void DepthFirstSearch(int startIndex, Graphics g, Pen pen)
        {
            List<int> visitedVertices = new List<int>();
            visitedVertices.Add(startIndex);
            DFS(startIndex, visitedVertices, g, pen);
        }
        private async Task DFS(int startIndex, List<int> visitedVertices, Graphics g, Pen pen)
        {
            foreach (int neighbour in Graph.AdjacencyList[startIndex])
            {
                if (!visitedVertices.Contains(neighbour))
                {
                    HighlightEdge(g, pen, startIndex, neighbour);

                    visitedVertices.Add(neighbour);

                    await MakePause();
                    await DFS(neighbour, visitedVertices,g ,pen);

                }
            }
        }

        public void PrintAllPaths(int start, int destination,Graphics g,Pen highlightPen)
        {
            bool[] isVisited = new bool[Graph.GraphVertices.Count];
            List<int> pathList = new List<int>();

            pathList.Add(start);

            PrintAllPathsUtil(start, destination, isVisited, pathList,g,highlightPen);
        }

        private void PrintAllPathsUtil(int start, int destination, bool[] isVisited, List<int> localPathList, Graphics g, Pen highlightPen)
        {

            if (start == destination)
            {
                DrawPath(localPathList,g, highlightPen);
                MessageBox.Show(string.Join(" ", localPathList) + " Длина: " + GetPathLength(localPathList));
                g.Clear(Color.White);
                return;
            }

            isVisited[start] = true;

            foreach (int nextVertex in Graph.AdjacencyList[start])
            {
                if (!isVisited[nextVertex])
                {
                    localPathList.Add(nextVertex);
                    PrintAllPathsUtil(nextVertex, destination, isVisited, localPathList,g,highlightPen);
                    localPathList.Remove(nextVertex);
                }
            }

            isVisited[start] = false;
        }

        private void DrawPath(List<int> localPathList,Graphics g, Pen pen)
        {
            for (int i = 0; i < localPathList.Count - 1; i++)
            {
                HighlightEdge(g, pen, localPathList[i], localPathList[i + 1]);
            }
        }

        private int GetPathLength(List<int> path)
        {
            int length = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                length += Graph.AdjacencyMatrix[path[i], path[i + 1]];
            }
            return length;
        }

        //later
        public void PrintPrecedenceSubgraph(Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        //later
        public void PrintBracketStructure()
        {
            throw new NotImplementedException();
        }

        public async void BFSWithTime(int startIndex, Graphics g, Pen highlightPen,Font font, Brush brush, StringFormat format)
        {
            List<int> visitedVertices = new List<int>
            {
                startIndex
            };

            int time = 0;

            Queue<int> verticesQueue = new Queue<int>();
            verticesQueue.Enqueue(startIndex);
            PrintVisitTime(startIndex,time,g, font, brush,format);

            while (verticesQueue.Count != 0)
            {
                int curVertex = verticesQueue.Dequeue();
                foreach (int neighbour in Graph.AdjacencyList[curVertex])
                {
                    if (!visitedVertices.Contains(neighbour))
                    {
                        HighlightEdge(g, highlightPen, curVertex, neighbour);

                        visitedVertices.Add(neighbour);
                        verticesQueue.Enqueue(neighbour);

                        PrintVisitTime(neighbour, ++time, g, font, brush, format);

                        await MakePause();
                    }
                }
            }
        }

        private void PrintVisitTime(int startIndex, int time, Graphics g,Font font,Brush brush, StringFormat format)
        {
            GraphVertex curVertex = Graph.GraphVertices[startIndex];
            PointF vertexCenter = curVertex.Center;
            PointF timePosition = new PointF(vertexCenter.X,vertexCenter.Y-curVertex.Size.Height-5);

            g.DrawString(Convert.ToString(time), font, new SolidBrush(Color.Black),timePosition ,format);
        }

        public void PrintStronglyConnectedComponents(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        public void FindEulerCycle(Graphics g, Pen highlightPen)
        {
            int result = IsEulerian(Graph);
            if (result == 0)
                MessageBox.Show("Graph is not Eulerian");
            else if (result == 1)
                MessageBox.Show("Graph has a Euler path");
            else
                MessageBox.Show("Graph has a Euler cycle");

        }

        private int IsEulerian(MyGraph graph)
        {
            if (!CheckZeroVerticesConnection(graph))
            {
                return 0;
            }

            int oddVertices = CountOddVertices(graph);

            if (oddVertices > 2)
            {
                return 0;
            }

            return (oddVertices == 2) ? 1 : 2;

        }


        private bool CheckZeroVerticesConnection(MyGraph graph)
        {
            int numberOfVertices = Graph.GraphVertices.Count;

            bool[] visited = new bool[numberOfVertices];

            for (int i = 0; i < numberOfVertices; i++)
                visited[i] = false;

            int nonZeroIndex = 0;
            //Find non-zero degree vertex
            for (int i = 0; i < numberOfVertices; i++)
                if (Graph.AdjacencyList[i].Count != 0)
                {
                    nonZeroIndex = i;
                    break;
                }

            //No other edges
            if (nonZeroIndex == numberOfVertices)
                return true;

            // Start DFS traversal from a vertex with non-zero degree
            DFSUtil(nonZeroIndex, visited);

            //Check if all non-zero is visited
            for (int i = 0; i < numberOfVertices; i++)
                if (visited[i] == false && Graph.AdjacencyList[i].Count > 0)
                    return false;

            return true;
        }

        private void DFSUtil(int startVertex, bool[] visited)
        {
            visited[startVertex] = true;

            foreach (int i in Graph.AdjacencyList[startVertex])
            {
                int n = i;
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }

        private int CountOddVertices(MyGraph graph)
        {
            int count = 0;
            for (int i = 0; i < Graph.GraphVertices.Count; i++)
            {
                if (Graph.AdjacencyList[i].Count % 2 != 0)
                {
                    count++;
                }
            }
            return count;
        }

        public void FleuryAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        public void FindHamiltonianCycle(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        public void RobertsFloresAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        public void MultichainMethod(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        public void PrintFundamentalSetOfCycles(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        public void KruskalAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void PrimAlgoriyhm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        public void DijkstraAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        public void FloydAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        public void BellmanFordAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ViewInteractions
        private void HighlightEdge(Graphics g, Pen pen, int vertexOut, int VertexIn)
        {
            GraphEdge curEdge = new GraphEdge();
            curEdge.VertexOut = Graph.GraphVertices[vertexOut];
            curEdge.VertexIn = Graph.GraphVertices[VertexIn];
            curEdge.DrawEdge(g, pen);

            View.ViewUpdate();
        }
        private static async Task MakePause()
        {
            await Task.Delay(1000);
        }
        #endregion

        #region AdditionalMethods
        private int[,] ConvertListMatrixToArrayMatrix(List<int[]> listMatrix)
        {
            int[,] convertedMatrix = new int[listMatrix.Count, listMatrix[0].Length];

            for (int i = 0; i < convertedMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < convertedMatrix.GetLength(1); j++)
                {
                    convertedMatrix[i, j] = listMatrix[i][j];
                }
            }

            return convertedMatrix;
        }
        #endregion

        #endregion



    }
}
