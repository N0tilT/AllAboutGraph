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
using System.Windows.Forms.DataVisualization.Charting;

namespace TSP_Research
{
    /// <summary>
    /// Форма для визуализации алгоритмов решения TSP
    /// </summary>
    public partial class TSPForm : Form
    {
        #region Fields
        private Bitmap _whitePlaneBitmap;

        private Pen _selectedPen;
        private Pen _highlightPen;
        private Brush _selectedVertexBackGroundBrush;
        private Brush _selectedFontBrush;
        private Font _selectedFont;
        private StringFormat _selectedStringFormat;

        private List<MyGraph> graphs;
        private MyGraph _selectedGraph;

        private int verticesCountStep;
        private int maxVerticesCount;
        #endregion
        #region Properties

        /// <summary>
        /// Шаг увеличения количества вершин графа
        /// </summary>
        public int VerticesCountStep { get => verticesCountStep; set => verticesCountStep = value; }

        /// <summary>
        /// Максимальное количество вершин графа
        /// </summary>
        public int MaxVerticesCount { get => maxVerticesCount; set => maxVerticesCount = value; }

        /// <summary>
        /// Список графов
        /// </summary>
        public List<MyGraph> Graphs { get => graphs; set => graphs = value; }
        
        /// <summary>
        /// Текущий обрабатываемый граф
        /// </summary>
        public MyGraph SelectedGraph { get => _selectedGraph; set => _selectedGraph = value; }

        #region Painting
        /// <summary>
        /// Пустая белая заливка для холста
        /// </summary>
        public Bitmap WhitePlaneBitmap
        {
            get
            {
                _whitePlaneBitmap  = new Bitmap(Canvas.Width, Canvas.Height);
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

        /// <summary>
        /// Доступ к кисти выделения ребра
        /// </summary>
        public Pen HighlightPen
        {
            get { return _highlightPen; }
            set { _highlightPen = value; }
        }

        /// <summary>
        /// Доступ к цвету заливки
        /// </summary>
        public Brush SelectedBackgroundBrush
        {
            get { return _selectedVertexBackGroundBrush; }
            set { _selectedVertexBackGroundBrush = value; }
        }

        /// <summary>
        /// Доступ к цвету шрифта
        /// </summary>
        public Brush SelectedFontBrush
        {
            get { return _selectedFontBrush; }
            set { _selectedFontBrush = value; }
        }

        /// <summary>
        /// Доступ к выбранному шрифту
        /// </summary>
        public Font SelectedFont
        {
            get { return _selectedFont; }
            set { _selectedFont = value; }
        }

        /// <summary>
        /// Доступ к выбранному формату строки
        /// </summary>
        public StringFormat SelectedStringFormat
        {
            get { return _selectedStringFormat; }
            set { _selectedStringFormat = value; }
        }
        #endregion
        #endregion

        #region Initialization

        /// <summary>
        /// Конструктор формы по умолчанию
        /// </summary>
        public TSPForm()
        {
            InitializeComponent();

            SelectedGraph = new MyGraph();

            SetDefaultPaintingProperties();

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
            SelectedPen = new Pen(Color.Black, 1);
            SelectedPen.SetLineCap(LineCap.Custom, LineCap.Custom, DashCap.Round);
        }

        private void SetHighlightPen()
        {
            HighlightPen = new Pen(Color.Yellow, 7);
            SelectedPen.SetLineCap(LineCap.Custom, LineCap.Custom, DashCap.Round);
        }

        private void SetDefaultVertexBackgroundBrush()
        {
            SelectedBackgroundBrush = new SolidBrush(Color.DarkViolet);
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

        private void InitializeCanvas()
        {
            Canvas.Image = WhitePlaneBitmap;
        }

        private List<MyGraph> InitializeGraphs(int maxVertices, int step)
        {
            List<MyGraph> graphs = new List<MyGraph>();
            int start = 5;
            for (int numberOfVertices = start; numberOfVertices <= maxVertices; numberOfVertices+=step)
            {
                graphs.Add(CreateCompleteGraph(numberOfVertices,(int)Math.Truncate((decimal)Canvas.Width), (int)Math.Truncate((decimal)Canvas.Height)));
            }

            return graphs;
        }

        private MyGraph CreateCompleteGraph(int numberOfVertices,int xBorder, int yBorder)
        {
            Random random = new Random();
            MyGraph graph = new MyGraph();

            for (int currentVertex = 0; currentVertex < numberOfVertices; currentVertex++)
            {
                PointF location = new PointF(random.Next(20,xBorder-20), random.Next(20,yBorder-20));
                graph.AddVertex(new GraphVertex(Convert.ToString(currentVertex+1),location,10));
            }

            foreach(GraphVertex vertex in graph.GraphVertices)
            {
                foreach(GraphVertex neighbour in graph.GraphVertices)
                {
                    if (vertex != neighbour)
                    {
                        float distance = GetDistance(vertex.Center,neighbour.Center);
                        graph.AddEdge(new GraphEdge(vertex,neighbour,distance,true));
                        graph.AddEdge(new GraphEdge(neighbour, vertex, distance*random.Next(0,3), true));
                    }
                }
            }

            return graph;
        }

        private float GetDistance(PointF point1, PointF point2)
        {
            return (float)Math.Sqrt(Math.Pow((point1.X - point2.X), 2) + Math.Pow((point1.Y - point2.Y), 2));
        }
        #endregion

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            SelectedGraph.DrawGraph(e.Graphics, SelectedPen, SelectedBackgroundBrush, SelectedFontBrush, SelectedFont, SelectedStringFormat);
        }

        /// <summary>
        /// Подсчёт результатов работы алгоритмов решения задачи коммивояжёра
        /// </summary>
        private void CalculateTSP()
        {
            TSPchart.Series[0].Points.Clear();
            TSPchart.Series[1].Points.Clear();
            TSPchart.Series[2].Points.Clear();
            TSPchart.Series[3].Points.Clear();
            TSPchart.Series[4].Points.Clear();

            Graphs = InitializeGraphs(MaxVerticesCount, VerticesCountStep);

            foreach (MyGraph graph in graphs)
            {
                TSPalgorithm tspAlgorithm = new TSPalgorithm(graph);

                TSPchart.Series[0].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.FullSearchTimer()));
                TSPchart.Series[1].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.NearestNeighbourTimer()));
                TSPchart.Series[2].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.ImprovedNearestNeighbourTimer()));
                TSPchart.Series[3].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.SimulatedAnnealingTimer()));
                TSPchart.Series[4].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.AntColonyAlgorithmTimer()));
                TSPchart.Series[5].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.BranchesAndBoundariesTimer()));

                FullSearchResult.Text = "" + tspAlgorithm.FullSearchResultPathLength == "0" ? "Время ожидания превышено" : Convert.ToString(tspAlgorithm.FullSearchResultPathLength);
                NearestNeighbourResult.Text = "" + tspAlgorithm.NearestNeighbourResultPathLength;
                ImprovedNearestNeighbourResult.Text = "" + tspAlgorithm.ImprovedNearestNeighbourResultPathLength;
                SimulatedAnnealingResult.Text = "" + tspAlgorithm.SimulatedAnnealingResultPathLength;
                AntColonyResult.Text = "" + tspAlgorithm.AntColonyAlgorithmResultPathLength;
                BranchesAndBoundariesResult.Text = "" + tspAlgorithm.BranchesAndBoundariesResultPathLength;

            }
            SelectedGraph = Graphs[Graphs.Count - 1];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                VerticesCountStep = int.Parse(textBoxStep.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Неверно введён шаг","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            try
            {
                MaxVerticesCount = int.Parse(textBoxMaxValue.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Неверно введено максимальное количество вершин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CalculateTSP();
            Canvas.Invalidate();

        }
    }
}
