using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Wolfram.NETLink;

namespace AnalyticHierarchyProcessDSS.WolframEngine
{
    public class UnityFactory
    {
        public static IUnityContainer CreateContainer()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterInstance(MathLinkFactory.CreateKernelLink())
                .RegisterType<IMinimizationStrategy, X2MinimizationStrategy>("X2")
                .RegisterType<IMinimizationStrategy, LeastSquaresMinimizationStrategy>("LeastSquares");

            return container;
        }
    }
}
