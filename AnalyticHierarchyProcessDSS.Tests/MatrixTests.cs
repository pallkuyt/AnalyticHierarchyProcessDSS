using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.Entities.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnalyticHierarchyProcessDSS.Tests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void EqualsSuccessOnSameMatrix()
        {
            // arrange
            var matrix = new Matrix<double>(new[,] {{1, 4}, {0.25, 1}});

            var matrixToCompare = new Matrix<double>(new[,] { { 1, 4 }, { 0.25, 1 } });

            // assert
            Assert.IsTrue(matrix.Equals(matrixToCompare));
        }

        [TestMethod]
        public void EqualsFailureOnOtherMatrix()
        {
            // arrange
            var matrix = new Matrix<double>(new[,] { { 1, 4 }, { 0.25, 1 } });

            var matrixToCompare = new Matrix<double>(new[,] { { 11, 4 }, { 0.25, 1 } });

            // assert
            Assert.IsFalse(matrix.Equals(matrixToCompare));
        }

        [TestMethod]
        public void ToStringReturnsCorrectResult()
        {
            // arrange
            double[,] array = { { 1, 4 }, { 0.25, 1 } };
            var matrix = new Matrix<double>(array);

            // act
            string expected = "{{1, 4},{0.25, 1}}";
            string actual = matrix.ToString();

            // assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void GetEnumeratorCorrectResult()
        {
            // arrange
            double[,] array = { { 1, 4 }, { 0.25, 1 } };
            var matrix = new Matrix<double>(array);

            // act
            double[] expected = { 1, 4 ,  0.25, 1 };
            double[] actual = matrix.ToArray();

            // assert
            CollectionAssert.AreEqual(actual, expected);
        }
    }
}
