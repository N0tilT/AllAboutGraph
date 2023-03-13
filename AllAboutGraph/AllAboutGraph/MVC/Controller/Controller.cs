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
                { 1, 1, 0, 0, 1, 0},
                { 1, 0, 1, 1, 0, 1},
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
        private async void DFS(int startIndex, List<int> visitedVertices, Graphics g, Pen pen)
        {
            foreach (int neighbour in Graph.AdjacencyList[startIndex])
            {
                if (!visitedVertices.Contains(neighbour))
                {
                    HighlightEdge(g, pen, startIndex, neighbour);


                    visitedVertices.Add(neighbour);
                    DFS(neighbour, visitedVertices,g ,pen);

                    await MakePause();
                }
            }
        }

        private static async Task MakePause()
        {
            await Task.Delay(1000);
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

        internal void PrintAllPaths(Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void PrintAllPathsWithWeights(Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void PrintPrecedenceSubgraph(Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void PrintBracketStructure()
        {
            throw new NotImplementedException();
        }

        internal void BFSWithTime(int v, Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void PrintStronglyConnectedComponents(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void FindEulerCycle(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void FleuryAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void FindHamiltonianCycle(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void RobertsFloresAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void MultichainMethod(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void PrintFundamentalSetOfCycles(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void KruskalAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void PrimAlgoriyhm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void DijkstraAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void FloydAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }

        internal void BellmanFordAlgorithm(Graphics g, Pen highlightPen)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion



    }
}
