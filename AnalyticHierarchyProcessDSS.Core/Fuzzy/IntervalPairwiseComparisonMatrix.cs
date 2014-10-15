using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Entities;

namespace AnalyticHierarchyProcessDSS.Core.Fuzzy
{
    public class IntervalPairwiseComparisonMatrix : Matrix<Interval>
    {
        public IntervalPairwiseComparisonMatrix(Interval[,] matrix)
            : base(matrix)
        {

        }
    }
}
