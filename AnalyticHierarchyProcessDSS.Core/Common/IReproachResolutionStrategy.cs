using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Precise;
using AnalyticHierarchyProcessDSS.Entities;

namespace AnalyticHierarchyProcessDSS.Core.Common
{
    public interface IReproachResolutionStrategy
    {
        Reproach FindReproach(PairwiseComparisonMatrix matrix);
    }
}
