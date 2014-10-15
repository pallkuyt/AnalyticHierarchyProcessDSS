using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Entities;

namespace AnalyticHierarchyProcessDSS.Core.Fuzzy
{
    public class IntervalPreferenceProgrammingSolution
    {
        private readonly double _consistencyIndex;

        private readonly double[] _weights;

        private readonly IntervalPairwiseComparisonMatrix _matrix;

        public IntervalPreferenceProgrammingSolution(IntervalPairwiseComparisonMatrix matrix, double[] weights, double consistencyIndex)
        {
            _matrix = matrix;
            _weights = weights;
            _consistencyIndex = consistencyIndex;
        }

        public double ConsistencyIndex
        {
            get { return _consistencyIndex; }
        }

        public double[] Weights
        {
            get { return _weights; }
        }

        public IntervalPairwiseComparisonMatrix Matrix
        {
            get { return _matrix; }
        }

        public override string ToString()
        {
            return string.Format("CI={0}; Weights={1}", ConsistencyIndex, Weights.ToMathematicaString());
        }
    }
}
