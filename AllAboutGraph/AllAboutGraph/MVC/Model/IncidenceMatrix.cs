using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class IncidenceMatrix
    {
        #region Fields
        private int[,] _matrix;
        #endregion

        #region Properties

        public int[,] Matrix
        {
            get {return _matrix;}
            set { _matrix = value;}
        } 

        public int CountVertices
        {
            get { return _matrix.GetLength(0); }
        }

        public int CountEdges
        {
            get { return _matrix.GetLength(1); }
        }
        #endregion
        #region Constructors
        public IncidenceMatrix()
        {
            _matrix = new int[0, 0];
        }

        public IncidenceMatrix(int[,] inidenceMatrix)
        {
            _matrix = inidenceMatrix;
        }

        public IncidenceMatrix(AdjacencyMatrix adjMatrix)
        {
            _matrix = FromAdjacencyMatrix(adjMatrix);
        }

        public IncidenceMatrix(AdjacencyList adjList)
        {
            _matrix = FromAdjacencyList(adjList);
        }
        #endregion
        #region Methods

        private int[,] FromAdjacencyMatrix(AdjacencyMatrix adjMatrix)
        {
            int numOfVertices = adjMatrix.Matrix.GetLength(0);
            int numOfEdges = 0;

            for (int i = 0; i < numOfVertices; i++)
            {
                for (int j = 0; j < numOfVertices; j++)
                {
                    if (i < j)
                    {
                        if (adjMatrix.Matrix[i, j] > 0)
                        {
                            numOfEdges++;
                        }
                        if (adjMatrix.Matrix[j, i] > 0 && adjMatrix.Matrix[j, i] != adjMatrix.Matrix[i, j])
                        {
                            numOfEdges++;
                        }
                    }
                }
            }

            int[,] incedenceMatrix = new int[numOfVertices, numOfEdges];
            for (int i = 0; i < numOfVertices; i++)
            {
                for (int j = 0; j < numOfVertices; j++)
                {
                    if (i < j)
                    {
                        int edgeNumber = adjMatrix.Matrix[i, j];
                        int altEdgeNumber = adjMatrix.Matrix[j, i];

                        if (edgeNumber > 0 && edgeNumber != altEdgeNumber)
                        {
                            incedenceMatrix[i, edgeNumber - 1] = -1;
                            incedenceMatrix[j, edgeNumber - 1] = 1;
                        }

                        if (altEdgeNumber > 0 && edgeNumber != altEdgeNumber)
                        {
                            incedenceMatrix[i, edgeNumber - 1] = 1;
                            incedenceMatrix[j, edgeNumber - 1] = -1;
                        }

                        if (altEdgeNumber > 0 && edgeNumber == altEdgeNumber)
                        {
                            incedenceMatrix[i, edgeNumber - 1] = 1;
                            incedenceMatrix[j, edgeNumber - 1] = 1;
                        }

                    }
                }
            }

            return incedenceMatrix;
        }

        private int[,] FromAdjacencyList(AdjacencyList adjList)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
