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
        const int INFINITY = 1000000;
        int[,] _matrix;

        public int[,] Matrix 
        {
            get 
            { return _matrix; } 
        }

        public int Rank
        {
            get {return Matrix.Length; }
        }

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
            _matrix = new int[matrix.GetUpperBound(0), matrix.GetUpperBound(1)];
            FillMatrixCopying(matrix);
        }

        private void FillMatrixCopying(int[,] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    _matrix[i, j] = matrix[i, j];
                }
            }
        }

        public AdjacencyMatrix(List<int[]> adjList)
        {
            _matrix = new int[adjList.Count,adjList.Count];

            ResetMatrix();
            FillAdjacencyMatrixFromAdjacencyList(adjList);

        }

        private void FillAdjacencyMatrixFromAdjacencyList(List<int[]> adjList)
        {
            for (int currentVertex = 0; currentVertex < Rank; currentVertex++)
            {
                foreach (int linkedVertex in adjList[currentVertex])
                {
                    _matrix[currentVertex, linkedVertex] = 1;
                }
            }
        }

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
        public AdjacencyMatrix(List<GraphVertex> vertices)
        {
            foreach(GraphVertex vertex in vertices)
            {
                foreach(GraphEdge edge in vertex.GetAllEdges)
                {

                }
            }
        }

    }
}
