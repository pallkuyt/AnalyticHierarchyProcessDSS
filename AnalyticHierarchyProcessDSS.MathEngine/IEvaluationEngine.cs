using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Entities;

namespace AnalyticHierarchyProcessDSS.WolframEngine
{
    public interface IEvaluationEngine : IDisposable
    {
        EigenSystem GetEigenSystem(IMatrix<double> matrix);

        IMinimizationStrategy CreatemMinimizationStrategy(string name);

        EigenPair GetMaxEigenPair(IMatrix<double> matrix);

        double[] IntervalPreferenceProgramming(double[,] matrix, double[] admissionParameters);
    }
}
