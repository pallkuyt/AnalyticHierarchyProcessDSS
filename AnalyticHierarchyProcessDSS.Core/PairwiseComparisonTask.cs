using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Core.Precise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.Core
{
    public class PairwiseComparisonTask
    {
        public PairwiseComparisonTask(int size)
        {
            Weights = new double[size];

            Matrix = new VerbalMatrix(size);
        }

        public VerbalMatrix Matrix { get; set; }

        public double[] Weights { get; set; }

        public double ConsistencyIndex { get; set; }

        public IWeightsResolutionStrategy WeightsResolutionStrategy { get; set; }

        /// <summary>
        /// Returns weights
        /// </summary>
        /// <returns></returns>
        public double[] Resolve()
        {
            if (WeightsResolutionStrategy != null)
            {
                Weights = WeightsResolutionStrategy.GetWeights(Matrix);
                ConsistencyIndex = WeightsResolutionStrategy.GetConsistencyIndex(Matrix); 
            }

            return Weights;
        }
    }
}
