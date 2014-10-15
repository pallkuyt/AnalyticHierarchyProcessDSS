using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.Entities.Extensions;

namespace AnalyticHierarchyProcessDSS.Core.Precise
{
    public class CovarianceReproachResolutionStrategy : IReproachResolutionStrategy
    {
        public Reproach FindReproach(PairwiseComparisonMatrix matrix)
        {
            double[] tempVector;
            double[] rowCovariance = new double[matrix.Size];
            double[] columnCovariance = new double[matrix.Size];

            for (int i = 0; i < matrix.Size; i++)
            {
                tempVector = new double[matrix.Size];

                for (int j = 0; j < matrix.Size; j++)
                {
                    if (i != j)
                    {
                        tempVector[j] = matrix.MatrixRow(i).Cov(matrix.MatrixRow(j));
                    }
                }

                rowCovariance[i] = tempVector.Mean() * matrix.Size / (matrix.Size - 1);
            }

            for (int i = 0; i < matrix.Size; i++)
            {
                tempVector = new double[matrix.Size];

                for (int j = 0; j < matrix.Size; j++)
                {
                    if (i != j)
                    {
                        tempVector[j] = matrix.MatrixColumn(i).Cov(matrix.MatrixColumn(j));
                    }
                }

                columnCovariance[i] = tempVector.Mean() * matrix.Size / (matrix.Size - 1);
            }

            return new Reproach(Array.IndexOf(rowCovariance, rowCovariance.Min()),
                Array.IndexOf(columnCovariance, columnCovariance.Min()));
        }
    }
}
