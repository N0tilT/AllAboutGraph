using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    /// <summary>
    /// Класс списка смежности
    /// </summary>
    public class AdjacencyList
    {
        #region Fields

        List<List<int>> _list;

        #endregion

        #region Properties
        /// <summary>
        /// Доступ к списку
        /// </summary>
        public List<List<int>> List { get { return _list; } }

        /// <summary>
        /// Получить количество вершин
        /// </summary>
        public int CountVertices { get { return _list.Count; } }

        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public AdjacencyList()
        {
            _list = new List<List<int>>();
        }

        /// <summary>
        /// Конструктор из списка списков
        /// </summary>
        /// <param name="list">список списков</param>
        public AdjacencyList(List<List<int>> list)
        {
            _list = list;
        }

        /// <summary>
        /// Конструктор по списку врешин и рёбер графа
        /// </summary>
        /// <param name="graphVertices">список вершин графа</param>
        /// <param name="graphEdges">список рёбер графа</param>
        public AdjacencyList(List<GraphVertex> graphVertices, List<GraphEdge> graphEdges)
        {
            AdjacencyMatrix adjMatrix = new AdjacencyMatrix(graphVertices,graphEdges);
            _list = new AdjacencyList(adjMatrix).List;
        }

        /// <summary>
        /// Конструктор по матрице смежности
        /// </summary>
        /// <param name="adjacencyMatrix">матрица смежности</param>
        public AdjacencyList(AdjacencyMatrix adjacencyMatrix)
        {
            _list  = new List<List<int>>();
            ResetList();
            FromAdjacencyMatrix(adjacencyMatrix);
        }

        /// <summary>
        /// Конструктор по матрице инцидентности
        /// </summary>
        /// <param name="incidenceMatrix">матрица инцидентности</param>
        public AdjacencyList(IncidenceMatrix incidenceMatrix)
        {
            _list = new List<List<int>>();
            ResetList();
            FromIncidenceMatrix(incidenceMatrix);
        }



        #endregion

        #region Methods
        /// <summary>
        /// Очистить список смежности
        /// </summary>
        private void ResetList()
        {
            _list.Clear();
        }

        /// <summary>
        /// Получить список смежности из матрицы смежности
        /// </summary>
        /// <param name="matrix">матрица смежности</param>
        private void FromAdjacencyMatrix(AdjacencyMatrix matrix)
        {
            for (int currentVertex = 0; currentVertex < matrix.Rank; currentVertex++)
            {
                //new row in adjacency list
                _list.Add(new List<int>());

                //fill row
                for (int neighbour = 0; neighbour < matrix.Rank; neighbour++)
                {
                    if (matrix.Matrix[currentVertex, neighbour] != 0)
                    {
                        _list[currentVertex].Add(neighbour);
                    }
                }
            }
        }

        /// <summary>
        /// Получить список смежности из матрицы инцидентности
        /// </summary>
        /// <param name="incidenceMatrix">матрица инцидентности</param>
        private void FromIncidenceMatrix(IncidenceMatrix incidenceMatrix)
        {
            for (int currentVertex = 0; currentVertex < incidenceMatrix.CountVertices; currentVertex++)
            {
                //new row in adjacency list
                _list.Add(new List<int>()); 

                //fill row
                for (int currentEdge = 0; currentEdge < incidenceMatrix.CountEdges; currentEdge++)
                {
                    //find edge, for which the current vertex will be outgoing
                    if (incidenceMatrix.Matrix[currentEdge,currentVertex] > 0)
                    {
                        int inVertexIndex = FindInVertex(incidenceMatrix, currentEdge, currentVertex);

                        //loop
                        if(inVertexIndex == -1)
                        {
                            _list[currentVertex].Add(currentVertex);
                        }
                        else //found in vertex
                        {
                            _list[currentVertex].Add(inVertexIndex);
                        }
                        
                    }
                }
            }
        }

        /// <summary>
        /// Поиск исходящей вершины
        /// </summary>
        /// <param name="incidenceMatrix"></param>
        /// <param name="currentEdge"></param>
        /// <returns>индекс найденной вершины</returns>
        private int FindInVertex(IncidenceMatrix incidenceMatrix, int currentEdge, int currentVertex)
        {
            int inVertexIndex = -1;

            for (int subVertex = 0; subVertex < incidenceMatrix.CountVertices; subVertex++)
            {
                if (subVertex != currentVertex)
                {
                    //it doesn`t matter if edge is oriented or not
                    if (incidenceMatrix.Matrix[currentEdge, subVertex] == -1 || incidenceMatrix.Matrix[currentEdge, subVertex] == 1)
                    {
                        inVertexIndex = subVertex;
                        break;
                    }
                }
            }

            return inVertexIndex;
        }
        #endregion
    }
}
