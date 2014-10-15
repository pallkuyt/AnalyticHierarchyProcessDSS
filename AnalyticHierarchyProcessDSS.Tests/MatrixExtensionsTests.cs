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
    public class MatrixExtensionsTests
    {
        [TestMethod]
        public void TruncatedMatrixWorksCorrectlyForMiddleElement()
        {
            var matrix = new Matrix<double>(
                new[,]
                {
                    {1, 2, 3, 4},
                    {0.5, 1, 3, 4},
                    {9, 2, 1, 0.6},
                    {5, 6, 7, 1}
                });

            var expectedTruncatedMatrix = new Matrix<double>(
                new[,]
                {
                    {1, 3, 4},
                    {9, 1, 0.6},
                    {5, 7, 1}
                });

            var actualTruncatedMatrix = matrix.TruncatedMatrix(1);


            Assert.AreEqual(expectedTruncatedMatrix, actualTruncatedMatrix);
        }

        [TestMethod]
        public void TruncatedMatrixWorksCorrectlyForFirstElement()
        {
            var matrix = new Matrix<double>(
                new[,]
                {
                    {1, 2, 3, 4},
                    {0.5, 1, 3, 4},
                    {9, 2, 1, 0.6},
                    {5, 6, 7, 1}
                });

            var expectedTruncatedMatrix = new Matrix<double>(
                new[,]
                {
                    {1, 3, 4},
                    {2, 1, 0.6},
                    {6, 7, 1}
                });

            var actualTruncatedMatrix = matrix.TruncatedMatrix(0);


            Assert.AreEqual(expectedTruncatedMatrix, actualTruncatedMatrix);
        }

        [TestMethod]
        public void TruncatedMatrixWorksCorrectlyForLastElement()
        {
            var matrix = new Matrix<double>(
                new[,]
                {
                    {1, 2, 3, 4},
                    {0.5, 1, 3, 4},
                    {9, 2, 1, 0.6},
                    {5, 6, 7, 1}
                });

            var expectedTruncatedMatrix = new Matrix<double>(
                new[,]
                {
                    {1, 2, 3},
                    {0.5, 1, 3},
                    {9, 2, 1}
                });

            var actualTruncatedMatrix = matrix.TruncatedMatrix(matrix.Size - 1);


            Assert.AreEqual(expectedTruncatedMatrix, actualTruncatedMatrix);
        }
    }
}
