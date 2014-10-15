using AnalyticHierarchyProcessDSS.Core.Network;
using AnalyticHierarchyProcessDSS.TestUI.Commands;
using AnalyticHierarchyProcessDSS.TestUI.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.TestUI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private NetworkStructure _network;
        private IUnityContainer _container;

        private IViewModel _currentViewModel;
        public IViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    NotifyPropertyChanged("CurrentViewModel");
                }
            }
        }

        private Dictionary<string, IViewModel> _navigationSequence;

        public MainWindowViewModel(NetworkStructure network, IUnityContainer container)
        {
            _network = network;
            _container = container;

            InitializeNavigationInfrastructure();
        }

        #region Navigation members

        private void InitializeNavigationInfrastructure()
        {
            _navigationSequence = new Dictionary<string, IViewModel>() 
            {
                {"NetworkInitialization",_container.Resolve<IViewModel>("NetworkInitialization")},
                {"ElementMatrices",_container.Resolve<IViewModel>("ElementMatrices")},
                {"ClusterMatrices",_container.Resolve<IViewModel>("ClusterMatrices")},
                {"WeightsMatrices",_container.Resolve<IViewModel>("WeightsMatrices")},
                {"SuperMatrix",_container.Resolve<IViewModel>("SuperMatrix")},              
            };

            InitializeNavigationItem("NetworkInitialization", null, "ElementMatrices");
            InitializeNavigationItem("ElementMatrices", "NetworkInitialization", "ClusterMatrices");
            InitializeNavigationItem("ClusterMatrices", "ElementMatrices", "WeightsMatrices");
            InitializeNavigationItem("WeightsMatrices", "ClusterMatrices", "SuperMatrix");
            InitializeNavigationItem("SuperMatrix", "WeightsMatrices", null);

            CurrentViewModel = _navigationSequence["NetworkInitialization"];
        }

        private void InitializeNavigationItem(string current, string previous, string next)
        {
            var currentViewModel = _navigationSequence[current];

            if (previous != null && _navigationSequence.ContainsKey(previous))
            {
                var previousViewModel = _navigationSequence[previous];
                if (previousViewModel != null)
                {
                    currentViewModel.BackToCommand = new NavigationCommand((parameter) =>
                        {
                            CurrentViewModel = previousViewModel;
                            CurrentViewModel.UpdateState();
                        }) { Description = previousViewModel.Description };
                }
            }

            if (next != null && _navigationSequence.ContainsKey(next))
            {
                var nextViewModel = _navigationSequence[next];
                if (nextViewModel != null)
                {
                    currentViewModel.MoveToCommand = new NavigationCommand((parameter) =>
                        {
                            CurrentViewModel = nextViewModel;
                            CurrentViewModel.UpdateState();
                        }) { Description = nextViewModel.Description };
                }
            }
        }

        #endregion
    }
}
