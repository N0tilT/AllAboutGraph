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
        }

        [TestMethod]
        public void TestGetAdjacencyMatrixFromIncidenceMatrix()
        {
        }

        [TestMethod]
        public void TestGetAdjacencyListFromAdjacencyMatrix()
        {
        }

        [TestMethod]
        public void TestGetAdjacencyListFromIncidenceMatrix()
        {
        }

        [TestMethod]
        public void TestGetIncidenceMatrixFromAdjacencyMatrix()
        {
            int[,] matrix = new int[,]
            {
                {0,1,1,0,1 },
                {1,0,1,0,0 },
                {1,0,0,0,0 },
                {0,0,1,0,1 },
                {1,0,1,1,0 },
            };
            AdjacencyMatrix adjacencyMatrix= new AdjacencyMatrix(matrix);

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
            List<List<int>> list = new List<List<int>>{ 
                new List<int> { 2, 3, 5 },
                new List<int> { 1, 3 },
                new List<int> { 1 }, 
                new List<int> { 3, 5 }, 
                new List<int> { 1, 3, 4 } };
            AdjacencyList adjacencyList = new AdjacencyList(list);

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

    }
}
