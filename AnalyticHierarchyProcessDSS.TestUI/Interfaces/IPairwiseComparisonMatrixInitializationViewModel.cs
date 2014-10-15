using AnalyticHierarchyProcessDSS.Core;
using AnalyticHierarchyProcessDSS.TestUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnalyticHierarchyProcessDSS.TestUI.Interfaces
{
    interface IPairwiseComparisonMatrixInitializationViewModel
    {
        VerbalMatrix PairwiseComparisonMatrix { get; }

        double[] Weights { get; }

        double ConsistencyIndex { get; }

        string[] Alternatives { get; }

        string Criteria { get; }

        ICommand ResolveCommand { get; }

        ICommand NextMatrixCommand { get; }

        ICommand PreviousMatrixCommand { get; }
    }
}
