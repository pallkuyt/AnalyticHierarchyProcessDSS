using System.Linq;
using System.Text.RegularExpressions;
using AnalyticHierarchyProcessDSS.Entities;
using WolframAlphaNET;
using WolframAlphaNET.Objects;

namespace AnalyticHierarchyProcessDSS.WolframEngine.Alpha
{
    public class WolframAlphaEvaluationEngine : IEvaluationEngine
    {
        private readonly WolframAlpha _wolframEngine = new WolframAlpha("XLV95V-VUJU3R6YWK");

        const string EigenSystemPattern = @"^(?'name'\w+)~~\((?'eigenVector'.*)\),\s*(?'name'\w+)~~(?'eigenValue'.*)$";

        public EigenSystem GetEigenSystem(IMatrix<double> matrix)
        {
            QueryResult results = _wolframEngine.Query(string.Join(" ", "eigensystem", matrix));
            var eigenSystemResult = results.Pods.Where(p => p.Title == "Result").Select(p => p.SubPods).First().First().Plaintext;

            var eigenSystemItems = eigenSystemResult.Split('\n');

            var eigenSystem = from item in eigenSystemItems
                              let match = Regex.Match(item, EigenSystemPattern)
                              let eigenValue = double.Parse(match.Groups["eigenValue"].Value)
                              let eigenVector = match.Groups["eigenVector"].Value.Split(',').Select(double.Parse).ToArray()
                              select new
                              {
                                  EigenValue = eigenValue,
                                  EigenVector = new EigenVector(eigenVector)
                              };

            double[] eigenValues = eigenSystem.Select(e => e.EigenValue).ToArray();
            EigenVector[] eigenVectors = eigenSystem.Select(e => e.EigenVector).ToArray();

            return new EigenSystem(eigenValues, eigenVectors);
        }

        public IMinimizationStrategy CreatemMinimizationStrategy(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }


        public EigenPair GetMaxEigenPair(IMatrix<double> matrix)
        {
            throw new System.NotImplementedException();
        }


        public double[] IntervalPreferenceProgramming(double[,] matrix, double[] admissionParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
