using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.WolframEngine;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;

namespace AnalyticHierarchyProcessDSS.Core.Fuzzy
{
    class NonLinearFuzzyPreferenceProgramming
    {
        private readonly IEvaluationEngine _engine = new WolframMathematicaEvaluationEngine();

        private FuzzyPairwiseComparisonMatrix _matrix;
    }
}
