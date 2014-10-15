using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.WolframEngine;
using AnalyticHierarchyProcessDSS.WolframEngine.Alpha;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;

namespace AnalyticHierarchyProcessDSS.Core.Precise
{
    public class MainEigenvectorResolutionStrategy : IWeightsResolutionStrategy
    {
        private readonly IEvaluationEngine _engine;

        public MainEigenvectorResolutionStrategy(IEvaluationEngine engine)
        {
            _engine = engine;
        }

        private double[] _weights;

        private double _consistencyIndex;

        private IMatrix<double> _requestedMatrix; 

        public double GetConsistencyIndex(IMatrix<double> matrix)
        {
            ProcessMatrix(matrix);
                
            return _consistencyIndex;
        }

        public double[] GetWeights(IMatrix<double> matrix)
        {
            ProcessMatrix(matrix);

            return _weights;
        }

        private void ProcessMatrix(IMatrix<double> matrix)
        {
            if (!matrix.Equals(_requestedMatrix))
            {
                _requestedMatrix = matrix;
                ResolveWeights(matrix);
            }
        }

        private void ResolveWeights(IMatrix<double> matrix)
        {
            var eigenPair = _engine.GetMaxEigenPair(matrix);

            _consistencyIndex = (eigenPair.EigenValue - matrix.Size) / (matrix.Size - 1);

            _weights = eigenPair.EigenVector.ToArray();
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
