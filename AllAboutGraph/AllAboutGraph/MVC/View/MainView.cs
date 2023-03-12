using AllAboutGraph.MVC.Controller;
using AllAboutGraph.MVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllAboutGraph
{
    public partial class MainView : Form
    {
        #region Contants
        private const string matrixMethod = "Adjacency matrix";
        private const string listMethod = "Adjacency list";
        #endregion

        #region Fields
        string[] creationMethods = new string[] 
        {
            matrixMethod,
            listMethod,
            "Default"
        };

        private Controller _controller;
        private Pen _selectedPen;
        private Pen _highlightPen;
        private Brush _selectedVertexBackGroundBrush;
        private Brush _selectedFontBrush;
        private Font _selectedFont;
        private StringFormat _selectedStringFormat;

        private MyGraph graph;
        
        /// <summary>
        /// Точка на экране, где последний раз был курсор
        /// </summary>
        private Point PreviousPoint = MousePosition;

        /// <summary>
        /// Точка на экране, где последний раз произошёл клик мышью
        /// </summary>
        private Point MouseDownLocation;

        /// <summary>
        /// Заливки для холста
        /// </summary>
        private Bitmap _whitePlaneBitmap;

        private int _numOfVertices;

        private int[,] _adjMatrix = new int[,] {
                { 0, 1, 1, 1, 1, 0},
                { 1, 0, 0, 1, 0, 0},
                { 1, 0, 0, 0, 1, 0},
                { 1, 1, 0, 0, 1, 0},
                { 1, 0, 1, 1, 0, 1},
                { 0, 0, 0, 0, 0, 0}
            };
        private bool dataGotSuccessfully = true;
        #endregion

        #region Properties

        /// <summary>
        /// Белый холст
        /// </summary>
        public Bitmap WhitePlaneBitmap
        {
            get
            {
                _whitePlaneBitmap = new Bitmap(Canvas.Width, Canvas.Height);
                Graphics g = Graphics.FromImage(_whitePlaneBitmap);
                g.Clear(Color.White);
                return _whitePlaneBitmap;
            }
        }

        /// <summary>
        /// Доступ к выбранной кисти
        /// </summary>
        public Pen SelectedPen
        {
            get { return _selectedPen; }
            set { _selectedPen = value; }
        }

        public Pen HighlightPen
        {
            get { return _highlightPen; }
            set { _highlightPen = value; }
        }

        public int NumOfVertices
        {
            get { return _numOfVertices; }
            set { _numOfVertices = value; }
        }

        public AdjacencyMatrix TestAdjMatrix
        {
            get { return new AdjacencyMatrix(_adjMatrix); }
        }

        public Brush SelectedBackgroundBrush 
        {
            get { return _selectedVertexBackGroundBrush; }
            set { _selectedVertexBackGroundBrush = value; }
        }
        public Brush SelectedFontBrush
        {
            get { return _selectedFontBrush; }
            set { _selectedFontBrush = value; }
        }

        public Font SelectedFont 
        {
            get {return _selectedFont; }
            set { _selectedFont = value; }
        }

        public StringFormat SelectedStringFormat 
        {
            get { return _selectedStringFormat; }
            set { _selectedStringFormat = value; } 
        }

        #endregion

        #region Initialization
        public void SetController(Controller controller)
        {
            _controller = controller;
        }

        public MainView()
        {
            InitializeComponent();

            graph = new MyGraph()
            {
            };


            SetDefaultPen();
            SetHighlightPen();
            SetDefaultFont();
            SetDefaultFontBrush();
            SetDefaultVertexBackgroundBrush();
            SetDefaultStringFormat();
            InitializeCreationMethodsComboBox(creationMethods);
            InitializeCanvas();
        }

        private void SetDefaultPen()
        {
            SelectedPen = new Pen(Color.Black, 3);
            SelectedPen.SetLineCap(LineCap.Custom, LineCap.Custom, DashCap.Round);
        }

        private void SetHighlightPen()
        {
            HighlightPen = new Pen(Color.Yellow, 7);
            SelectedPen.SetLineCap(LineCap.Custom, LineCap.Custom, DashCap.Round);
        }

        private void SetDefaultVertexBackgroundBrush()
        {
            SelectedBackgroundBrush= new SolidBrush(Color.DarkViolet);
        }

        private void SetDefaultFont()
        {
            SelectedFont = new Font("Segoe UI", 16);
        }

        private void SetDefaultFontBrush()
        {
            SelectedFontBrush = new SolidBrush(Color.White);
        }

        private void SetDefaultStringFormat()
        {
            SelectedStringFormat = new StringFormat();
            SelectedStringFormat.Alignment = StringAlignment.Center;
            SelectedStringFormat.LineAlignment = StringAlignment.Center;
        }

        private void InitializeCreationMethodsComboBox(string[] creationMethods)
        {
            comboBoxCreationMethodSelector.Items.Clear();
            foreach (string method in creationMethods)
            {
                comboBoxCreationMethodSelector.Items.Add(method);
            }
        }

        private void InitializeCanvas()
        {
            Canvas.Image = WhitePlaneBitmap;
        }


        #endregion

        #region CanvasEvents

        private void Canvas_Resize(object sender, EventArgs e)
        {
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Point cursorPosition = GetCurrentCursorPosition();

            //Ставит точки на холсте на месте кликов
            Graphics g = GetSmoothGraphicsFromCanvas();

            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;


                g.DrawEllipse(SelectedPen, cursorPosition.X-25, cursorPosition.Y-25, 50, 50);
            }
            if (e.Button == MouseButtons.Right)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point cursorPosition = GetCurrentCursorPosition();
            Graphics g = GetSmoothGraphicsFromCanvas();

            Canvas.Invalidate();
            PreviousPoint = cursorPosition;
        }

        private Point GetCurrentCursorPosition()
        {
            //Позиция курсора корректируется для корректного отображения изображения.
            //Вычисляются его координаты относительно окна приложения

            int x = MousePosition.X - this.Location.X - 8;
            int y = MousePosition.Y - this.Location.Y - 30;

            return new Point(x, y);
        }
        private Graphics GetSmoothGraphicsFromCanvas()
        {
            Graphics g = Graphics.FromImage(Canvas.Image);
            g.SmoothingMode = SmoothingMode.HighQuality;
            return g;
        }
        
        /// <summary>
        /// Перерисовка холста. Осуществляется при любом обращении к Canvas
        /// </summary>
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            graph.DrawGraph(e.Graphics,SelectedPen,SelectedBackgroundBrush,SelectedFontBrush, SelectedFont, SelectedStringFormat);
        }
        #endregion

        #region CreateGraph
        private void CreateGraphButton_Click(object sender, EventArgs e)
        {
            dataGotSuccessfully= true;

            if(comboBoxCreationMethodSelector.Text == matrixMethod)
            {
                AdjacencyMatrix matrix = GetAdjacencyMatrixFromUser();
                if (dataGotSuccessfully)
                {
                    graph = new MyGraph(matrix);
                }
            }
            else if (comboBoxCreationMethodSelector.Text == listMethod)
            {
                AdjacencyList list = GetAdjacencyListFromUser(); 
                if (dataGotSuccessfully)
                {
                    graph = new MyGraph(list);
                }
            }
            else
            {
                graph = new MyGraph(TestAdjMatrix);
                //MessageBox.Show("Не выбран метод создания графа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Canvas.Invalidate();
        }

        #region GetGraphDataFromUser
        private AdjacencyMatrix GetAdjacencyMatrixFromUser()
        {
            int[,] matrix = new int[0,0];

            try
            {
                matrix = GetMatrixRepresentation(textBoxGraphRepresentation.Text);
                if(matrix.GetLength(0) != matrix.GetLength(1))
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

        private AdjacencyList GetAdjacencyListFromUser()
        {
            List<List<int>> list = new List<List<int>>();

            try
            {
                list = GetListRepresentation(textBoxGraphRepresentation.Text);
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

        private List<List<int>> GetListRepresentation(string userInput)
        {
            List<List<int>> list = new List<List<int>>();

            string[] listRows = GetRepresentationRows(userInput);

            foreach(string row in listRows)
            {
                List<int> intRow = GetIntRowRepresentation(row);
                if(intRow.Count == 0)
                {
                    dataGotSuccessfully= false;
                    return new List<List<int>>();
                }
                list.Add(intRow);
            }

            return list;

        }


        #region InputConvertManipulations
        private string[] GetRepresentationRows(string userInput)
        {
            string[] tmp = new string[0];

            try
            {
                tmp = userInput.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (tmp.Length != NumOfVertices)
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

            string[] splitedRow = row.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            foreach(string item in splitedRow)
            {
                try
                {
                    intRow.Add(int.Parse(item));
                }
                catch
                {
                    MessageBox.Show("Неверно указан элемент представления графа","Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    dataGotSuccessfully = false;
                    return new List<int>();
                }
            }
            return intRow;
        }
        #endregion

        #endregion

        private void comboBoxCreationMethodSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreationMethodLabel.Text = comboBoxCreationMethodSelector.Text;
        }

        private void textBoxNumberOfVertices_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int tmp = int.Parse(textBoxNumberOfVertices.Text);
                if (tmp < 0)
                {
                    throw new Exception();
                }
                NumOfVertices = tmp;
               
            }
            catch
            {
                MessageBox.Show("Неверное число вершин","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        #endregion

        #region GraphAlgorithms

        public void BFS(int startIndex)
        {
            List<int> visitedVertices = new List<int>
            {
                startIndex
            };

            string consoleMessage = "";

            Queue<int> verticesQueue = new Queue<int>();
            verticesQueue.Enqueue(startIndex);

            while (verticesQueue.Count != 0)
            {
                int curVertex = verticesQueue.Dequeue();
                foreach (int neighbour in graph.AdjacencyList[curVertex])
                {
                    if (!visitedVertices.Contains(neighbour))
                    {
                        consoleMessage += Convert.ToString(curVertex) + "->" + Convert.ToString(neighbour) + "; ";

                        GraphEdge curEdge = new GraphEdge();
                        curEdge.VertexOut = graph.GraphVertices[curVertex];
                        curEdge.VertexIn = graph.GraphVertices[neighbour];

                        Graphics g = GetSmoothGraphicsFromCanvas();

                        curEdge.DrawEdge(g,HighlightPen);

                        Canvas.Invalidate();

                        visitedVertices.Add(neighbour);
                        verticesQueue.Enqueue(neighbour);
                    }
                }
            }

            MessageBox.Show(consoleMessage);

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

        private void buttonStartAlgorithm_Click(object sender, EventArgs e)
        {
            BFS(0);
        }
    }
}
