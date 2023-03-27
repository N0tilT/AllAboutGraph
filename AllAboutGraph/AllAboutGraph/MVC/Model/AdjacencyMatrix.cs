using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class AdjacencyMatrix
    {
        #region Constants

        public const int INFINITY = 1000000;

        #endregion

        #region Fields

        int[,] _matrix;

        #endregion

        #region Properties
        public int[,] Matrix 
        {
            get 
            { return _matrix; } 
        }

        public int Rank
        {
            get {return Matrix.GetLength(0); }
        }

        #endregion

        #region Constructors
        public AdjacencyMatrix() 
        {
            _matrix = new int[0, 0]; 
        }

        public AdjacencyMatrix(int rows, int columns) 
        {
            _matrix = new int[rows, columns];
            ResetMatrix();
        }

        public AdjacencyMatrix(int[,] matrix)
        {
            _matrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
            FillMatrixCopying(matrix);
        }

        public AdjacencyMatrix(List<GraphVertex> graphVertices, List<GraphEdge> graphEdges)
        {
            _matrix = new int[graphVertices.Count, graphVertices.Count];

            ResetMatrix();

            for (int i = 0; i < graphVertices.Count; i++)
            {
                for (int j = 0; j < graphEdges.Count; j++)
                {
                    if (graphEdges[j].VertexOut == graphVertices[i])
                    {
                        _matrix[i, int.Parse(graphEdges[j].VertexIn.Name) - 1]++;
                    }
                }
            }
        }

        public AdjacencyMatrix(AdjacencyList adjList)
        {
            _matrix = new int[adjList.CountVertices, adjList.CountVertices];

            ResetMatrix();
            FromAdjacencyList(adjList.List);

        }

        public AdjacencyMatrix(IncidenceMatrix incidenceMatrix) 
        {
            _matrix = new int[incidenceMatrix.CountVertices, incidenceMatrix.CountVertices];

            ResetMatrix();
            FromIncidenceMatrix(incidenceMatrix);
        }
        #endregion

        #region Methods
        

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
        /// Create adjacency matrix from incidence matrix
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
        /// find vertex, for which current edge wull be ingoing
        /// </summary>
        /// <param name="incidenceMatrix"></param>
        /// <param name="currentEdge"></param>
        /// <returns>found vertex index in incidence matrix</returns>
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
        /// create adjacency matrix from adjacency list
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
