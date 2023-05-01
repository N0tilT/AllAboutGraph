using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    /// <summary>
    /// Класс матрицы инцидентности
    /// </summary>
    public class IncidenceMatrix
    {
        #region Fields
        private int[,] _matrix;
        #endregion

        #region Properties

        /// <summary>
        /// Доступ к матрице
        /// </summary>
        public int[,] Matrix
        {
            get {return _matrix;}
            set { _matrix = value;}
        } 
        
        /// <summary>
        /// Количество вершин
        /// </summary>
        public int CountVertices
        {
            get { return _matrix.GetLength(1); }
        }

        /// <summary>
        /// Количество рёбер
        /// </summary>
        public int CountEdges
        {
            get { return _matrix.GetLength(0); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public IncidenceMatrix()
        {
            _matrix = new int[0, 0];
        }

        /// <summary>
        /// Конструктор по матрице
        /// </summary>
        /// <param name="inidenceMatrix"></param>
        public IncidenceMatrix(int[,] inidenceMatrix)
        {
            _matrix = inidenceMatrix;
        }

        /// <summary>
        /// Конструктор по матрице смежности
        /// </summary>
        /// <param name="adjMatrix">матрица смежности</param>
        public IncidenceMatrix(AdjacencyMatrix adjMatrix)
        {
            _matrix = FromAdjacencyMatrix(adjMatrix);
        }

        /// <summary>
        /// Конструктор по списку смежности
        /// </summary>
        /// <param name="adjList"></param>
        public IncidenceMatrix(AdjacencyList adjList)
        {
            _matrix = FromAdjacencyList(adjList);
        }

        /// <summary>
        /// Конструктор по списку вершин и рёбер графа
        /// </summary>
        /// <param name="graphVertices"></param>
        /// <param name="graphEdges"></param>
        public IncidenceMatrix(List<GraphVertex> graphVertices, List<GraphEdge> graphEdges)
        {
            _matrix = new int[graphEdges.Count, graphVertices.Count];

            for (int i = 0; i < graphEdges.Count; i++)
            {
                int curVertexIndex = int.Parse(graphEdges[i].VertexOut.Name) - 1;
                int adjVertexINdex = int.Parse(graphEdges[i].VertexIn.Name) - 1;

                _matrix[i,curVertexIndex] = 1;
                
                _matrix[i, adjVertexINdex] = -1;
            }


        }
        #endregion

        #region Methods

        /// <summary>
        /// Получить матрицу инцидентности из матрицы смежности
        /// </summary>
        /// <param name="adjMatrix"></param>
        /// <returns></returns>
        private int[,] FromAdjacencyMatrix(AdjacencyMatrix adjMatrix)
        {
            int numOfVertices = adjMatrix.Matrix.GetLength(0);
            int numOfEdges = 0;
            numOfEdges = CountVerticesFromAdjacentMatrix(adjMatrix, numOfVertices, numOfEdges);

            int[,] incedenceMatrix = new int[numOfEdges,numOfVertices];
            int edgeIndex = 0;
            for (int i = 0; i < numOfVertices-1; i++)
            {
                for (int j = i+1; j < numOfVertices; j++)
                {
                    //not-oriented edge
                    if(adjMatrix.Matrix[i,j] != 0 && adjMatrix.Matrix[j, i] != 0)
                    {
                        incedenceMatrix[edgeIndex, i]++;
                        incedenceMatrix[edgeIndex, j]++;
                        edgeIndex++;
                    }
                    //start - i, end - j
                    else if(adjMatrix.Matrix[i, j] != 0)
                    {
                        incedenceMatrix[edgeIndex, i]++;
                        incedenceMatrix[edgeIndex, j]--; 
                        edgeIndex++;
                    }
                    //start - j, end - i
                    else if(adjMatrix.Matrix[j,i] != 0)
                    {
                        incedenceMatrix[edgeIndex, i]--;
                        incedenceMatrix[edgeIndex, j]++; 
                        edgeIndex++;
                    }
                }
            }

            return incedenceMatrix;
        }

        /// <summary>
        /// Получить количество вершин из матрицы смежности
        /// </summary>
        /// <param name="adjMatrix">матрица смежности</param>
        /// <param name="numOfVertices">количество вершин</param>
        /// <param name="numOfEdges">количество рёбер</param>
        /// <returns></returns>
        private static int CountVerticesFromAdjacentMatrix(AdjacencyMatrix adjMatrix, int numOfVertices, int numOfEdges)
        {
            for (int i = 0; i < numOfVertices; i++)
            {
                for (int j = i; j < numOfVertices; j++)
                {
                    if (adjMatrix.Matrix[i, j] != 0)
                    {
                        numOfEdges++;
                    }
                    else if (adjMatrix.Matrix[j, i] != 0 && adjMatrix.Matrix[j, i] != adjMatrix.Matrix[i, j])
                    {
                        numOfEdges++;
                    }
                }
            }

            return numOfEdges;
        }

        /// <summary>
        /// получить матрицу инцидентности из списка смежности
        /// </summary>
        /// <param name="adjList">список смежности</param>
        /// <returns></returns>
        private int[,] FromAdjacencyList(AdjacencyList adjList)
        {
            int numOfVertices = adjList.CountVertices;
            int numOfEdges = 0;
            numOfEdges = CountEdgesFromAdjacencyList(CopyList(adjList));

            int[,] incidenceMatrix = new int[numOfEdges,numOfVertices];


            AdjacencyList copyAdjList = CopyList(adjList);

            int edgeIndex = 0;

            for (int i = 0; i < copyAdjList.List.Count; i++)
            {
                for (int j = 0; j < copyAdjList.List[i].Count; j++)
                {
                    incidenceMatrix[edgeIndex, i]++;
                    int neighbourIndex = copyAdjList.List[i][j] - 1;
                    if (copyAdjList.List[neighbourIndex].Contains(i+1))
                    {
                        incidenceMatrix[edgeIndex, neighbourIndex]++;
                        copyAdjList.List[neighbourIndex].Remove(i+1);
                        edgeIndex++;
                    }
                    else
                    {
                        incidenceMatrix[edgeIndex, neighbourIndex]--;
                        edgeIndex++;
                    }
                }
            }


            return incidenceMatrix;
        }

        /// <summary>
        /// Копировать список смежности
        /// </summary>
        /// <param name="adjList">список смежности</param>
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
        /// Получить количество рёбер из матрицы смежности
        /// </summary>
        /// <param name="adjList">матрица смежности</param>
        /// <returns></returns>
        private int CountEdgesFromAdjacencyList(AdjacencyList adjList)
        {
            List<int> counted = new List<int>();
            int count = 0;
            for (int i = 0; i < adjList.List.Count; i++)
            {
                foreach (int neighbour in adjList.List[i])
                {
                    count++;
                    adjList.List[neighbour-1].Remove(i+1);
                }
            }
            return count;
        }

        #endregion
    }
}
