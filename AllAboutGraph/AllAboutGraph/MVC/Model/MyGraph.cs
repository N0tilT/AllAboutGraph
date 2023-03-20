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
            AdjacencyList list = new AdjacencyList(adjMatrix);
            _adjacencyList = list.List;
        }


        public MyGraph(AdjacencyList adjList)
        {
            _adjacencyList = adjList.List;
            CreateGraphFromAdjacencyList(adjList);
        }


        #region Methods

        
        #region GraphCreation
        private void CreateGraphFromAdjacencyMatrix(AdjacencyMatrix adjMatrix)
        {
            PointF[] verticesPoints = GetGraphVerticesPoints(adjMatrix.Rank, 200, new PointF(250, 250));

            for (int i = 0; i < adjMatrix.Rank; i++)
            {
                string vertexname = GetVertexName(i);
                GraphVertex vertex = new GraphVertex(vertexname);
                vertex.Size = new SizeF(50, 50);
                vertex.Location = verticesPoints[i];
                AddVertex(vertex);
            }

            int edgeIndex = -1;
            for (int i = 0; i < adjMatrix.Rank; i++)
            {
                for (int j = 0; j < adjMatrix.Rank; j++)
                {
                    if (i < j)
                    {
                        if (adjMatrix.Matrix[i, j] != 0 && adjMatrix.Matrix[j, i] != 0)
                        {
                            AddEdge(new GraphEdge(GraphVertices[i], GraphVertices[j], adjMatrix.Matrix[i, j], false));
                            edgeIndex++;

                            LinkVertices(i, j, edgeIndex);
                        }
                        else if (adjMatrix.Matrix[i, j] != 0)
                        {
                            AddEdge(new GraphEdge(GraphVertices[i], GraphVertices[j], adjMatrix.Matrix[i, j], true));
                            edgeIndex++;

                            LinkVertices(i, j, edgeIndex);
                        }
                        else if(adjMatrix.Matrix[j, i] != 0)
                        {
                            AddEdge(new GraphEdge(GraphVertices[i], GraphVertices[j], adjMatrix.Matrix[j, i], true));
                            edgeIndex++;

                            LinkVertices(i, j, edgeIndex);
                        }
                    }
                }
            }
        }
        private void CreateGraphFromAdjacencyList(AdjacencyList adjList)
        {
            PointF[] verticesPoints = GetGraphVerticesPoints(adjList.CountVertices, 200, new PointF(250, 250));

            for (int i = 0; i < adjList.CountVertices; i++)
            {
                string vertexName = GetVertexName(i); 
                GraphVertex vertex = new GraphVertex(vertexName);
                vertex.Size = new SizeF(50, 50);
                vertex.Location = verticesPoints[i];
                AddVertex(vertex);
            }

            int edgeIndex = -1;
            for (int i = 0; i < adjList.CountVertices; i++)
            {
                for (int j = 0; j < adjList.List[i].Count; j++)
                {
                    bool directed = adjList.List[j].Contains(adjList.List[i][j]);


                    AddEdge(new GraphEdge(GraphVertices[i], GraphVertices[j], 1, !directed));
                    edgeIndex++;

                    LinkVertices(i, j, edgeIndex);
                }
            }
        }

        private void CreateGraphFromIncidenceMatrix(IncidenceMatrix incMatrix)
        {
            return;
        }
        private PointF[] GetGraphVerticesPoints(int numberOfVertices, float radius, PointF center)
        {
            PointF[] points = new PointF[numberOfVertices];

            for (int i = 0; i < numberOfVertices; i++)
            {
                float x = center.X + radius * (float)Math.Cos(2 * Math.PI * i / numberOfVertices);
                float y = center.Y + radius * (float)Math.Sin(2 * Math.PI * i / numberOfVertices);

                points[i] = new PointF(x, y);
            }

            return points;
        }


        #endregion

        private string GetVertexName(int i)
        {
            char index = Convert.ToChar(i+64);
            StringBuilder name = new StringBuilder();

            name.Append(index);
            if (name[name.Length-1] == 'Z')
            {
                name[name.Length - 1] = 'A';
                name.Append('A');
            }
            else
            {
                name[name.Length - 1]++;
            }

            return name.ToString();
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

        public void DrawGraph(Graphics Graphics,Pen pen,Brush backgroundBrush,Brush fontBrush, Font font, StringFormat format)
        {
            foreach (GraphEdge edge in GraphEdges)
            {
                edge.DrawEdge(Graphics, pen);
            }

            foreach (GraphVertex vertex in GraphVertices)
            {
                vertex.DrawVertex(Graphics,pen,backgroundBrush,fontBrush,font,format);
            }
        }

        #endregion

    }
}
