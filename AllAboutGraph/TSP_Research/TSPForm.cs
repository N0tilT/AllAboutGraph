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
    public partial class TSPForm : Form
    {
        #region Constants
        const int verticesCountStep = 5;
        const int maxVerticesCount = 100;
        #endregion
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
        #endregion
        #region Properties
        public List<MyGraph> Graphs { get => graphs; set => graphs = value; }

        public MyGraph SelectedGraph { get => _selectedGraph; set => _selectedGraph = value; }

        #region Painting
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
            get { return _selectedFont; }
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
        public TSPForm()
        {
            InitializeComponent();

            Graphs = InitializeGraphs(maxVerticesCount, verticesCountStep);

            foreach (MyGraph graph in graphs)
            {
                TSPalgorithm tspAlgorithm = new TSPalgorithm(graph);

                TSPchart.Series[0].Points.Add(new DataPoint(graph.GraphVertices.Count,tspAlgorithm.FullSearchTimer()));
                TSPchart.Series[1].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.NearestNeighbourTimer()));
                TSPchart.Series[2].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.ImprovedNearestNeighbourTimer()));
                TSPchart.Series[3].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.SimulatedAnnealingTimer()));
                TSPchart.Series[4].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.BranchesAndBoundariesTimer()));
                TSPchart.Series[5].Points.Add(new DataPoint(graph.GraphVertices.Count, tspAlgorithm.AntColonyAlgorithmTimer()));

                //MessageBox.Show(PrintPath(tspAlgorithm.FullSearchResultPath) + " " + tspAlgorithm.FullSearchResultPathLength);
                //MessageBox.Show(PrintPath(tspAlgorithm.NearestNeighbourResultPath) + " " + tspAlgorithm.NearestNeighbourResultPathLength);
                //MessageBox.Show(PrintPath(tspAlgorithm.ImprovedNearestNeighbourResultPath) + " " + tspAlgorithm.ImprovedNearestNeighbourResultPathLength);
                //MessageBox.Show(PrintPath(tspAlgorithm.SimulatedAnnealingResultPath) + " " + tspAlgorithm.SimulatedAnnealingResultPathLength);
                //MessageBox.Show(PrintPath(tspAlgorithm.BranchesAndBoundariesResultPath) + " " + tspAlgorithm.BranchesAndBoundariesResultPathLength);
                //MessageBox.Show(PrintPath(tspAlgorithm.AntColonyAlgorithmResultPath) + " " + tspAlgorithm.AntColonyAlgorithmResultPathLength);
            }
            SelectedGraph = Graphs[0];
            SetDefaultPaintingProperties();

            InitializeCanvas();
        }

        private string PrintPath(List<int> path)
        {
            string result = "";
            foreach(int item in path)
            {
                result += Convert.ToString(item) + " ";
            }
            return result;
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
            int start = step;
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
                        graph.AddEdge(new GraphEdge(vertex,neighbour,distance,false));
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
    }
}
