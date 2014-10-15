using System;
using System.Linq;
using System.Text.RegularExpressions;
using AnalyticHierarchyProcessDSS.Entities;
using Microsoft.Practices.Unity;
using Wolfram.NETLink;

namespace AnalyticHierarchyProcessDSS.WolframEngine.Mathematica
{
    public class WolframMathematicaEvaluationEngine : IEvaluationEngine
    {
        private readonly IKernelLink _kernel;

        private readonly IUnityContainer _container = UnityFactory.CreateContainer();
        
        public WolframMathematicaEvaluationEngine()
        {
            _kernel = _container.Resolve<IKernelLink>();
            _kernel.WaitAndDiscardAnswer();
        }

        public EigenSystem GetEigenSystem(IMatrix<double> matrix)
        {
            var queryString = "R = {{1, 5, 0.1429, 6}, {0.2, 1, 1, 0.3333}, {7, 1, 1, 0.3333}, {0.1667, 3, 3, 1}}; Max[Re[Eigenvalues[R]]]; Max[Re[Eigenvalues[R]]] // N";

            var queryResult = _kernel.EvaluateToInputForm(queryString, 0);

            var match = Regex.Match(queryResult, MathematicalConstants.EigenSystemResultFormat);

            var eigenValues = match.Groups["values"].Value.Split(',').Select(n => new ComplexNumber(n).Real).ToArray();

            var eigenVectors = (from v in match.Groups["vectors"].Value.Split('{', '}')
                let eigenVector = v.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                where eigenVector.Length == eigenValues.Length
                select new EigenVector(eigenVector.Select(e => new ComplexNumber(e).Real).ToArray())).ToArray();

            return new EigenSystem(eigenValues, eigenVectors);
        }

        public IMinimizationStrategy CreatemMinimizationStrategy(string name)
        {
            return _container.Resolve<IMinimizationStrategy>(name);
        }

        public void Dispose()
        {
            _kernel.Close();
        }

        public EigenPair GetMaxEigenPair(IMatrix<double> matrix)
        {
            string valueQueryString = string.Format(MathematicalConstants.MaxEigenValueQueryFormat, matrix);
            string vectorQueryString = string.Format(MathematicalConstants.MaxEigenVectorQueryFormat, matrix);

            string valueQueryResult = _kernel.EvaluateToInputForm(valueQueryString, 0);
            string vectorQueryResult = _kernel.EvaluateToInputForm(vectorQueryString, 0);

            var vectorArray = vectorQueryResult.Split(new[] {'{', ',', '}'}, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            var value = double.Parse(valueQueryResult);

            return new EigenPair(value, new EigenVector(vectorArray));
        }


        public double[] IntervalPreferenceProgramming(double[,] matrix, double[] admissionParameters)
        {
            string queryString = string.Format(
                MathematicalConstants.FuzzyPreferenceProgrammingQueryTemplate,
                matrix.ToMathematicaString(),
                matrix.GetLength(0),
                matrix.GetLength(1),
                admissionParameters.ToMathematicaString()
                );

            string queryResult = _kernel.EvaluateToInputForm(queryString, 0);
            var weightsArray = queryResult.Split(new[] { '{', ',', '}' }, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            
            return weightsArray;
        }
    }
}
