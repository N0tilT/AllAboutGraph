using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AllAboutGraph;
using AllAboutGraph.MVC.Model;
using System.Collections.Generic;

namespace GraphUnitTest
{
    [TestClass]
    public class GraphUnitTest
    {
        [TestMethod]
        public void TestCreateGraphWithAdjacencyMatrix()
        {
            int[,] adjMatrix = new int[,]
            {
                {0,1,1,0,1 },
                {1,0,1,0,0 },
                {1,0,0,0,0 },
                {0,0,1,0,1 },
                {1,0,1,1,0 },
            };
            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(adjMatrix);

            MyGraph graph = new MyGraph(adjacencyMatrix);

            List<GraphVertex> expectedVertices = new List<GraphVertex>() {
                new GraphVertex("A"),
                new GraphVertex("B"),
                new GraphVertex("C"),
                new GraphVertex("D"),
                new GraphVertex("E"),
            };
            List<GraphEdge> expectedEdges = new List<GraphEdge>() {
                new GraphEdge(expectedVertices[0],expectedVertices[1],1,false), //1-2
                new GraphEdge(expectedVertices[0],expectedVertices[2],1,false), //1-3
                new GraphEdge(expectedVertices[0],expectedVertices[4],1,false), //1-5
                new GraphEdge(expectedVertices[1],expectedVertices[2],1,true),  //2->3
                new GraphEdge(expectedVertices[3], expectedVertices[2], 1, true), //4->3
                new GraphEdge(expectedVertices[4], expectedVertices[2], 1, true), //5->3
                new GraphEdge(expectedVertices[3], expectedVertices[4], 1, false), //4-5
            };

            Assert.AreEqual(GetStringEdgeList(expectedEdges), GetStringEdgeList(graph.GraphEdges));

        }

        [TestMethod]
        public void TestCreateGraphWithAdjacencyList()
        {
            List<List<int>> adjList = new List<List<int>>{
                new List<int> { 2, 3, 5 },
                new List<int> { 1, 3 },
                new List<int> { 1 },
                new List<int> { 3, 5 },
                new List<int> { 1, 3, 4 } };
            AdjacencyList adjacencyList = new AdjacencyList(adjList);

            MyGraph graph = new MyGraph(adjacencyList);
            
            List<GraphVertex> expectedVertices = new List<GraphVertex>() {
                new GraphVertex("A"),
                new GraphVertex("B"),
                new GraphVertex("C"),
                new GraphVertex("D"),
                new GraphVertex("E"),
            };
            List<GraphEdge> expectedEdges = new List<GraphEdge>() { 
                new GraphEdge(expectedVertices[0],expectedVertices[1],1,false), //1-2
                new GraphEdge(expectedVertices[0],expectedVertices[2],1,false), //1-3
                new GraphEdge(expectedVertices[0],expectedVertices[4],1,false), //1-5
                new GraphEdge(expectedVertices[1],expectedVertices[2],1,true),  //2->3
                new GraphEdge(expectedVertices[3], expectedVertices[2], 1, true), //4->3
                new GraphEdge(expectedVertices[3], expectedVertices[4], 1, false), //4-5
                new GraphEdge(expectedVertices[4], expectedVertices[2], 1, true), //5->3
            };

            Assert.AreEqual(GetStringEdgeList(expectedEdges), GetStringEdgeList(graph.GraphEdges));
        }


        [TestMethod]
        public void TestCreateGraphWithIncidenceMatrix()
        {
            int[,] incMatrix = new int[,] {
                    {1, 1, 0, 0, 0 },
                    {1, 0, 1, 0 , 0 },
                    {1, 0, 0, 0, 1 },
                    {0, 1, -1, 0, 0 },
                    {0, 0, -1, 1, 0 },
                    {0, 0, -1, 0, 1 },
                    {0, 0, 0, 1, 1 }};
            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(incMatrix);

            MyGraph graph = new MyGraph(incidenceMatrix);

            List<GraphVertex> expectedVertices = new List<GraphVertex>() {
                new GraphVertex("A"),
                new GraphVertex("B"),
                new GraphVertex("C"),
                new GraphVertex("D"),
                new GraphVertex("E"),
            };
            List<GraphEdge> expectedEdges = new List<GraphEdge>() {
                new GraphEdge(expectedVertices[0],expectedVertices[1],1,false), //1-2
                new GraphEdge(expectedVertices[0],expectedVertices[2],1,false), //1-3
                new GraphEdge(expectedVertices[0],expectedVertices[4],1,false), //1-5
                new GraphEdge(expectedVertices[1],expectedVertices[2],1,true),  //2->3
                new GraphEdge(expectedVertices[3], expectedVertices[2], 1, true), //4->3
                new GraphEdge(expectedVertices[4], expectedVertices[2], 1, true), //5->3
                new GraphEdge(expectedVertices[3], expectedVertices[4], 1, false), //4-5
            };

            Assert.AreEqual(GetStringEdgeList(expectedEdges), GetStringEdgeList(graph.GraphEdges));
        }

        [TestMethod]
        public void TestGetAdjacencyMatrixFromAdjacencyList()
        {
            List<List<int>> adjList = new List<List<int>>{
                new List<int> { 2, 3, 5 },
                new List<int> { 1, 3 },
                new List<int> { 1 },
                new List<int> { 3, 5 },
                new List<int> { 1, 3, 4 } };
            AdjacencyList adjacencyList = new AdjacencyList(adjList);

            float[,] expectedMatrix = new float[,] {
                {0,1,1,0,1 },
                {1,0,1,0,0 },
                {1,0,0,0,0 },
                {0,0,1,0,1 },
                {1,0,1,1,0 } };

            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(adjacencyList);
            PrintMatrix(adjacencyMatrix.Matrix);
            Assert.AreEqual(GetStringMatrix(expectedMatrix), GetStringMatrix(adjacencyMatrix.Matrix));
        }

        [TestMethod]
        public void TestGetAdjacencyMatrixFromIncidenceMatrix()
        {
            int[,] incMatrix = new int[,] {
                    {1, 1, 0, 0, 0 },
                    {1, 0, 1, 0 , 0 },
                    {1, 0, 0, 0, 1 },
                    {0, 1, -1, 0, 0 },
                    {0, 0, -1, 1, 0 },
                    {0, 0, -1, 0, 1 },
                    {0, 0, 0, 1, 1 }};
            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(incMatrix);

            float[,] expectedMatrix = new float[,] {
                {0,1,1,0,1 },
                {1,0,1,0,0 },
                {1,0,0,0,0 },
                {0,0,1,0,1 },
                {1,0,1,1,0 } };

            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(incidenceMatrix);
            PrintMatrix(adjacencyMatrix.Matrix);
            Assert.AreEqual(GetStringMatrix(expectedMatrix), GetStringMatrix(adjacencyMatrix.Matrix));
        }

        [TestMethod]
        public void TestGetAdjacencyListFromAdjacencyMatrix()
        {
            int[,] adjMatrix = new int[,]
            {
                {0,1,1,0,1 },
                {1,0,1,0,0 },
                {1,0,0,0,0 },
                {0,0,1,0,1 },
                {1,0,1,1,0 },
            };
            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(adjMatrix);

            List<List<int>> expectedList = new List<List<int>>()
            {
                new List<int>(){1,2,4 },
                new List<int>(){0,2 },
                new List<int>(){0 },
                new List<int>(){2,4 },
                new List<int>(){0,2,3 },
            };

            AdjacencyList adjacencyList = new AdjacencyList(adjacencyMatrix);

            PrintList(adjacencyList.List);
            Assert.AreEqual(GetStringAdjList(expectedList), GetStringAdjList(adjacencyList.List));
        }

        [TestMethod]
        public void TestGetAdjacencyListFromIncidenceMatrix()
        {
            int[,] incMatrix = new int[,] {
                    {1, 1, 0, 0, 0 },
                    {1, 0, 1, 0 , 0 },
                    {1, 0, 0, 0, 1 },
                    {0, 1, -1, 0, 0 },
                    {0, 0, -1, 1, 0 },
                    {0, 0, -1, 0, 1 },
                    {0, 0, 0, 1, 1 }};
            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(incMatrix);

            List<List<int>> expectedList = new List<List<int>>()
            {
                new List<int>(){1,2,4 },
                new List<int>(){0,2 },
                new List<int>(){0 },
                new List<int>(){2,4 },
                new List<int>(){0,2,3 },
            };

            AdjacencyList adjacencyList = new AdjacencyList(incidenceMatrix);

            PrintList(adjacencyList.List);
            Assert.AreEqual(GetStringAdjList(expectedList), GetStringAdjList(adjacencyList.List));

        }


        [TestMethod]
        public void TestGetIncidenceMatrixFromAdjacencyMatrix()
        {
            int[,] adjMatrix = new int[,]
            {
                {0,1,1,0,1 },
                {1,0,1,0,0 },
                {1,0,0,0,0 },
                {0,0,1,0,1 },
                {1,0,1,1,0 },
            };
            AdjacencyMatrix adjacencyMatrix= new AdjacencyMatrix(adjMatrix);

            float[,] expectedMatrix = new float[,] {
                    {1, 1, 0, 0, 0 },
                    {1, 0, 1, 0 , 0 },
                    {1, 0, 0, 0, 1 },
                    {0, 1, -1, 0, 0 },
                    {0, 0, -1, 1, 0 },
                    {0, 0, -1, 0, 1 },
                    {0, 0, 0, 1, 1 } };

            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjacencyMatrix);
            PrintIntMatrix(incidenceMatrix.Matrix);
            Assert.AreEqual(GetStringMatrix(expectedMatrix), GetStringIntMatrix(incidenceMatrix.Matrix));

        }

        [TestMethod]
        public void TestGetIncidenceMatrixFromAdjacencyList()
        {
            List<List<int>> adjList = new List<List<int>>{ 
                new List<int> { 2, 3, 5 },
                new List<int> { 1, 3 },
                new List<int> { 1 }, 
                new List<int> { 3, 5 }, 
                new List<int> { 1, 3, 4 } };
            AdjacencyList adjacencyList = new AdjacencyList(adjList);

            float[,] expectedMatrix = new float[,] {
                    {1, 1, 0, 0, 0 },
                    {1, 0, 1, 0 , 0 },
                    {1, 0, 0, 0, 1 },
                    {0, 1, -1, 0, 0 },
                    {0, 0, -1, 1, 0 },
                    {0, 0, 0, 1, 1 },
                    {0, 0, -1, 0, 1 } };

            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjacencyList);
            PrintIntMatrix(incidenceMatrix.Matrix);
            Assert.AreEqual(GetStringMatrix(expectedMatrix), GetStringIntMatrix(incidenceMatrix.Matrix));
        }
        private object GetStringMatrix(float[,] matrix)
        {
            string result = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result += matrix[i, j];
                }
            }
            return result;
        }
        private object GetStringIntMatrix(int[,] matrix)
        {
            string result = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result += matrix[i, j];
                }
            }
            return result;
        }

        private object GetStringAdjList(List<List<int>> list)
        {
            string result = "";

            foreach(List<int> row in list)
            {
                foreach (int item in row)
                {
                    result += item;
                }
            }

            return result;
        }

        private string GetStringEdgeList(List<GraphEdge> expectedEdges)
        {
            string result = "";
            foreach (GraphEdge edge in expectedEdges)
            {
                result += edge.VertexOut.Name + " " + edge.VertexIn.Name + " " + edge.Weight + " " + edge.Directed.ToString() + " ";
            }
            return result;
        }

        private string GetStringVertexList(List<GraphVertex> expectedVertices)
        {
            string result = "";
            foreach (GraphVertex vertex in expectedVertices)
            {
                result += vertex.Name + " ";
            }
            return result;
        }
        private void PrintMatrix(float[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
            }
        }

        private void PrintIntMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
            }
        }

        private void PrintList(List<List<int>> list)
        {
            foreach (List<int> row in list)
            {
                Console.WriteLine();
                foreach (int item in row)
                {
                    Console.Write(item + " ");
                }
            }
        }
    }
}
