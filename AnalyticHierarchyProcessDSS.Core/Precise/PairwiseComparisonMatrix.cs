using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Entities;

namespace AnalyticHierarchyProcessDSS.Core.Precise
{
    public class PairwiseComparisonMatrix : Matrix<double>
    {
        private readonly Lazy<double[]> _weights;

        private readonly Lazy<double> _consistencyIndex;

        private readonly IWeightsResolutionStrategy _weightsResolutionStrategy;

        private readonly IReproachResolutionStrategy _reproachResolutionStrategy;

        public PairwiseComparisonMatrix(double[,] matrix)
            : this(matrix, null, null)
        {
        }

        public PairwiseComparisonMatrix(double[][] comparisons)
        {
            double[,] matrix = new double[comparisons.Length + 1, comparisons.Length + 1];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (i < j)
                    {
                        matrix[i, j] = comparisons[i][j - i - 1];
                        matrix[j, i] =  1.0 / matrix[i, j];
                    }
                }

                matrix[i, i] = 1;
            }

            _matrix = matrix;
        }

        public PairwiseComparisonMatrix(double[,] matrix, IWeightsResolutionStrategy weightsResolutionStrategy)
            : this(matrix, weightsResolutionStrategy, null)
        {
            _weightsResolutionStrategy = weightsResolutionStrategy;
        }

        public PairwiseComparisonMatrix(double[,] matrix, IWeightsResolutionStrategy weightsResolutionStrategy, IReproachResolutionStrategy reproachResolutionStrategy)
            : base(matrix)
        {
            _weightsResolutionStrategy = weightsResolutionStrategy;
            _reproachResolutionStrategy = reproachResolutionStrategy;

            if (_weightsResolutionStrategy != null)
            {
                _weights = new Lazy<double[]>(() => _weightsResolutionStrategy.GetWeights(this));
                _consistencyIndex = new Lazy<double>(() => _weightsResolutionStrategy.GetConsistencyIndex(this)); 
            }
        }

        public double[] Weights
        {
            get { return _weights.Value; }
        }

        public double ConsistencyIndex
        {
            get { return _consistencyIndex.Value; }
        }

        static readonly double[] MRCI = { 0, 0, 0.52, 0.89, 1.11, 1.25, 1.35, 1.40, 1.45, 1.49, 1.52, 1.54, 1.56, 1.58, 1.59 };

        public double ConsistencyRatio
        {
            get
            {
                return ConsistencyIndex/MRCI[Size - 1];
            }
        }
    }
}
