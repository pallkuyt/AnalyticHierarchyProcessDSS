using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;

namespace AnalyticHierarchyProcessDSS.WolframEngine
{
    public interface IMinimizationStrategy
    {
        double MinimizationValue(IMatrix<double> matrix);

        double[] MinimizationArguments(IMatrix<double> matrix);
    }
}
