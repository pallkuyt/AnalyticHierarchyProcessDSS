using System;
using System.Linq;
using AnalyticHierarchyProcessDSS.Entities;
using Wolfram.NETLink;

namespace AnalyticHierarchyProcessDSS.WolframEngine.Mathematica
{
    public class LeastSquaresMinimizationStrategy : IMinimizationStrategy
    {
        private readonly IKernelLink _kernel;

        public LeastSquaresMinimizationStrategy(IKernelLink kernel)
        {
            _kernel = kernel;
        }

        public double MinimizationValue(IMatrix<double> matrix)
        {
            var queryString = string.Format(MathematicalConstants.OptimizationQueryTemplate, matrix, matrix.Size, MathematicalConstants.MinimumValue, MathematicalConstants.LeastSquaresTargetFunction);

            var queryResult = _kernel.EvaluateToInputForm(queryString, 0);

            return double.Parse(queryResult);
        }

        public double[] MinimizationArguments(Entities.IMatrix<double> matrix)
        {
            var queryString = string.Format(MathematicalConstants.OptimizationQueryTemplate, matrix, matrix.Size, MathematicalConstants.ArgMinQuery, MathematicalConstants.LeastSquaresTargetFunction);

            var queryResult = _kernel.EvaluateToInputForm(queryString, 0);

            var result =
                queryResult.Split(new[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();

            return result;
        }
    }
}
