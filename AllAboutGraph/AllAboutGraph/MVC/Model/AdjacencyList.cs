using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Model
{
    public class AdjacencyList
    {
        List<List<int>> _list;
        public List<List<int>> List { get { return _list; } }

        public int CountVertices { get { return _list.Count; } }

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
            ResetList();
            FillAdjacencyListFromAdjacencyMatrix(adjacencyMatrix);
        }

        private void FillAdjacencyListFromAdjacencyMatrix(AdjacencyMatrix matrix)
        {
            for (int i = 0; i < matrix.Rank; i++)
            {
                _list.Add(new List<int>());
                for (int j = 0; j < matrix.Rank; j++)
                {
                    if (matrix.Matrix[i,j] != 0)    
                        _list[i].Add(j);
                }
            }
        }

        private void ResetList()
        {
            _list.Clear();
        }

        /// <summary>
        /// Получить вершины, смежные указанной
        /// </summary>
        /// <param name="vertexIndex">индекс выбранной вершины</param>
        /// <returns>индексы вершин, смежных с выбранной</returns>
        public List<int> GetAdjacentVertices(int vertexIndex)
        {
            return _list[vertexIndex];
        }
    }
}
