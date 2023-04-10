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

namespace TSP_Research
{
    public partial class GraphVisualization : Form
    {
        private Bitmap _whitePlaneBitmap;

        private Pen _selectedPen;
        private Pen _highlightPen;
        private Brush _selectedVertexBackGroundBrush;
        private Brush _selectedFontBrush;
        private Font _selectedFont;
        private StringFormat _selectedStringFormat;

        private MyGraph _selectedGraph;

        public MyGraph SelectedGraph { get => _selectedGraph; set => _selectedGraph = value; }

        #region Painting
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
            get { return _selectedFont; }
            set { _selectedFont = value; }
        }

        public StringFormat SelectedStringFormat
        {
            get { return _selectedStringFormat; }
            set { _selectedStringFormat = value; }
        }


        #endregion

        #region Initialization
        public GraphVisualization()
        {
            InitializeComponent();

            SetDefaultPaintingProperties();

            InitializeCanvas();
        }

        public GraphVisualization(MyGraph graph) : this()
        {
            SelectedGraph = graph;
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
        #endregion
    }
}
