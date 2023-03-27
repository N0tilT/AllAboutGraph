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
        private int[,] _incidenceMatrix;
        #endregion

        #region Properties
        public List<GraphEdge> GraphEdges { get { return _graphEdges; } }
        public List<GraphVertex> GraphVertices { get { return _graphVertices; } }
        public bool IsDirected { get { return _isDirected; } }

        public int[,] AdjacencyMatrix { get { return _adjacencyMatrix; } }
        public List<List<int>> AdjacencyList { get { return _adjacencyList; } }
        public int[,] IncidenceMatrix { get { return _incidenceMatrix; } }

        #endregion

        #region Constructors
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

            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjMatrix);
            _incidenceMatrix = incidenceMatrix.Matrix;
        }


        public MyGraph(AdjacencyList adjList)
        {
            _adjacencyList = adjList.List;
            CreateGraphFromAdjacencyList(adjList);

            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(adjList);
            _adjacencyMatrix = adjacencyMatrix.Matrix;

            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjList);
            _incidenceMatrix = incidenceMatrix.Matrix;
        }

        public MyGraph(IncidenceMatrix incidenceMatrix)
        {
            _incidenceMatrix = incidenceMatrix.Matrix;
            CreateGraphFromIncidenceMatrix(incidenceMatrix);

            AdjacencyMatrix adjMatrix = new AdjacencyMatrix(incidenceMatrix);
            _adjacencyMatrix = adjMatrix.Matrix;

            AdjacencyList adjList = new AdjacencyList(incidenceMatrix);
            _adjacencyList = adjList.List;
        }

        #endregion

        #region Methods


        #region GraphCreation
        private void CreateGraphFromAdjacencyMatrix(AdjacencyMatrix adjMatrix)
        {
            PointF[] verticesPoints = GetGraphVerticesPoints(adjMatrix.Rank, 200, new PointF(250, 250));

            CreateVerticesFromMatrix(adjMatrix.Matrix, verticesPoints);

            int edgeIndex = -1;
            for (int currentVertex = 0; currentVertex < adjMatrix.Rank; currentVertex++)
            {
                for (int neighbour = currentVertex+1; neighbour < adjMatrix.Rank; neighbour++)
                {
                    int weight = adjMatrix.Matrix[currentVertex, neighbour];

                    if (adjMatrix.Matrix[currentVertex, neighbour] != 0 && adjMatrix.Matrix[neighbour, currentVertex] != 0)
                    {
                        if(adjMatrix.Matrix[currentVertex, neighbour] == adjMatrix.Matrix[neighbour, currentVertex])
                        {
                            AddEdge(new GraphEdge(GraphVertices[currentVertex], GraphVertices[neighbour], weight, false));
                            edgeIndex++;
                        }
                        else
                        {
                            AddEdge(new GraphEdge(GraphVertices[currentVertex], GraphVertices[neighbour], weight, true));
                            AddEdge(new GraphEdge(GraphVertices[neighbour], GraphVertices[currentVertex], adjMatrix.Matrix[neighbour, currentVertex], true));
                            edgeIndex++;
                        }
                        LinkVertices(currentVertex, neighbour, edgeIndex);
                    }
                    else if (adjMatrix.Matrix[currentVertex, neighbour] != 0)
                    {
                        AddEdge(new GraphEdge(GraphVertices[currentVertex], GraphVertices[neighbour], weight, true));
                        edgeIndex++;

                        LinkVertices(currentVertex, neighbour, edgeIndex);
                    }
                    else if (adjMatrix.Matrix[neighbour, currentVertex] != 0)
                    {
                        AddEdge(new GraphEdge(GraphVertices[neighbour], GraphVertices[currentVertex], adjMatrix.Matrix[neighbour, currentVertex], true));
                        edgeIndex++;

                        LinkVertices(currentVertex, neighbour, edgeIndex);
                    }
                }
            }
        }

        private void CreateVerticesFromMatrix(int[,] adjMatrix, PointF[] verticesPoints)
        {
            for (int i = 0; i < adjMatrix.GetLength(1); i++)
            {
                string vertexname = GetVertexName(i);
                GraphVertex vertex = new GraphVertex(vertexname);
                vertex.Size = new SizeF(50, 50);
                vertex.Location = verticesPoints[i];
                AddVertex(vertex);
            }
        }

        private void CreateGraphFromAdjacencyList(AdjacencyList adjList)
        {
            PointF[] verticesPoints = GetGraphVerticesPoints(adjList.CountVertices, 200, new PointF(250, 250));
            CreateVerticesFromAdjacencyList(adjList, verticesPoints);

            AdjacencyList copy = CopyList(adjList);

            int edgeIndex = -1;
            for (int currentVertex = 0; currentVertex < copy.List.Count; currentVertex++)
            {
                for (int neighbourIndex = 0; neighbourIndex < copy.List[currentVertex].Count; neighbourIndex++)
                {
                    int adjVertex = copy.List[currentVertex][neighbourIndex];
                    bool oriented = !copy.List[adjVertex-1].Contains(currentVertex+1);

                    if (oriented)
                    {
                        AddEdge(new GraphEdge(GraphVertices[currentVertex], GraphVertices[adjVertex-1], 1, true));
                        edgeIndex++;
                    }
                    else
                    {
                        AddEdge(new GraphEdge(GraphVertices[currentVertex], GraphVertices[adjVertex - 1], 1, false));
                        copy.List[adjVertex - 1].Remove(currentVertex+1);
                        edgeIndex++;
                    }


                    LinkVertices(currentVertex, neighbourIndex, edgeIndex);
                }
            }
        }
        private AdjacencyList CopyList(AdjacencyList adjList)
        {
            List<List<int>> copyList = new List<List<int>>();

            for (int i = 0; i < adjList.List.Count; i++)
            {
                copyList.Add(new List<int>());
                for (int j = 0; j < adjList.List[i].Count; j++)
                {
                    copyList[i].Add(adjList.List[i][j]);
                }
            }
            return new AdjacencyList(copyList);


        }

        private void CreateVerticesFromAdjacencyList(AdjacencyList adjList, PointF[] verticesPoints)
        {
            for (int i = 0; i < adjList.CountVertices; i++)
            {
                string vertexName = GetVertexName(i);
                GraphVertex vertex = new GraphVertex(vertexName);
                vertex.Size = new SizeF(50, 50);
                vertex.Location = verticesPoints[i];
                AddVertex(vertex);
            }
        }

        private void CreateGraphFromIncidenceMatrix(IncidenceMatrix incMatrix)
        {
            PointF[] verticesPoints = GetGraphVerticesPoints(incMatrix.CountVertices, 200, new PointF(250, 250));
            CreateVerticesFromMatrix(incMatrix.Matrix,verticesPoints);

            for (int currentEdge = 0; currentEdge < incMatrix.CountEdges; currentEdge++)
            {
                for (int firstVertex = 0; firstVertex < incMatrix.CountVertices; firstVertex++)
                {
                    if(incMatrix.Matrix[currentEdge,firstVertex] == 1)
                    {
                        for (int adjVertex = 0; adjVertex < incMatrix.CountVertices; adjVertex++)
                        {
                            if (firstVertex != adjVertex)
                            {
                                if (incMatrix.Matrix[currentEdge, adjVertex] == -1)
                                {
                                    AddEdge(new GraphEdge(GraphVertices[firstVertex], GraphVertices[adjVertex], 1, true));
                                    LinkVertices(firstVertex, adjVertex, currentEdge);
                                    break;
                                }
                                else if (incMatrix.Matrix[currentEdge, adjVertex] == 1)
                                {
                                    AddEdge(new GraphEdge(GraphVertices[firstVertex], GraphVertices[adjVertex], 1, false));
                                    LinkVertices(firstVertex, adjVertex, currentEdge);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    else if(incMatrix.Matrix[currentEdge, firstVertex] == 2) //loop
                    {
                        AddEdge(new GraphEdge(GraphVertices[firstVertex], GraphVertices[firstVertex], 1, true));
                        LinkVertices(firstVertex, firstVertex, currentEdge);
                        break;
                    }
                }
            }

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

        #region VerticesName

        private string GetVertexName(int i)
        {
            char index = Convert.ToChar(i + 64);
            StringBuilder name = new StringBuilder();

            name.Append(index);
            if (name[name.Length - 1] == 'Z')
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

        #endregion

        #region VerticesAndEdges

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

        #endregion

        #region Draw

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

        #endregion

    }
}
