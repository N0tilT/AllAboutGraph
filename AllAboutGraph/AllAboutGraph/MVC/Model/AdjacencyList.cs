using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class AdjacencyList
    {
        #region Fields

        List<List<int>> _list;

        #endregion

        #region Properties
        public List<List<int>> List { get { return _list; } }

        public int CountVertices { get { return _list.Count; } }

        #endregion

        #region Constructors
        public AdjacencyList()
        {
            _list = new List<List<int>>();
        }

        public AdjacencyList(List<List<int>> list)
        {
            _list = list;
        }

        public AdjacencyList(AdjacencyMatrix adjacencyMatrix)
        {
            _list  = new List<List<int>>();
            ResetList();
            FromAdjacencyMatrix(adjacencyMatrix);
        }

        public AdjacencyList(IncidenceMatrix incidenceMatrix)
        {
            _list = new List<List<int>>();
            ResetList();
            FromIncidenceMatrix(incidenceMatrix);
        }


        #endregion

        #region Methods
        private void ResetList()
        {
            _list.Clear();
        }

        /// <summary>
        /// Create adjacency list from adjacency matrix
        /// </summary>
        /// <param name="matrix"></param>
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
                        _list[currentVertex].Add(neighbour+1);
                    }
                }
            }
        }

        /// <summary>
        /// Create adjacency list from incidence matrix
        /// </summary>
        /// <param name="incidenceMatrix"></param>
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
                            _list[currentVertex].Add(currentVertex+1);
                        }
                        else //found in vertex
                        {
                            _list[currentVertex].Add(inVertexIndex+1);
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

        public List<int> GetAdjacentVertices(int vertexIndex)
        {
            return _list[vertexIndex];
        }
        #endregion
    }
}
