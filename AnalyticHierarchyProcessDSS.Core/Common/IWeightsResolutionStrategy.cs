using AnalyticHierarchyProcessDSS.Core.Precise;
using AnalyticHierarchyProcessDSS.Entities;

namespace AnalyticHierarchyProcessDSS.Core.Common
{
    public interface IWeightsResolutionStrategy
    {
        double GetConsistencyIndex(IMatrix<double> matrix);

        double GetConsistencyIndex(VerbalMatrix matrix);

        double[] GetWeights(IMatrix<double> matrix);

        double[] GetWeights(VerbalMatrix matrix);
    }
}
