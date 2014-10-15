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
    public class X2ReproachResolutionStrategy : IReproachResolutionStrategy
    {
        public Reproach FindReproach(PairwiseComparisonMatrix matrix)
        {
            Matrix<double> T = matrix.EmpiricalMatrix();
            Matrix<double> delta = matrix.DeltaMatrix();

            double sigma = Math.Pow(delta.Variance(), 0.5);
            double a = delta.Mean();

            var reproach = (from i in Enumerable.Range(0, matrix.Size)
                from j in Enumerable.Range(0, matrix.Size)
                select new
                {
                    I = i,
                    J = j,
                    L = delta[i, j] > a + sigma
                        ? Math.Abs(delta[i, j] - a - sigma)
                        : delta[i, j] < a - sigma ? Math.Abs(delta[i, j] - a + sigma) : 0
                })
                .OrderByDescending(e => e.L)
                .FirstOrDefault();

            if (reproach != null)
            {
                return new Reproach(reproach.I, reproach.J);
            }

            return null;
        }
    }
}
