using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.WolframEngine
{
    public static class MathematicalConstants
    {
        #region Wolfram Mathematica Constants
        
        internal const string MaxEigenVectorQueryFormat = "R = {0}; Eigensystem[R][[2]][[Position[Eigenvalues[R], Max[Re[Eigenvalues[R]]]][[1]][[1]]]]//N";

        internal const string MaxEigenValueQueryFormat = "R = {0}; Max[Re[Eigensystem[R][[1]]]]";

        internal const string EigenSystemQueryFormat = @"Eigensystem[{0}]";
        //internal const string EigenSystemQueryFormat =
            //"R = {0}; Eigensystem[R][[2]][[Position[Eigenvalues[R], Max[Re[Eigenvalues[R]]]][[1]][[1]]]] // N";

        internal const string EigenSystemResultFormat = @"^{{(?'values'.*?)}, {(?'vectors'.*?)}}$";

        internal const string OptimizationQueryTemplate = "P = {0}; n = {1};{2}[{{{3}, Sum[Subscript[w, i], {{i, 1, n}}] == 1, Resolve[(# > 0) & /@ Table[Subscript[w, i], {{i, 1, n}}]]}}, Table[Subscript[w, i], {{i, 1, n}}]]//N";

        internal const string ArgMinQuery = "ArgMin";

        internal const string MinimumValue = "MinValue";

        internal const string LeastSquaresTargetFunction =
            "Sum[(P[[i, j]] - Subscript[w, i]/Subscript[w, j])^2, {i, 1, n}, {j, 1, n}]";

        internal const string X2TargetFunction =
            "Sum[(P[[i, j]]*Subscript[w, j]^2/Subscript[w, i]^2 - Subscript[w, j]/Subscript[w, i])^2, {i, 1, n}, {j, 1, n}]";

        internal const string FuzzyPreferenceProgrammingQueryTemplate = "R = {0}; m = {1}; n = {2}; d = {3}; ArgMin[{{-lambda, Sum[Subscript[w, i], {{i, 1, n}}] == 1, Resolve[(# >= 0) & /@ Table[Subscript[w, i], {{i, 1, n}}]],Resolve[(# <= 0) & /@ Table[R[[i]].Table[Subscript[w, i], {{i, 1, n}}] + d[[i]]*lambda - d[[i]], {{i, 1, m}}]]}}, Flatten[{{lambda, Table[Subscript[w, i], {{i, 1, n}}]}}, 1]]//N";

        #endregion
    }
}
