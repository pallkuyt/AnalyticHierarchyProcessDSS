using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.WolframEngine;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;

namespace AnalyticHierarchyProcessDSS.Core.Precise
{
    public class X2ResolutionStrategy : IWeightsResolutionStrategy
    {
        private readonly IEvaluationEngine _engine;

        private readonly IMinimizationStrategy _minimizationStrategy;

        public X2ResolutionStrategy(IEvaluationEngine engine)
        {
            _engine = engine;
            _minimizationStrategy = _engine.CreatemMinimizationStrategy("X2");
        }

        private double[] _weights;

        private double _consistencyIndex;

        public double GetConsistencyIndex(IMatrix<double> matrix)
        {
            var consistencyIndex = _minimizationStrategy.MinimizationValue(matrix);
            return consistencyIndex;
        }

        public double[] GetWeights(IMatrix<double> matrix)
        {
            var weights = _minimizationStrategy.MinimizationArguments(matrix);
            return weights;
        }


        public double[] GetWeights(VerbalMatrix matrix)
        {
            return GetWeights(matrix.ToPairwiseComparisonMatrix());
        }


        public double GetConsistencyIndex(VerbalMatrix matrix)
        {
            return GetConsistencyIndex(matrix.ToPairwiseComparisonMatrix());
        }
    }
}