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
        private List<Edge> _graphEdges= new List<Edge>();
        private List<Vertex> _graphVertices= new List<Vertex>();

        private bool _isDirected = false;

        private int[,] _adjacencyMatrix;
        private List<List<int>> _adjacencyList;
        private int[,] _incidenceMatrix;
        private int[,] _degreeTable;
        #endregion

        #region Properties
        public List<Edge> GraphEdges { get { return _graphEdges; } }
        public List<Vertex> GraphVertices { get { return _graphVertices; } }
        public bool IsDirected { get { return _isDirected; } }

        public int[,] AdjacencyMatrix { get { return _adjacencyMatrix; } }
        public List<List<int>> AdjacencyList { get { return _adjacencyList; } }
        public int[,] IncidenceMatrix { get { return _incidenceMatrix; } }

        public int[,] DegreeTable { get { return GetDegreeTableFromIncidenceMatrix(IncidenceMatrix); } }

        #endregion

        #region Constructors
        public MyGraph() { }

        public MyGraph(List<Edge> graphEdges) { _graphEdges = graphEdges; }

        public MyGraph(List<Vertex> graphVertices, List<Edge> graphEdges)
        {
            _graphEdges = graphEdges;
            _graphVertices = graphVertices;

            _adjacencyMatrix = new AdjacencyMatrix(graphVertices, graphEdges).Matrix;
            _adjacencyList = new AdjacencyList(graphVertices, graphEdges).List;
            _incidenceMatrix = new IncidenceMatrix(graphVertices, graphEdges).Matrix;
        }

        public MyGraph(List<Vertex> graphVertices,List<Edge> graphEdges, bool isDirected) : this(graphVertices,graphEdges)
        {
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
                            AddEdge(new Edge(GraphVertices[currentVertex], GraphVertices[neighbour], weight, false));
                            edgeIndex++;
                        }
                        else
                        {
                            AddEdge(new Edge(GraphVertices[currentVertex], GraphVertices[neighbour], weight, true));
                            AddEdge(new Edge(GraphVertices[neighbour], GraphVertices[currentVertex], adjMatrix.Matrix[neighbour, currentVertex], true));
                            edgeIndex++;
                        }
                        LinkVertices(currentVertex, neighbour, edgeIndex);
                    }
                    else if (adjMatrix.Matrix[currentVertex, neighbour] != 0)
                    {
                        AddEdge(new Edge(GraphVertices[currentVertex], GraphVertices[neighbour], weight, true));
                        edgeIndex++;

                        LinkVertices(currentVertex, neighbour, edgeIndex);
                    }
                    else if (adjMatrix.Matrix[neighbour, currentVertex] != 0)
                    {
                        AddEdge(new Edge(GraphVertices[neighbour], GraphVertices[currentVertex], adjMatrix.Matrix[neighbour, currentVertex], true));
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
                Vertex vertex = new Vertex(vertexname);
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
                        AddEdge(new Edge(GraphVertices[currentVertex], GraphVertices[adjVertex-1], 1, true));
                        edgeIndex++;
                    }
                    else
                    {
                        AddEdge(new Edge(GraphVertices[currentVertex], GraphVertices[adjVertex - 1], 1, false));
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
                Vertex vertex = new Vertex(vertexName);
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
                                    AddEdge(new Edge(GraphVertices[firstVertex], GraphVertices[adjVertex], 1, true));
                                    LinkVertices(firstVertex, adjVertex, currentEdge);
                                    break;
                                }
                                else if (incMatrix.Matrix[currentEdge, adjVertex] == 1)
                                {
                                    AddEdge(new Edge(GraphVertices[firstVertex], GraphVertices[adjVertex], 1, false));
                                    LinkVertices(firstVertex, adjVertex, currentEdge);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    else if(incMatrix.Matrix[currentEdge, firstVertex] == 2) //loop
                    {
                        AddEdge(new Edge(GraphVertices[firstVertex], GraphVertices[firstVertex], 1, true));
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


        private int[,] GetDegreeTableFromIncidenceMatrix(int[,] incidenceMatrix)
        {
            List<int> outDegrees, inDegrees;
            CountDegrees(incidenceMatrix, out outDegrees, out inDegrees);

            int[,] degreeTable = FillDegreeTable(incidenceMatrix, outDegrees, inDegrees);

            return degreeTable;
        }

        private static void CountDegrees(int[,] incidenceMatrix, out List<int> outDegrees, out List<int> inDegrees)
        {
            outDegrees = new List<int>();
            inDegrees = new List<int>();
            for (int i = 0; i < incidenceMatrix.GetLength(1); i++)
            {
                int currentVertexInDegree = 0;
                int currentVertexOutDegree = 0;

                for (int j = 0; j < incidenceMatrix.GetLength(0); j++)
                {
                    if (incidenceMatrix[j, i] == 1)
                    {
                        currentVertexOutDegree++;
                    }
                    if (incidenceMatrix[j, i] == -1)
                    {
                        currentVertexInDegree++;
                    }
                }

                outDegrees.Add(currentVertexOutDegree);
                inDegrees.Add(currentVertexInDegree);
            }
        }

        private static int[,] FillDegreeTable(int[,] incidenceMatrix, List<int> outDegrees, List<int> inDegrees)
        {
            int[,] degreeTable = new int[incidenceMatrix.GetLength(1), 3];
            for (int i = 0; i < degreeTable.GetLength(0); i++)
            {
                degreeTable[i, 0] = outDegrees[i];
                degreeTable[i, 1] = inDegrees[i];
                degreeTable[i, 2] = outDegrees[i] + inDegrees[i];
            }

            return degreeTable;
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

        public void AddVertex(Vertex vertex)
        {
            _graphVertices.Add(vertex);
        }

        public void RemoveVertex(Vertex vertex)
        {
            _graphVertices.Remove(vertex);
        }

        public void AddEdge(Edge edge)
        {
            _graphEdges.Add(edge);
        }

        public void RemoveEdge(Edge edge)
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
            foreach (Edge edge in GraphEdges)
            {
                edge.DrawEdge(Graphics, pen);
            }

            foreach (Vertex vertex in GraphVertices)
            {
                vertex.DrawVertex(Graphics,pen,backgroundBrush,fontBrush,font,format);
            }
        }

        #endregion

        #endregion

    }
}
