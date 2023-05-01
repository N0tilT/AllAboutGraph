using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    /// <summary>
    /// Класс матрицы смежности
    /// </summary>
    public class AdjacencyMatrix
    {
        #region Constants

        public const int INFINITY = 1000000;

        #endregion

        #region Fields

        float[,] _matrix;

        #endregion

        #region Properties
        /// <summary>
        /// Доступ к матрице
        /// </summary>
        public float[,] Matrix 
        {
            get 
            { return _matrix; } 
        }
        
        /// <summary>
        /// Ранк матрицы
        /// </summary>
        public int Rank
        {
            get {return Matrix.GetLength(0); }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public AdjacencyMatrix() 
        {
            _matrix = new float[0, 0]; 
        }

        /// <summary>
        /// Конструктор по числу строк и столбцов
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public AdjacencyMatrix(int rows, int columns) 
        {
            _matrix = new float[rows, columns];
            ResetMatrix();
        }

        /// <summary>
        /// Коснтруктор по матрице смежности
        /// </summary>
        /// <param name="matrix"></param>
        public AdjacencyMatrix(int[,] matrix)
        {
            _matrix = new float[matrix.GetLength(0), matrix.GetLength(1)];
            FillMatrixCopying(matrix);
        }

        /// <summary>
        /// Конструктор по списку вершин и рёбер графа
        /// </summary>
        /// <param name="graphVertices">список вершин рёбер</param>
        /// <param name="graphEdges">список рёбер графа</param>
        public AdjacencyMatrix(List<GraphVertex> graphVertices, List<GraphEdge> graphEdges)
        {
            _matrix = new float[graphVertices.Count, graphVertices.Count];

            ResetMatrix();

            for (int i = 0; i < graphVertices.Count; i++)
            {
                for (int j = 0; j < graphEdges.Count; j++)
                {
                    if (graphEdges[j].VertexOut == graphVertices[i])
                    {
                        _matrix[i, int.Parse(graphEdges[j].VertexIn.Name) - 1] = graphEdges[j].Weight;
                    }
                }
            }
        }

        /// <summary>
        /// Конструктор по списку смежности
        /// </summary>
        /// <param name="adjList">список смежности</param>
        public AdjacencyMatrix(AdjacencyList adjList)
        {
            _matrix = new float[adjList.CountVertices, adjList.CountVertices];

            ResetMatrix();
            FromAdjacencyList(adjList.List);

        }

        /// <summary>
        /// Коснтруктор по матрице инцидентности
        /// </summary>
        /// <param name="incidenceMatrix">матрица иницидентности</param>
        public AdjacencyMatrix(IncidenceMatrix incidenceMatrix) 
        {
            _matrix = new float[incidenceMatrix.CountVertices, incidenceMatrix.CountVertices];

            ResetMatrix();
            FromIncidenceMatrix(incidenceMatrix);
        }
        #endregion

        #region Methods
        
        /// <summary>
        /// Очистить матрицу
        /// </summary>
        private void ResetMatrix()
        {
            for (int i = 0; i < Rank; i++)
            {
                for (int j = 0; j < Rank; j++)
                {
                    _matrix[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Заполнить матрицу копированием
        /// </summary>
        /// <param name="matrix">копируемая матрица</param>
        private void FillMatrixCopying(int[,] matrix)
        {
            for (int i = 0; i < this.Rank; i++)
            {
                for (int j = 0; j < this.Rank; j++)
                {
                    _matrix[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Получить матрицу смежности из матрицы инцидентности
        /// </summary>
        /// <param name="incidenceMatrix"></param>
        private void FromIncidenceMatrix(IncidenceMatrix incidenceMatrix)
        {
            for (int currentVertex = 0; currentVertex < incidenceMatrix.CountVertices; currentVertex++)
            {
                for (int currentEdge = 0; currentEdge < incidenceMatrix.CountEdges; currentEdge++)
                {
                    if (incidenceMatrix.Matrix[currentEdge,currentVertex] > 0)
                    {
                        int inVertexIndex = FindInVertex(incidenceMatrix, currentVertex, currentEdge);

                        //loop
                        if (inVertexIndex == -1)
                        {
                            _matrix[currentVertex, currentVertex]++;
                        }
                        else
                        {
                            _matrix[currentVertex, inVertexIndex]++;
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
        /// <returns> индекс найденной вершины</returns>
        private static int FindInVertex(IncidenceMatrix incidenceMatrix, int currentVertex, int currentEdge)
        {
            int inVertexIndex = -1;
            for (int subVertex = 0; subVertex < incidenceMatrix.CountVertices; subVertex++)
            {
                if (subVertex != currentVertex)
                {
                    if (incidenceMatrix.Matrix[currentEdge, subVertex] == -1 || incidenceMatrix.Matrix[currentEdge, subVertex] == 1)
                    {
                        inVertexIndex = subVertex;
                        break;
                    }
                }
            }

            return inVertexIndex;
        }

        /// <summary>
        /// Получить матрицу смежности по списку смежности
        /// </summary>
        /// <param name="adjList"></param>
        private void FromAdjacencyList(List<List<int>> adjList)
        {
            for (int currentVertex = 0; currentVertex < Rank; currentVertex++)
            {
                foreach (int neighbour in adjList[currentVertex])
                {
                    _matrix[currentVertex, neighbour-1] = 1;
                }
            }
        }


        public List<int> GetAdjacentVertices(int vertexIndex)
        {
            List<int> adjacnetRow = new List<int>();
            for (int i = 0; i < Rank; i++)
            {
                if (_matrix[vertexIndex,i] != 0)
                    adjacnetRow.Add(i);
            }
            return adjacnetRow;
        }
        #endregion
    }
}
