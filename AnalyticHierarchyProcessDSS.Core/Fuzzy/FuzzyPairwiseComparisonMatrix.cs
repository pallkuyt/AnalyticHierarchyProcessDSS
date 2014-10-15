using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Entities;

namespace AnalyticHierarchyProcessDSS.Core.Fuzzy
{
    public class FuzzyPairwiseComparisonMatrix : Matrix<FuzzyNumber>
    {
        public FuzzyPairwiseComparisonMatrix(FuzzyNumber[,] matrix)
            : base(matrix)
        {
        }

        public FuzzyPairwiseComparisonMatrix(FuzzyNumber[][] comparisons)
        {
            FuzzyNumber[,] matrix = new FuzzyNumber[comparisons.Length + 1, comparisons.Length + 1];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (i < j)
                    {
                        matrix[i, j] = comparisons[i][j - i - 1];
                        matrix[j, i] = new FuzzyNumber(matrix[i, j]);
                    }
                }

                matrix[i, i] = new FuzzyNumber();
            }

            _matrix = matrix;
        }

        public IntervalPairwiseComparisonMatrix GetAlphaLevelMatrix(double alpha)
        {
            Interval[,] matrix = new Interval[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    matrix[i, j] = new Interval(
                        this[i, j].Lower + alpha * (this[i, j].Middle - this[i, j].Lower),
                        this[i, j].Upper + alpha * (this[i, j].Middle - this[i, j].Upper)
                        );
                }
            }

            return new IntervalPairwiseComparisonMatrix(matrix);
        }
    }
}
