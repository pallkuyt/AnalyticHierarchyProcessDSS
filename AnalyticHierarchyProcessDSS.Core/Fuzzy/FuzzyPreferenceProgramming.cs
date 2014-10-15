using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Core.Precise;
using AnalyticHierarchyProcessDSS.WolframEngine;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;
using AnalyticHierarchyProcessDSS.Entities;

namespace AnalyticHierarchyProcessDSS.Core.Fuzzy
{
    public class FuzzyPreferenceProgramming : IWeightsResolutionStrategy
    {
        private readonly IEvaluationEngine _engine;

        private FuzzyPairwiseComparisonMatrix _matrix;

        private double[] _admissionParameters;

        private double _alphaSamplingStep = 0.1;

        public double AlphaSamplingStep
        {
            get { return _alphaSamplingStep; }
            set { _alphaSamplingStep = value; }
        }

        private double[] _weights;

        private double _consistencyIndex;

        private Dictionary<double, IntervalPreferenceProgrammingSolution> _alphaWeights;

        public Dictionary<double, IntervalPreferenceProgrammingSolution> AlphaWeights
        {
            get { return _alphaWeights; }
            set { _alphaWeights = value; }
        }

        public FuzzyPreferenceProgramming(IEvaluationEngine engine) { _engine = engine; }

        private PairwiseComparisonSolution IntervalPreferenceProgramming(IntervalPairwiseComparisonMatrix matrix)
        {
            int n = matrix.Size;

            int m = n * (n - 1);
            int k = 0;

            double[,] R = new double[n * (n - 1), n];

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    if (i >= j) continue;

                    R[k, i] = 1;
                    R[k, j] = -matrix[i, j].UpperLimit;

                    R[k + 1, i] = -1;
                    R[k + 1, j] = matrix[i, j].LowerLimit;

                    k++;
                    k++;
                }
            }

            double[] weights = _engine.IntervalPreferenceProgramming(R, _admissionParameters);

            return new PairwiseComparisonSolution() {ConsistencyIndex = weights[0], Weights = weights.Skip(1).ToArray()};
        }

        public double[] Program()
        {
            int stepsCount = (int)Math.Floor(1 / _alphaSamplingStep) + 1;
            _alphaWeights = new Dictionary<double, IntervalPreferenceProgrammingSolution>();

            // initializing alpha normalizing array
            double sum = Enumerable.Range(0, stepsCount).Sum(i => i * _alphaSamplingStep);
            double[] alphaNormalize = Enumerable.Range(0, stepsCount).Select(i => i * _alphaSamplingStep / sum).ToArray();

            for (int i = 0; i < stepsCount; i++)
            {
                var ippMatrix = _matrix.GetAlphaLevelMatrix(i * _alphaSamplingStep);
                PairwiseComparisonSolution solution = IntervalPreferenceProgramming(ippMatrix);

                _alphaWeights.Add(i * _alphaSamplingStep, new IntervalPreferenceProgrammingSolution(ippMatrix, solution.Weights, solution.ConsistencyIndex));
            }

            for (int j = 0; j <= _matrix.Size; j++)
            {
                double product = 1;

                if (j != _matrix.Size)
                {
                    for (int i = 0; i < stepsCount; i++)
                    {
                        product *= Math.Pow(_alphaWeights[i*_alphaSamplingStep].Weights[j], alphaNormalize[i]);
                    }
                    _weights[j] = product; 
                }
                else
                {
                    for (int i = 0; i < stepsCount; i++)
                    {
                        product *= Math.Pow(_alphaWeights[i * _alphaSamplingStep].ConsistencyIndex, alphaNormalize[i]);
                    }
                    _consistencyIndex = product; 
                }
            }

            return _weights;
        }

        public double GetConsistencyIndex(IMatrix<double> matrix)
        {
            return _consistencyIndex;
        }

        public double[] GetWeights(IMatrix<double> matrix)
        {
            return _weights;
        }

        public double[] GetWeights(VerbalMatrix matrix)
        {
            _matrix = matrix.ToFuzzyPairwiseComparisonMatrix();
            _admissionParameters = Enumerable.Range(0, matrix.Size * (matrix.Size - 1)).Select(i => 1.0).ToArray();
            _weights = new double[_matrix.Size];

            return Program();
        }


        public double GetConsistencyIndex(VerbalMatrix matrix)
        {
            return _consistencyIndex;
        }
    }
}
