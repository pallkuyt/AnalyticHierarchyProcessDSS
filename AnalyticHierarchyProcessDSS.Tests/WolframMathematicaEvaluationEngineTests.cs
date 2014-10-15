using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.WolframEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnalyticHierarchyProcessDSS.Tests
{
    [TestClass]
    public class WolframMathematicaEvaluationEngineTests
    {
        private const string TestMatrix = "{{1, 4, 7}, {0.25, 1, 3}, {6.25, 9, 1}}";
        private const int TestMatrixSize = 3;

        const string LeastSquaresArgminQueryFormat = "P = {{1, 4, 7}, {0.25, 1, 3}, {6.25, 9, 1}}; n = 3;ArgMin[{Sum[(P[[i, j]] - Subscript[w, i]/Subscript[w, j])^2, {i, 1, n}, {j, 1, n}], Sum[Subscript[w, i], {i, 1, n}] == 1, Resolve[(# > 0) & /@ Table[Subscript[w, i], {i, 1, n}]]}, Table[Subscript[w, i], {i, 1, n}]]//N";
        const string X2ArgminQueryFormat = "P = {{1, 4, 7}, {0.25, 1, 3}, {6.25, 9, 1}}; n = 3;ArgMin[{Sum[(P[[i, j]]*Subscript[w, j]^2/Subscript[w, i]^2 - Subscript[w, j]/Subscript[w, i])^2, {i, 1, n}, {j, 1, n}], Sum[Subscript[w, i], {i, 1, n}] == 1, Resolve[(# > 0) & /@ Table[Subscript[w, i], {i, 1, n}]]}, Table[Subscript[w, i], {i, 1, n}]]//N";

        const string LeastSquaresMinValueQueryFormat = "P = {{1, 4, 7}, {0.25, 1, 3}, {6.25, 9, 1}}; n = 3;MinValue[{Sum[(P[[i, j]] - Subscript[w, i]/Subscript[w, j])^2, {i, 1, n}, {j, 1, n}], Sum[Subscript[w, i], {i, 1, n}] == 1, Resolve[(# > 0) & /@ Table[Subscript[w, i], {i, 1, n}]]}, Table[Subscript[w, i], {i, 1, n}]]//N";
        const string X2MinValueQueryFormat = "P = {{1, 4, 7}, {0.25, 1, 3}, {6.25, 9, 1}}; n = 3;MinValue[{Sum[(P[[i, j]]*Subscript[w, j]^2/Subscript[w, i]^2 - Subscript[w, j]/Subscript[w, i])^2, {i, 1, n}, {j, 1, n}], Sum[Subscript[w, i], {i, 1, n}] == 1, Resolve[(# > 0) & /@ Table[Subscript[w, i], {i, 1, n}]]}, Table[Subscript[w, i], {i, 1, n}]]//N";  
        
        [TestMethod]
        public void LeastSquaresArgminQueryInRightFormat()
        {
            // arrange
            var actualQuery = string.Format(MathematicalConstants.OptimizationQueryTemplate, TestMatrix,
                TestMatrixSize, MathematicalConstants.ArgMinQuery,
                MathematicalConstants.LeastSquaresTargetFunction);

            Assert.AreEqual(actualQuery, LeastSquaresArgminQueryFormat);
        }

        [TestMethod]
        public void X2ArgminQueryInRightFormat()
        {
            // arrange
            var actualQuery = string.Format(MathematicalConstants.OptimizationQueryTemplate, TestMatrix,
                TestMatrixSize, MathematicalConstants.ArgMinQuery,
                MathematicalConstants.X2TargetFunction);

            Assert.AreEqual(actualQuery, X2ArgminQueryFormat);
        }

        [TestMethod]
        public void LeastSquaresMinValueQueryInRightFormat()
        {
            // arrange
            var actualQuery = string.Format(MathematicalConstants.OptimizationQueryTemplate, TestMatrix,
                TestMatrixSize, MathematicalConstants.MinimumValue,
                MathematicalConstants.LeastSquaresTargetFunction);

            Assert.AreEqual(actualQuery, LeastSquaresMinValueQueryFormat);
        }

        [TestMethod]
        public void X2MinValueQueryInRightFormat()
        {
            // arrange
            var actualQuery = string.Format(MathematicalConstants.OptimizationQueryTemplate, TestMatrix,
                TestMatrixSize, MathematicalConstants.MinimumValue,
                MathematicalConstants.X2TargetFunction);

            Assert.AreEqual(actualQuery, X2MinValueQueryFormat);
        }
    }
}
