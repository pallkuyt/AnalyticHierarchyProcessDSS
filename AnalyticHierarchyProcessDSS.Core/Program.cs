using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Core.Fuzzy;
using AnalyticHierarchyProcessDSS.Core.Network;
using AnalyticHierarchyProcessDSS.Core.Precise;
using AnalyticHierarchyProcessDSS.WolframEngine;
using AnalyticHierarchyProcessDSS.WolframEngine.Alpha;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;
using Microsoft.Practices.Unity;

namespace AnalyticHierarchyProcessDSS.Core
{
    class Program
    {
        //public void LSM(double[,] MPP)
        //{
        //    string weights = String.Empty;

        //    int u = (int)Math.Sqrt(MPP.Length);
        //    string queryString = "P = " + MPP.ConvertToMathematicaArray() + @"; n = " + u.ToString() + "; ArgMin[{Sum[(P[[i, j]] - Subscript[w, i]/Subscript[w, j])^2, {i, 1, n}, {j, 1, n}], Sum[Subscript[w, i], {i, 1, n}] == 1, Resolve[(# > 0) & /@ Table[Subscript[w, i], {i, 1, n}]]}, Table[Subscript[w, i], {i, 1, n}]]//N";
        //    weights = ml.EvaluateToInputForm(queryString, 0);


        //    //queryString = @"w = " + weights + ";" + "P = " + MPP.ConvertToMathematicaArray() + @"; n = " + u.ToString() + "; Sqrt[Sum[(P[[i,j]] - w[[i]]/w[[j]])^2, {i, 1, n}, {j, 1, n}]]//N";

        //    double ci = 0;

        //    //ml.Close();
        //}
        static void Main(string[] args)
        {
            //IUnityContainer container = new UnityContainer()
            //    .RegisterType<IEvaluationEngine, WolframMathematicaEvaluationEngine>()
            //    .RegisterType<IWeightsResolutionStrategy, LeastSquaresResolutionStrategy>()
            //    .RegisterType<IReproachResolutionStrategy, TruncatedMatrixCiReproachResolutionStrategy>();

            //var engine = container.Resolve<IEvaluationEngine>();

            //var network = new NetworkStructure();




            //var fuzzy = new FuzzyPairwiseComparisonMatrix(new[]
            //{
            //    new[]
            //    {new FuzzyNumber(4, 5, 6), new FuzzyNumber(2, 3, 4), new FuzzyNumber(2, 3, 4), new FuzzyNumber(4, 5, 6)},
            //    new[]
            //    {new FuzzyNumber(2, 3, 4), new FuzzyNumber(), new FuzzyNumber()},
            //    new[]
            //    {new FuzzyNumber(0.25, 0.3333, 0.5), new FuzzyNumber(0.167, 0.2, 0.25)},
            //    new[]
            //    {new FuzzyNumber()}
            //});
            ////var fpp = new FuzzyPreferenceProgramming(fuzzy);
            ////var wwww = fpp.Program();


            //double[,] a =
            //{
            //    {1, 5, 3, 0.1429, 6}, {0.2, 1, 0.3333, 1, 0.3333}, {0.3333, 3, 1, 6, 3}, {7, 1, 0.1667, 1, 0.3333}, {0.1667, 3, 0.3333, 3, 1}
            //};
            //var l1 = new PairwiseComparisonMatrix(a, new LeastSquaresResolutionStrategy());
            //var l2 = new PairwiseComparisonMatrix(a, new MainEigenvectorResolutionStrategy());
            //var l3 = new PairwiseComparisonMatrix(a, new X2ResolutionStrategy());

            //var reproach = new CovarianceReproachResolutionStrategy().FindReproach(l2);

            //var mtrx =
            //    container.Resolve<PairwiseComparisonMatrix>(new ParameterOverride("matrix", new[,] { { 1.2, 2 }, { 1, 2 } }));

            //var engine1 = container.Resolve<IEvaluationEngine>();

            //var strategy = engine1.CreatemMinimizationStrategy("X2");

            //double[,] array =
            //{
            //    {1, 5, 3, 0.1429, 6}, {0.2, 1, 0.3333, 1, 0.3333}, {0.3333, 3, 1, 6, 3}, {7, 1, 0.1667, 1, 0.3333}, {0.1667, 3, 0.3333, 3, 1}
            //};

            //double[,] v2 =
            //{
            //    {1, 5, 0.1429, 6}, {0.2, 1, 1, 0.3333}, {7, 1, 1, 0.3333},
            //    {
            //        0.1667,
            //        3, 3, 1
            //    }
            //};
            //var lst = new LeastSquaresResolutionStrategy();
            //var matrix = new PairwiseComparisonMatrix(v2, lst);
            //var weights1 = matrix.Weights;



            //var u = engine1.GetMaxEigenPair(matrix);

            //var u2 = engine1.GetMaxEigenPair(new PairwiseComparisonMatrix(v2));

            ////double[,] array = null;

            ////var matrix = new PairwiseComparisonMatrix(array, new LeastSquaresResolutionStrategy());


            //var weights = matrix.Weights;
            //var ci = matrix.ConsistencyIndex;

            ////var temp = strategy.MinimizationArguments(matrix);

            //engine1.Dispose();


        }
    }
}
