using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.Entities.Extensions;

namespace AnalyticHierarchyProcessDSS.Core.Precise
{
    class TruncatedMatrixCiReproachResolutionStrategy : IReproachResolutionStrategy
    {
        private readonly IWeightsResolutionStrategy _weightsResolutionStrategy;

        public TruncatedMatrixCiReproachResolutionStrategy(IWeightsResolutionStrategy weightsResolutionStrategy)
        {
            _weightsResolutionStrategy = weightsResolutionStrategy;
        }

        public Reproach FindReproach(PairwiseComparisonMatrix matrix)
        {
            int[] ciArray = (from i in Enumerable.Range(0, matrix.Size)
                select new
                {
                    Index = i,
                    CI = _weightsResolutionStrategy.GetConsistencyIndex(matrix.TruncatedMatrix(i))
                })
                .OrderBy(e => e.CI)
                .Select(e => e.Index)
                .Take(2)
                .ToArray();

            if (ciArray.Length == 2)
            {
                return new Reproach(ciArray[0], ciArray[1]);
            }

            return null;
        }
    }
}
