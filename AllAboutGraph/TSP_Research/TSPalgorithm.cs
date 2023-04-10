using AllAboutGraph.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace TSP_Research
{
    internal class TSPalgorithm
    {
        #region Fields
        MyGraph _graph;
        #endregion

        #region Properties
        public MyGraph Graph { get => _graph; set => _graph = value; }
        #endregion

        #region Constructors
        public TSPalgorithm(MyGraph graph)
        {
            Graph = graph;
        }

        internal float FullSearch()
        {
            return Graph.GraphEdges.Count * 10;
        }

        internal float RandomFullSearch()
        {
            return Graph.GraphEdges.Count * 5;
        }

        internal float NearestNeighbour()
        {
            return Graph.GraphEdges.Count;
        }

        internal float ImprovedNearestNeighbour()
        {
            return Graph.GraphVertices.Count * 100;
        }

        internal float SimulatedAnnealing()
        {
            return Graph.GraphVertices.Count * 10;
        }

        internal float BranchesAndBoundaries()
        {
            return Graph.GraphVertices.Count * 5;
        }

        internal float AntColonyAlgorithm()
        {
            return Graph.GraphVertices.Count;
        }
        #endregion

        #region Methods

        #endregion

    }
}
