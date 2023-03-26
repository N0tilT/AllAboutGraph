using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
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
            numOfEdges = CountVerticesFromAdjacentMatrix(adjMatrix, numOfVertices, numOfEdges);

            int[,] incedenceMatrix = new int[numOfVertices, numOfEdges];
            int edgeIndex = 0;
            for (int i = 0; i < numOfVertices; i++)
            {
                for (int j = i; j < numOfVertices; j++)
                {
                    //not-oriented edge
                    if(adjMatrix.Matrix[i,j] != AdjacencyMatrix.INFINITY && adjMatrix.Matrix[j, i] != AdjacencyMatrix.INFINITY)
                    {
                        incedenceMatrix[edgeIndex, i]++;
                        incedenceMatrix[edgeIndex, j]++;
                    }
                    //start - i, end - j
                    else if(adjMatrix.Matrix[i, j] != AdjacencyMatrix.INFINITY)
                    {
                        incedenceMatrix[edgeIndex, i]++;
                        incedenceMatrix[edgeIndex, j]--;
                    }
                    //start - j, end - i
                    else
                    {
                        incedenceMatrix[edgeIndex, i]--;
                        incedenceMatrix[edgeIndex, j]++;
                    }
                }
            }

            return incedenceMatrix;
        }

        private static int CountVerticesFromAdjacentMatrix(AdjacencyMatrix adjMatrix, int numOfVertices, int numOfEdges)
        {
            for (int i = 0; i < numOfVertices; i++)
            {
                for (int j = i; j < numOfVertices; j++)
                {
                    if (adjMatrix.Matrix[i, j] != AdjacencyMatrix.INFINITY)
                    {
                        numOfEdges++;
                    }
                    else if (adjMatrix.Matrix[j, i] != AdjacencyMatrix.INFINITY && adjMatrix.Matrix[j, i] != adjMatrix.Matrix[i, j])
                    {
                        numOfEdges++;
                    }
                }
            }

            return numOfEdges;
        }

        private int[,] FromAdjacencyList(AdjacencyList adjList)
        {
            int numOfVertices = adjList.CountVertices;
            int numOfEdges = 0;
            numOfEdges = CountEdgesFromAdjacencyList(CopyList(adjList));

            int[,] incidenceMatrix = new int[numOfVertices,numOfEdges];


            AdjacencyList copyAdjList = CopyList(adjList);

            int edgeIndex = 0;

            for (int i = 0; i < copyAdjList.List.Count; i++)
            {
                for (int j = 0; j < copyAdjList.List[i].Count; j++)
                {
                    incidenceMatrix[edgeIndex, i]++;
                    if (copyAdjList.List[j].Contains(i))
                    {
                        incidenceMatrix[edgeIndex, j]++;
                        copyAdjList.List[j].Remove(i);
                    }
                    else
                    {
                        incidenceMatrix[edgeIndex, j]--;
                    }
                }
            }


            return incidenceMatrix;
        }

        private AdjacencyList CopyList(AdjacencyList adjList)
        {
            List<List<int>> copyList = new List<List<int>>();

            for (int i = 0; i < adjList.List.Count; i++)
            {
                copyList.Add(adjList.List[i]);
                for (int j = 0; j < adjList.List[i].Count; j++)
                {
                    copyList[i].Add(adjList.List[i][j]);
                }
            }
            
            return new AdjacencyList(copyList);
            

        }

        private int CountEdgesFromAdjacencyList(AdjacencyList adjList)
        {
            int count = 0;
            for (int i = 0; i < adjList.List.Count; i++)
            {
                foreach(int neighbour in adjList.List[i])
                {
                    count++;
                    adjList.List[neighbour].Remove(i);
                }
            }
            return count;
        }

        #endregion
    }
}
