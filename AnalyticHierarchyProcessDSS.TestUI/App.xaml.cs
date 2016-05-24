using System.Threading;
using System.Windows.Threading;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Core.Fuzzy;
using AnalyticHierarchyProcessDSS.Core.Network;
using AnalyticHierarchyProcessDSS.Core.Precise;
using AnalyticHierarchyProcessDSS.TestUI.Interfaces;
using AnalyticHierarchyProcessDSS.TestUI.ViewModels;
using AnalyticHierarchyProcessDSS.WolframEngine;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AnalyticHierarchyProcessDSS.WolframEngine.Alpha;

namespace AnalyticHierarchyProcessDSS.TestUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherUnhandledException += OnDispatcherUnhandledException;

            IUnityContainer container = new UnityContainer();
            container.RegisterInstance<IUnityContainer>(container);
            container.RegisterInstance<IEvaluationEngine>(new WolframAlphaEvaluationEngine());
            container.RegisterInstance<NetworkStructure>(new NetworkStructure());
            container.RegisterType<IWeightsResolutionStrategy, MainEigenvectorResolutionStrategy>("EM");
            container.RegisterType<IWeightsResolutionStrategy, LeastSquaresResolutionStrategy>("LSM");
            container.RegisterType<IWeightsResolutionStrategy, X2ResolutionStrategy>("X2");
            container.RegisterType<IWeightsResolutionStrategy, FuzzyPreferenceProgramming>("FPP");

            container.RegisterType<IViewModel, NetworkInitializationViewModel>("NetworkInitialization", new InjectionProperty("Description", "ПОБУДОВА МЕРЕЖІ"));
            container.RegisterType<IViewModel, ElementMatricesInitializationViewModel>("ElementMatrices", new InjectionProperty("Description", "ІНІЦІАЛІЗАЦІЯ МАТРИЦЬ ПАРНИХ ПОРІВНЯНЬ ЕЛЕМЕНТІВ"));
            container.RegisterType<IViewModel, ClusterMatricesInitializationViewModel>("ClusterMatrices", new InjectionProperty("Description", "ІНІЦІАЛІЗАЦІЯ МАТРИЦЬ ПАРНИХ ПОРІВНЯНЬ КЛАСТЕРІВ"));
            container.RegisterType<IViewModel, WeightsMatricesViewModel>("WeightsMatrices", new InjectionProperty("Description", "МАТРИЦІ WE І WC"));
            container.RegisterType<IViewModel, SuperMatrixViewModel>("SuperMatrix", new InjectionProperty("Description", "СУПЕРМАТРИЦЯ WWE"));

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var error = e.Exception.Message;

            MessageBox.Show(error);

            App.Current.Shutdown();
        }
    }
}
