using AnalyticHierarchyProcessDSS.Core;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Core.Fuzzy;
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
    class ClusterMatricesInitializationViewModel : BaseViewModel, IViewModel, IPairwiseComparisonMatrixInitializationViewModel
    {
        private readonly NetworkStructure _network;

        private readonly IUnityContainer _container;

        public string[] DependentElements
        {
            get
            {
                var clusterDependents = from d in _network.ClusterDependencies
                                     where d.Master.Equals(CurrentCluster)
                                     select d.Dependent.Name;

                return clusterDependents.ToArray();
            }
        }

        public ClusterMatricesInitializationViewModel(NetworkStructure network, IUnityContainer container)
        {
            _network = network;
            _container = container;

            NextMatrixCommand = new RelayCommand((parameter) =>
            {
                if (_currentClusterNode.Next != null)
                {
                    CurrentClusterNode = _currentClusterNode.Next;
                    UpdateResolutionStrategyName();
                }
            });

            PreviousMatrixCommand = new RelayCommand((parameter) =>
            {
                if (_currentClusterNode.Previous != null)
                {
                    CurrentClusterNode = _currentClusterNode.Previous;
                    UpdateResolutionStrategyName();
                }
            });

            ResolveCommand = new RelayCommand((parameter) =>
            {
                _network.ClusterComparisons[CurrentCluster].Resolve();
                NotifyPropertyChanged("Weights");
                NotifyPropertyChanged("ConsistencyIndex");
                NotifyPropertyChanged("HasAlphaData");
                NotifyPropertyChanged("AlphaData");
                NotifyPropertyChanged("AlphaDataColumnHeaders");
                NotifyPropertyChanged("AlphaDataRowHeaders");
            });
        }

        private string _resolutionStrategyName;

        public string ResolutionStrategyName
        {
            get { return _resolutionStrategyName; }
            set
            {
                if (_resolutionStrategyName != value)
                {
                    _resolutionStrategyName = value;
                    _network.ClusterComparisons[CurrentCluster].WeightsResolutionStrategy = _container.Resolve<IWeightsResolutionStrategy>(_resolutionStrategyName);
                }
            }
        }

        public Cluster CurrentCluster { get { return _currentClusterNode.Value; } }

        public VerbalMatrix PairwiseComparisonMatrix
        {
            get { return _network.ClusterComparisons[CurrentCluster].Matrix; }
        }

        #region IViewModel members

        public ICommand MoveToCommand { get; set; }

        public ICommand BackToCommand { get; set; }

        public string Description { get; set; }

        public void UpdateState()
        {
            _clusters = new LinkedList<Cluster>(_network.Clusters);
            CurrentClusterNode = _clusters.First;
        }

        public ICommand NextClusterCommand { get; set; }

        public ICommand PreviousClusterCommand { get; set; }

        private LinkedList<Cluster> _clusters;

        private LinkedListNode<Cluster> _currentClusterNode;

        public LinkedListNode<Cluster> CurrentClusterNode
        {
            get { return _currentClusterNode; }
            private set
            {
                _currentClusterNode = value;
                NotifyPropertyChanged("CurrentCluster");
                NotifyPropertyChanged("Criteria");
                NotifyPropertyChanged("PairwiseComparisonMatrix");
                NotifyPropertyChanged("Weights");
                NotifyPropertyChanged("ConsistencyIndex");
                NotifyPropertyChanged("Alternatives");
            }
        }

        #endregion

        private void UpdateResolutionStrategyName()
        {
            if (_network.ClusterComparisons[CurrentCluster].WeightsResolutionStrategy != null)
            {
                var type = _network.ClusterComparisons[CurrentCluster].WeightsResolutionStrategy.GetType();
                _resolutionStrategyName = _container.Registrations.Single(r => r.MappedToType == type).Name;
            }
            else
            {
                _resolutionStrategyName = null;
            }
            NotifyPropertyChanged("ResolutionStrategyName");
            NotifyPropertyChanged("HasAlphaData");
            NotifyPropertyChanged("AlphaData");
            NotifyPropertyChanged("AlphaDataColumnHeaders");
            NotifyPropertyChanged("AlphaDataRowHeaders");
        }

        public double[] Weights
        {
            get { return _network.ClusterComparisons[CurrentCluster].Weights; }
        }

        public double ConsistencyIndex
        {
            get { return _network.ClusterComparisons[CurrentCluster].ConsistencyIndex; }
        }

        public string[] Alternatives
        {
            get
            {
                var clusterDependents = from d in _network.ClusterDependencies
                                        where d.Master.Equals(CurrentCluster)
                                        select d.Dependent.Name;

                return clusterDependents.ToArray();
            }
        }

        public string Criteria
        {
            get { return CurrentCluster.Name; }
        }

        public ICommand ResolveCommand { get; set; }        

        public ICommand NextMatrixCommand { get; set; }

        public ICommand PreviousMatrixCommand { get; set; }

        public bool HasAlphaData
        {
            get
            {
                return _network.ClusterComparisons[CurrentCluster].WeightsResolutionStrategy is FuzzyPreferenceProgramming
                    && _network.ClusterComparisons[CurrentCluster].Weights.Sum() != 0;
            }
        }

        public double[] AlphaData
        {
            get
            {
                double[] alphaData = new double[0];
                var fpp = _network.ClusterComparisons[CurrentCluster].WeightsResolutionStrategy as FuzzyPreferenceProgramming;

                if (fpp != null)
                {
                    double[] increment;

                    for (int i = 0; i < AlphaDataRowHeaders.Length - 1; i++)
                    {
                        increment = fpp.AlphaWeights.Values.Select(v => v.Weights[i]).ToArray();
                        alphaData = alphaData.Concat(increment).ToArray();
                    }

                    increment = fpp.AlphaWeights.Values.Select(v => v.ConsistencyIndex).ToArray();
                    alphaData = alphaData.Concat(increment).ToArray();

                    return alphaData;
                }

                return null;
            }
        }

        public string[] AlphaDataColumnHeaders
        {
            get
            {
                var fpp = _network.ClusterComparisons[CurrentCluster].WeightsResolutionStrategy as FuzzyPreferenceProgramming;

                if (fpp != null)
                {
                    var alphaData = fpp.AlphaWeights.Keys.Select(e => e.ToString()).ToArray();
                    return alphaData;
                }

                return null;
            }
        }

        public string[] AlphaDataRowHeaders
        {
            get
            {
                string[] headers = Alternatives.Concat(new[] { "Індекс узгодженості" }).ToArray();

                return headers;
            }
        }
    }
}
