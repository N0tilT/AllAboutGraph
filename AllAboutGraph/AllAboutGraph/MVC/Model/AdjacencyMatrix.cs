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

        const int INFINITY = 1000000;

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
                    _matrix[i, j] = INFINITY;
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
        private void FromIncidenceMatrix(IncidenceMatrix incidenceMatrix)
        {
            throw new NotImplementedException();
        }

        private void FromAdjacencyList(List<List<int>> adjList)
        {
            for (int currentVertex = 0; currentVertex < Rank; currentVertex++)
            {
                foreach (int linkedVertex in adjList[currentVertex])
                {
                    _matrix[currentVertex, linkedVertex] = 1;
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
