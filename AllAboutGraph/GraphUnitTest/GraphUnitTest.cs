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
        public void TestCreateWithAdjacencyMatrix()
        {
        }

        [TestMethod]
        public void TestCreateWithAdjacencyList()
        {
        }

        [TestMethod]
        public void TestCreateWithIncidenceMatrix()
        {
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

            int[,] expectedMatrix = new int[,] {
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

            int[,] expectedMatrix = new int[,] {
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
                new List<int>(){2,3,5 },
                new List<int>(){1,3 },
                new List<int>(){1 },
                new List<int>(){3,5 },
                new List<int>(){1,3,4 },
            };

            AdjacencyList adjacencyList = new AdjacencyList(adjacencyMatrix);

            PrintList(adjacencyList.List);
            Assert.AreEqual(GetStringList(expectedList), GetStringList(adjacencyList.List));
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
                new List<int>(){2,3,5 },
                new List<int>(){1,3 },
                new List<int>(){1 },
                new List<int>(){3,5 },
                new List<int>(){1,3,4 },
            };

            AdjacencyList adjacencyList = new AdjacencyList(incidenceMatrix);

            PrintList(adjacencyList.List);
            Assert.AreEqual(GetStringList(expectedList), GetStringList(adjacencyList.List));

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

            int[,] expectedMatrix = new int[,] {
                    {1, 1, 0, 0, 0 },
                    {1, 0, 1, 0 , 0 },
                    {1, 0, 0, 0, 1 },
                    {0, 1, -1, 0, 0 },
                    {0, 0, -1, 1, 0 },
                    {0, 0, -1, 0, 1 },
                    {0, 0, 0, 1, 1 } };

            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjacencyMatrix);
            PrintMatrix(incidenceMatrix.Matrix);
            Assert.AreEqual(GetStringMatrix(expectedMatrix), GetStringMatrix(incidenceMatrix.Matrix));

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

            int[,] expectedMatrix = new int[,] {
                    {1, 1, 0, 0, 0 },
                    {1, 0, 1, 0 , 0 },
                    {1, 0, 0, 0, 1 },
                    {0, 1, -1, 0, 0 },
                    {0, 0, -1, 1, 0 },
                    {0, 0, 0, 1, 1 },
                    {0, 0, -1, 0, 1 } };

            IncidenceMatrix incidenceMatrix = new IncidenceMatrix(adjacencyList);
            PrintMatrix(incidenceMatrix.Matrix);
            Assert.AreEqual(GetStringMatrix(expectedMatrix), GetStringMatrix(incidenceMatrix.Matrix));
        }

        private object GetStringMatrix(int[,] matrix)
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

        private object GetStringList(List<List<int>> list)
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

        private void PrintMatrix(int[,] matrix)
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
