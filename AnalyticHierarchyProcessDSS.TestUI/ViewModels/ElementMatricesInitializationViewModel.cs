using AnalyticHierarchyProcessDSS.Core.Network;
using AnalyticHierarchyProcessDSS.TestUI.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnalyticHierarchyProcessDSS.TestUI.ViewModels
{
    class ElementMatricesInitializationViewModel : BaseViewModel, IViewModel
    {
        private readonly NetworkStructure _network;

        private readonly IUnityContainer _container;

        private LinkedList<CrossClusterDependencyViewModel> _clusterDependencies;

        public ElementMatricesInitializationViewModel(NetworkStructure network, IUnityContainer container)
        {
            _network = network;
            _container = container;

            NextDependencyCommand = new RelayCommand((parameter) =>
            {
                if (_currentDependencyNode.Next != null)
                {
                    CurrentDependencyNode = _currentDependencyNode.Next;
                }
            });

            PreviousDependencyCommand = new RelayCommand((parameter) =>
            {
                if (_currentDependencyNode.Previous != null)
                {
                    CurrentDependencyNode = _currentDependencyNode.Previous;
                }
            });
        }

        #region IViewModel members

        public ICommand MoveToCommand { get; set; }

        public ICommand BackToCommand { get; set; }

        public string Description { get; set; }

        #endregion

        public CrossClusterDependencyViewModel CurrentDependency
        {
            get { return _currentDependencyNode.Value; }
        }

        public void UpdateState()
        {
            _network.UpdateNetwork();

            _clusterDependencies = new LinkedList<CrossClusterDependencyViewModel>(_network.ClusterDependencies.Select(cd => new CrossClusterDependencyViewModel(cd, _container)));

            CurrentDependencyNode = _clusterDependencies.First;
        }

        public ICommand NextDependencyCommand { get; set; }

        public ICommand PreviousDependencyCommand { get; set; }

        private LinkedListNode<CrossClusterDependencyViewModel> _currentDependencyNode;

        public LinkedListNode<CrossClusterDependencyViewModel> CurrentDependencyNode
        {
            get { return _currentDependencyNode; }
            private set
            {
                _currentDependencyNode = value;
                NotifyPropertyChanged("CurrentDependency");
            }
        }
    }
}
