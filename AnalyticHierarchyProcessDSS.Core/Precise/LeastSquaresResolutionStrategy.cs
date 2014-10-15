using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.WolframEngine;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;

namespace AnalyticHierarchyProcessDSS.Core.Precise
{
    public class LeastSquaresResolutionStrategy : IWeightsResolutionStrategy
    {
        private readonly IEvaluationEngine _engine;

        private readonly IMinimizationStrategy _minimizationStrategy;

        public LeastSquaresResolutionStrategy(IEvaluationEngine engine)
        {
            _engine = engine;
            _minimizationStrategy = _engine.CreatemMinimizationStrategy("LeastSquares");
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
