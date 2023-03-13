using AllAboutGraph.MVC.Controller;
using AllAboutGraph.MVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace AllAboutGraph
{
    public partial class MainView : Form, IViewModel
    {
        #region Constants
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

        string[] graphAlgorithms = new string[]
        {
            "10.1 Breadth-first search",
            "10.2 Depth-first search",
            "10.3 Print all paths",
            "10.4 All paths weights",
            "10.5 Precedence subgraph",
            "10.6 Bracket structure",
            "10.7 Vertex visit time",
            "11. Strongly connected components",
            "12.1. Find Euler cycle",
            "12.2. Fleury`s algorithm",
            "12.3. Find Hamiltonian cycle",
            "12.4. Algebraic method for Hamiltonian cycle",
            "12.5. Roberts and Flores algorithm",
            "12.6. Improved Roberts and Flores algorithm",
            "12.7. Multichain method",
            "12.8. Fundamental set of cycles",
            "1.1. Kruskal`s algorithm",
            "1.2. Prim`s algorithm",
            "2. Dijkstra`s algorithm",
            "3. Floyd`s algorithm",
            "4. Bellman-Ford`s algorithm"
        };

        private Controller _controller;

        //Painting fields
        private Pen _selectedPen;
        private Pen _highlightPen;
        private Brush _selectedVertexBackGroundBrush;
        private Brush _selectedFontBrush;
        private Font _selectedFont;
        private StringFormat _selectedStringFormat;

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

        #endregion

        #region Properties
        #region Controller
        public Controller Controller
        {
            get { return _controller; }
            set { _controller = value; }
        }
        #endregion

        #region Painting
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
        #endregion

        #region Initialization
        public void SetController(Controller controller)
        {
            _controller = controller;
        }

        public MainView()
        {
            InitializeComponent();

            SetDefaultPaintingProperties();
            InitializeCreationMethodsComboBox(creationMethods);
            InitializeGraphAlgorithmsComboBox(graphAlgorithms);
            InitializeCanvas();
        }


        private void SetDefaultPaintingProperties()
        {
            SetDefaultPen();
            SetHighlightPen();
            SetDefaultFont();
            SetDefaultFontBrush();
            SetDefaultVertexBackgroundBrush();
            SetDefaultStringFormat();
        }

        #region PaintPropsSetters
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

        #endregion

        private void InitializeCreationMethodsComboBox(string[] creationMethods)
        {
            comboBoxCreationMethodSelector.Items.Clear();
            foreach (string method in creationMethods)
            {
                comboBoxCreationMethodSelector.Items.Add(method);
            }
        }

        private void InitializeGraphAlgorithmsComboBox(string[] graphAlgorithms)
        {
            comboBoxAlgorithms.Items.Clear();
            foreach(string algorithm in graphAlgorithms)
            {
                comboBoxAlgorithms.Items.Add(algorithm);
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
        #region MouseEvents
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

        #endregion

        public Graphics GetSmoothGraphicsFromCanvas()
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
            Controller.Graph.DrawGraph(e.Graphics,SelectedPen,SelectedBackgroundBrush,SelectedFontBrush, SelectedFont, SelectedStringFormat);
        }
        #endregion

        #region CreateGraph
        private void CreateGraphButton_Click(object sender, EventArgs e)
        {
            Canvas.Image = WhitePlaneBitmap;
            Controller.CreateGraph(textBoxGraphRepresentation.Text,comboBoxCreationMethodSelector.Text);

            Canvas.Invalidate();
        }

        #endregion

        private void comboBoxCreationMethodSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreationMethodLabel.Text = comboBoxCreationMethodSelector.Text;
        }

        private void textBoxNumberOfVertices_TextChanged(object sender, EventArgs e)
        {
            Controller.SetNumberOfVertices(textBoxNumberOfVertices.Text);
        }


        private void buttonStartAlgorithm_Click(object sender, EventArgs e)
        {
            Canvas.Image = WhitePlaneBitmap;
            Canvas.Invalidate();
            Controller.BreadthFirstSearch(0,GetSmoothGraphicsFromCanvas(),HighlightPen);
        }

        public void ViewUpdate()
        {
            Canvas.Invalidate();
        }

        private void comboBoxAlgorithms_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAlgorithm = comboBoxAlgorithms.Text;

            Graphics g = GetSmoothGraphicsFromCanvas();

            switch (selectedAlgorithm)
            {
                case "10.1 Breadth-first search":
                    Controller.BreadthFirstSearch(0,g,HighlightPen);
                    break;
                case "10.2 Depth-first search":
                    Controller.DepthFirstSearch(0,g,HighlightPen);
                    break;
                case "10.3 Print all paths":
                    Controller.PrintAllPaths(HighlightPen);
                    break;
                case "10.4 All paths weights":
                    Controller.PrintAllPathsWithWeights(HighlightPen);
                    break;
                case "10.5 Precedence subgraph":
                    Controller.PrintPrecedenceSubgraph(HighlightPen);
                    break;
                case "10.6 Bracket structure":
                    Controller.PrintBracketStructure();
                    break;
                case "10.7 Vertex visit time":
                    Controller.BFSWithTime(0, g, HighlightPen);
                    break;
                case "11. Strongly connected components":
                    Controller.PrintStronglyConnectedComponents(g, HighlightPen);
                    break;
                case "12.1. Find Euler cycle":
                    Controller.FindEulerCycle(g,HighlightPen);
                    break;
                case "12.2. Fleury`s algorithm":
                    Controller.FleuryAlgorithm(g, HighlightPen);
                    break;
                case "12.3. Find Hamiltonian cycle":
                    Controller.FindHamiltonianCycle(g, HighlightPen);
                    break;
                case "12.4. Algebraic method for Hamiltonian cycle":
                    break;
                case "12.5. Roberts and Flores algorithm":
                    Controller.RobertsFloresAlgorithm(g, HighlightPen);
                    break;
                case "12.6. Improved Roberts and Flores algorithm":
                    break;
                case "12.7. Multichain method":
                    Controller.MultichainMethod(g, HighlightPen);
                    break;
                case "12.8. Fundamental set of cycles":
                    Controller.PrintFundamentalSetOfCycles(g, HighlightPen);
                    break;
                case "1.1. Kruskal`s algorithm":
                    Controller.KruskalAlgorithm(g,HighlightPen);
                    break;
                case "1.2. Prim`s algorithm":
                    Controller.PrimAlgoriyhm(g, HighlightPen);
                    break;
                case "2. Dijkstra`s algorithm":
                    Controller.DijkstraAlgorithm(g,HighlightPen);
                    break;
                case "3. Floyd`s algorithm":
                    Controller.FloydAlgorithm(g, HighlightPen);
                    break;
                case "4. Bellman-Ford`s algorithm":
                    Controller.BellmanFordAlgorithm(g, HighlightPen);
                    break;
            }
        }
    }
}
