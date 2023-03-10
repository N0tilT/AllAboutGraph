using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class MyGraph
    {
        #region Fields
        private List<GraphEdge> _graphEdges= new List<GraphEdge>();
        private List<GraphVertex> _graphVertices= new List<GraphVertex>();

        private bool _isDirected = false;

        private int[,] _adjacencyMatrix;
        private List<List<int>> _adjacencyList;
        #endregion

        #region Properties
        public List<GraphEdge> GraphEdges { get { return _graphEdges; } }
        public List<GraphVertex> GraphVertices { get { return _graphVertices; } }
        public bool IsDirected { get { return _isDirected; } }

        public int[,] AdjacencyMatrix { get { return _adjacencyMatrix; } }
        public List<List<int>> AdjacencyList { get { return _adjacencyList; } }

        #endregion

        public MyGraph() { }

        public MyGraph(List<GraphEdge> graphEdges) { _graphEdges = graphEdges; }

        public MyGraph(List<GraphEdge> graphEdges, List<GraphVertex> graphVertices)
        {
            _graphEdges = graphEdges;
            _graphVertices = graphVertices;
        }

        public MyGraph(List<GraphEdge> graphEdges, List<GraphVertex> graphVertices, bool isDirected) : this(graphEdges)
        {
            _graphVertices = graphVertices;
            _isDirected = isDirected;
        }

        public MyGraph(AdjacencyMatrix adjMatrix)
        {
            _adjacencyMatrix = adjMatrix.Matrix;
            CreateGraphFromAdjacencyMatrix(adjMatrix);
        }

        private void CreateGraphFromAdjacencyMatrix(AdjacencyMatrix adjMatrix)
        {
            for (int i = 0; i < adjMatrix.Rank; i++)
            {
                GraphVertices.Add(new GraphVertex(i.ToString()));
            }

            int edgeIndex = 0;
            for (int i = 0; i < adjMatrix.Rank; i++)
            {
                for (int j = 0; j < adjMatrix.Rank; j++)
                {
                    if (adjMatrix.Matrix[i, j] != 0)
                    {
                        GraphEdges.Add(new GraphEdge(GraphVertices[i], GraphVertices[j], adjMatrix.Matrix[i,j],false));
                        edgeIndex++;

                        GraphVertices[i].OutEdges.Add(GraphEdges[edgeIndex]);
                        GraphVertices[j].InEdges.Add(GraphEdges[edgeIndex]);
                    }

                }
            }
        }

        public MyGraph(AdjacencyList adjList)
        {
            _adjacencyList = adjList.List;
            CreateGraphFromAdjacencyList(adjList);
        }


        #region Methods

        private void CreateGraphFromAdjacencyList(AdjacencyList adjList)
        {
            for (int i = 0; i < adjList.CountVertices; i++)
            {
                GraphVertices.Add(new GraphVertex(i.ToString()));
            }

            int edgeIndex = 0;
            for (int i = 0; i < adjList.CountVertices; i++)
            {
                for (int j = 0; j < adjList.List[i].Count; j++)
                {
                    GraphEdges.Add(new GraphEdge(GraphVertices[i], GraphVertices[j], 1, false));
                    edgeIndex++;

                    GraphVertices[i].OutEdges.Add(GraphEdges[edgeIndex]);
                    GraphVertices[j].InEdges.Add(GraphEdges[edgeIndex]);
                }
            }
        }

        public void AddVertex(GraphVertex vertex)
        {
            _graphVertices.Add(vertex);
        }

        public void RemoveVertex(GraphVertex vertex)
        {
            _graphVertices.Remove(vertex);
        }

        public void AddEdge(GraphEdge edge)
        {
            _graphEdges.Add(edge);
        }

        public void RemoveEdge(GraphEdge edge)
        {
            _graphEdges.Remove(edge);
        }

        public void LinkVertices(int firstVertexIndex, int secondVertexIndex, int edgeIndex)
        {
            GraphVertices[firstVertexIndex].OutEdges.Add(GraphEdges[edgeIndex]);
            GraphVertices[secondVertexIndex].InEdges.Add(GraphEdges[edgeIndex]);
        }

        public void DrawGraph(Graphics Graphics,Pen pen)
        {
            foreach(GraphVertex vertex in GraphVertices)
            {
                vertex.DrawVertex(Graphics,pen);
            }
            foreach(GraphEdge edge in GraphEdges)
            {
                edge.DrawEdge(Graphics, pen);
            }
        }

        #endregion

    }
}
