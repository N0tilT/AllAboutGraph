using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    /// <summary>
    /// Класс графа
    /// </summary>
    public class MyGraph
    {
        #region Fields
        private List<GraphEdge> _graphEdges= new List<GraphEdge>();
        private List<GraphVertex> _graphVertices= new List<GraphVertex>();

        private bool _isDirected = false;

        private float[,] _adjacencyMatrix;
        private List<List<int>> _adjacencyList;
        private int[,] _incidenceMatrix;
        private int[,] _degreeTable;
        #endregion

        #region Properties
        /// <summary>
        /// Список рёбер графа
        /// </summary>
        public List<GraphEdge> GraphEdges { get { return _graphEdges; } }
        /// <summary>
        /// Список вершин графа
        /// </summary>
        public List<GraphVertex> GraphVertices { get { return _graphVertices; } }
        /// <summary>
        /// Ориентирован ли граф
        /// </summary>
        public bool IsDirected { get { return _isDirected; } }

        /// <summary>
        /// Доступ к матрице смежности
        /// </summary>
        public float[,] AdjacencyMatrix 
        { 
            get 
            {
                if(_adjacencyMatrix == null)
                {
                    _adjacencyMatrix = new AdjacencyMatrix(GraphVertices, GraphEdges).Matrix;
                }
                if (_adjacencyMatrix.GetLength(0) < GraphVertices.Count)
                {
                    _adjacencyMatrix = new AdjacencyMatrix(GraphVertices, GraphEdges).Matrix;
                }

                return _adjacencyMatrix;
            }
        }
        /// <summary>
        /// Доступ к списку смежности
        /// </summary>
        public List<List<int>> AdjacencyList
        { 
            get 
            { 
                if(_adjacencyList == null)
                {
                    _adjacencyList = new AdjacencyList(GraphVertices, GraphEdges).List;
                }

                if(_adjacencyList.Count < GraphVertices.Count)
                {
                    _adjacencyList = new AdjacencyList(GraphVertices, GraphEdges).List;
                }

                return _adjacencyList; 
            }
        }
        /// <summary>
        /// Доступ к матрице инцидентности
        /// </summary>
        public int[,] IncidenceMatrix 
        { 
            get 
            { 
                if(_incidenceMatrix == null)
                {
                    _incidenceMatrix = new IncidenceMatrix(GraphVertices, GraphEdges).Matrix;
                }

                if(_incidenceMatrix.GetLength(1) < GraphVertices.Count)
                {
                    _incidenceMatrix = new IncidenceMatrix(GraphVertices, GraphEdges).Matrix;
                }

                return _incidenceMatrix; 
            } 
        }

        /// <summary>
        /// Получить матрицу степеней вершин
        /// </summary>
        public int[,] DegreeTable { get { return GetDegreeTableFromIncidenceMatrix(); } }

        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MyGraph() { }

        /// <summary>
        /// Конструктор по списку рёбер
        /// </summary>
        /// <param name="graphEdges"></param>
        public MyGraph(List<GraphEdge> graphEdges) { _graphEdges = graphEdges; }

        /// <summary>
        /// Конструктор по списку вершин и рёбер графа
        /// </summary>
        /// <param name="graphVertices">список вершин</param>
        /// <param name="graphEdges">список рёбер</param>
        public MyGraph(List<GraphVertex> graphVertices, List<GraphEdge> graphEdges)
        {
            _graphEdges = graphEdges;
            _graphVertices = graphVertices;

            _adjacencyMatrix = new AdjacencyMatrix(graphVertices, graphEdges).Matrix;
            _adjacencyList = new AdjacencyList(graphVertices, graphEdges).List;
            _incidenceMatrix = new IncidenceMatrix(graphVertices, graphEdges).Matrix;
        }

        /// <summary>
        /// Конструктор по списку вершин, рёбер и ориентированности
        /// </summary>
        /// <param name="graphVertices">список вершин</param>
        /// <param name="graphEdges">список рёбер</param>
        /// <param name="isDirected">ориентирован ли граф</param>
        public MyGraph(List<GraphVertex> graphVertices,List<GraphEdge> graphEdges, bool isDirected) : this(graphVertices,graphEdges)
        {
            _isDirected = isDirected;
        }

        /// <summary>
        /// Конструктор по матрице смежности
        /// </summary>
        /// <param name="adjMatrix"></param>
        public MyGraph(AdjacencyMatrix adjMatrix)
        {
            _adjacencyMatrix = adjMatrix.Matrix;
            CreateGraphFromAdjacencyMatrix(adjMatrix);

            AdjacencyList list = new AdjacencyList(adjMatrix);
            _adjacencyList = list.List;

            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjMatrix);
            _incidenceMatrix = incidenceMatrix.Matrix;
        }

        /// <summary>
        /// Конструктор по списку смежности
        /// </summary>
        /// <param name="adjList"></param>
        public MyGraph(AdjacencyList adjList)
        {
            _adjacencyList = adjList.List;
            CreateGraphFromAdjacencyList(adjList);

            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(adjList);
            _adjacencyMatrix = adjacencyMatrix.Matrix;

            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjList);
            _incidenceMatrix = incidenceMatrix.Matrix;
        }

        /// <summary>
        /// Конструктор по матрице инцидентности
        /// </summary>
        /// <param name="incidenceMatrix"></param>
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
        /// <summary>
        /// Создание графа по матрице смежности
        /// </summary>
        /// <param name="adjMatrix"></param>
        private void CreateGraphFromAdjacencyMatrix(AdjacencyMatrix adjMatrix)
        {
            PointF[] verticesPoints = GetGraphVerticesPoints(adjMatrix.Rank, 200, new PointF(250, 250));

            CreateVerticesFromAdjacencyMatrix(adjMatrix.Matrix, verticesPoints);

            int edgeIndex = -1;
            for (int currentVertex = 0; currentVertex < adjMatrix.Rank; currentVertex++)
            {
                for (int neighbour = currentVertex+1; neighbour < adjMatrix.Rank; neighbour++)
                {
                    float weight = adjMatrix.Matrix[currentVertex, neighbour];

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

        /// <summary>
        /// Создать вершины из матрицы смежности
        /// </summary>
        /// <param name="adjMatrix">матрица смежности</param>
        /// <param name="verticesPoints">координаты вершин</param>
        private void CreateVerticesFromAdjacencyMatrix(float[,] adjMatrix, PointF[] verticesPoints)
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
        /// <summary>
        /// Создать вершины из матрицы инцидентности
        /// </summary>
        /// <param name="incMatrix">матрица инцидентности</param>
        /// <param name="verticesPoints">координаты вершин</param>
        private void CreateVerticesFromIncidenceMatrix(int[,] incMatrix, PointF[] verticesPoints)
        {
            for (int i = 0; i < incMatrix.GetLength(1); i++)
            {
                string vertexname = GetVertexName(i);
                GraphVertex vertex = new GraphVertex(vertexname);
                vertex.Size = new SizeF(50, 50);
                vertex.Location = verticesPoints[i];
                AddVertex(vertex);
            }
        }
        /// <summary>
        /// Создать граф по списку смежности
        /// </summary>
        /// <param name="adjList">список смежности</param>
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
        /// <summary>
        /// Копировать список
        /// </summary>
        /// <param name="adjList"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Создать вершины из списка смежности
        /// </summary>
        /// <param name="adjList">список смежности</param>
        /// <param name="verticesPoints">координаты вершин</param>
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

        /// <summary>
        /// Создать граф из матрицы инцидентности
        /// </summary>
        /// <param name="incMatrix">матрица инцидентности</param>
        private void CreateGraphFromIncidenceMatrix(IncidenceMatrix incMatrix)
        {
            PointF[] verticesPoints = GetGraphVerticesPoints(incMatrix.CountVertices, 200, new PointF(250, 250));
            CreateVerticesFromIncidenceMatrix(incMatrix.Matrix,verticesPoints);

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
        /// <summary>
        /// Получить координаты вершин графа
        /// </summary>
        /// <param name="numberOfVertices">количество вершин</param>
        /// <param name="radius">радиус круга, по которому расположены вершины</param>
        /// <param name="center">центр круга</param>
        /// <returns></returns>
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

        #region SubTables
        /// <summary>
        /// Получить матрицу вершин из матрицы инцидентности
        /// </summary>
        /// <returns></returns>
        public int[,] GetDegreeTableFromIncidenceMatrix()
        {
            List<int> outDegrees, inDegrees;
            CountDegrees(IncidenceMatrix, out outDegrees, out inDegrees);

            int[,] degreeTable = FillDegreeTable(IncidenceMatrix, outDegrees, inDegrees);

            return degreeTable;
        }

        /// <summary>
        /// Подсчитать степени вершин
        /// </summary>
        /// <param name="incidenceMatrix">матрица иницдентности</param>
        /// <param name="outDegrees">исходящие степени</param>
        /// <param name="inDegrees">входящие степени</param>
        private void CountDegrees(int[,] incidenceMatrix, out List<int> outDegrees, out List<int> inDegrees)
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
        /// <summary>
        /// Заполнить таблицу степеней вершин
        /// </summary>
        /// <param name="incidenceMatrix">матрица инцидентности</param>
        /// <param name="outDegrees">исходящие вершины</param>
        /// <param name="inDegrees">входящие вершины</param>
        /// <returns></returns>
        private int[,] FillDegreeTable(int[,] incidenceMatrix, List<int> outDegrees, List<int> inDegrees)
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

        /// <summary>
        /// Получить таблицу расстояний с помощью алгоритма Дейкстры
        /// </summary>
        /// <returns></returns>
        public float[,] GetDistanceTable()
        {
            float[,] distanceTable = new float[GraphVertices.Count, GraphVertices.Count];
            for (int startVertex = 0; startVertex < GraphVertices.Count; startVertex++)
            {
                float[] distances = DijkstraAlgorithm(AdjacencyMatrix, startVertex);
                for (int j = 0; j < GraphVertices.Count; j++)
                {
                    distanceTable[startVertex, j] = distances[j];
                }
            }
            return distanceTable;
        }

        /// <summary>
        /// Получить расстояния до вершин от указанной с помощью алгоритма Дейкстры
        /// </summary>
        /// <param name="adjacencyMatrix">матрица смежности</param>
        /// <param name="startVertex">начальная вершина</param>
        /// <returns></returns>
        private static float[] DijkstraAlgorithm(float[,] adjacencyMatrix, int startVertex)
        {
            int numberOfVertices = adjacencyMatrix.GetLength(0);
            float[] distances = new float[numberOfVertices];
            bool[] inShortestPath = new bool[numberOfVertices];

            const int INFINITY = int.MaxValue;

            for (int i = 0; i < numberOfVertices; i++)
            {
                distances[i] = INFINITY;
                inShortestPath[i] = false;
            }

            distances[startVertex] = 0;

            for (int i = 0; i < numberOfVertices - 1; i++)
            {
                int minVertex = MinDistance(distances, inShortestPath);

                inShortestPath[minVertex] = true;

                for (int adjacentVertex = 0; adjacentVertex < numberOfVertices; adjacentVertex++)
                {
                    if (!inShortestPath[adjacentVertex]
                        && adjacencyMatrix[minVertex, adjacentVertex] != 0
                        && distances[minVertex] != INFINITY
                        && distances[minVertex] + adjacencyMatrix[minVertex, adjacentVertex] < distances[adjacentVertex])
                    {
                        distances[adjacentVertex] = distances[minVertex] + adjacencyMatrix[minVertex, adjacentVertex];
                    }
                }

            }

            return distances;
        }

        /// <summary>
        /// Найти вершину с минимальным путём до неё, которой ещё нет в кратчайшем пути
        /// </summary>
        /// <param name="distances"></param>
        /// <param name="inShortestPath"></param>
        /// <returns></returns>
        private static int MinDistance(float[] distances, bool[] inShortestPath)
        {
            float min = int.MaxValue;
            int minVertexIndex = -1;

            for (int v = 0; v < distances.Length; v++)
                if (inShortestPath[v] == false && distances[v] <= min)
                {
                    min = distances[v];
                    minVertexIndex = v;
                }

            return minVertexIndex;
        }

        #endregion

        #region VerticesName

        /// <summary>
        /// Получить имя очередной вершины
        /// </summary>
        /// <param name="i">номер вершины</param>
        /// <returns></returns>
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

        /// <summary>
        /// Добавить вершину в граф
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(GraphVertex vertex)
        {
            _graphVertices.Add(vertex);
        }

        /// <summary>
        /// Удалить вершину графа
        /// </summary>
        /// <param name="vertex"></param>
        public void RemoveVertex(GraphVertex vertex)
        {
            _graphVertices.Remove(vertex);
        }

        /// <summary>
        /// Добавить ребро в граф
        /// </summary>
        /// <param name="edge"></param>
        public void AddEdge(GraphEdge edge)
        {
            _graphEdges.Add(edge);
        }

        /// <summary>
        /// Удалить ребро из графа
        /// </summary>
        /// <param name="edge"></param>
        public void RemoveEdge(GraphEdge edge)
        {
            _graphEdges.Remove(edge);
        }

        /// <summary>
        /// Связать вершины ребром
        /// </summary>
        /// <param name="firstVertexIndex">индекс исходящей вершины</param>
        /// <param name="secondVertexIndex">индекс входящей вершины</param>
        /// <param name="edgeIndex">индекс ребра, связывающего вершины</param>
        public void LinkVertices(int firstVertexIndex, int secondVertexIndex, int edgeIndex)
        {
            GraphVertices[firstVertexIndex].OutEdges.Add(GraphEdges[edgeIndex]);
            GraphVertices[secondVertexIndex].InEdges.Add(GraphEdges[edgeIndex]);
        }

        #endregion

        #region Draw

        /// <summary>
        /// Изобразить граф на экране
        /// </summary>
        /// <param name="Graphics">объект графики</param>
        /// <param name="pen">цвет обводки графа</param>
        /// <param name="backgroundBrush">цвет заливки вершин графа</param>
        /// <param name="fontBrush">цвет шрифта</param>
        /// <param name="font">шрифт</param>
        /// <param name="format">формат текста</param>
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
