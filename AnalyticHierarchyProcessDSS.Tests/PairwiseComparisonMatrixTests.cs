using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Precise;
using AnalyticHierarchyProcessDSS.WolframEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnalyticHierarchyProcessDSS.Tests
{
    [TestClass]
    public class PairwiseComparisonMatrixTests
    {
        [TestMethod]
        public void ToStringReturnsCorrectResult()
        {
            //// arrange
            //double[,] array = { { 1, 4 }, { 0.25, 1 } };
            //var matrix = new PairwiseComparisonMatrix(array, new MainEigenvectorResolutionStrategy());

            //// act
            //string expected = "{{1, 4},{0.25, 1}}";
            //string actual = matrix.ToString();

            //// assert
            //Assert.AreEqual(actual, expected);

        }

        [TestMethod]
        public void ToConstructorWithJaggedInputCorrectResult()
        {
            // arrange
            double[][] array = new double[][]{ new double[]{ 2, 4, 8}, new[]{ 0.25, 2 }, new double[]{8} };
            var matrix = new PairwiseComparisonMatrix(array);
            // act

            var expected = new PairwiseComparisonMatrix(new double[,] { { 1, 2, 4, 8 }, { 0.5, 1, 0.25, 2 }, { 0.25,4, 1, 8 }, { 0.125, 0.5, 0.125, 1 } });
            var actual = matrix;

            // assert
            Assert.AreEqual(expected, actual);

        }
    }
}
