using AnalyticHierarchyProcessDSS.Core;
using AnalyticHierarchyProcessDSS.Core.Common;
using AnalyticHierarchyProcessDSS.Core.Fuzzy;
using AnalyticHierarchyProcessDSS.Core.Network;
using AnalyticHierarchyProcessDSS.Entities;
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
    class CrossClusterDependencyViewModel : BaseViewModel, IPairwiseComparisonMatrixInitializationViewModel
    {
        private CrossClusterDependency _crossClusterDependency;

        private string[] _dependentElements;

        private LinkedList<ClusterElement> _masterElements;

        private IUnityContainer _container;

        public CrossClusterDependencyViewModel(CrossClusterDependency crossClusterDependency, IUnityContainer container)
        {
            _crossClusterDependency = crossClusterDependency;
            _container = container;

            _dependentElements = _crossClusterDependency.Dependent.Elements.Select(e => e.Name).ToArray();

            _masterElements = new LinkedList<ClusterElement>(_crossClusterDependency.Master.Elements);

            CurrentMasterElementNode = _masterElements.First;

            NextMatrixCommand = new RelayCommand((parameter) =>
            {
                if (_currentMasterElementNode.Next != null)
                {
                    CurrentMasterElementNode = _currentMasterElementNode.Next;
                    UpdateResolutionStrategyName();
                }
            });

            PreviousMatrixCommand = new RelayCommand((parameter) =>
            {
                if (_currentMasterElementNode.Previous != null)
                {
                    CurrentMasterElementNode = _currentMasterElementNode.Previous;
                    UpdateResolutionStrategyName();
                }
            });

            ResolveCommand = new RelayCommand((parameter) =>
            {
                _crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].Resolve();
                NotifyPropertyChanged("Weights");
                NotifyPropertyChanged("ConsistencyIndex");
                NotifyPropertyChanged("HasAlphaData");
                NotifyPropertyChanged("AlphaData");
                NotifyPropertyChanged("AlphaDataColumnHeaders");
                NotifyPropertyChanged("AlphaDataRowHeaders");
            });
        }

        public string MasterName { get { return _crossClusterDependency.Master.Name; } }

        public string DependentName { get { return _crossClusterDependency.Dependent.Name; } }

        public string[] DependentElements { get { return _dependentElements; } }

        public ClusterElement CurrentMasterElement
        {
            get { return _currentMasterElementNode.Value; }
        }

        private LinkedListNode<ClusterElement> _currentMasterElementNode;

        public LinkedListNode<ClusterElement> CurrentMasterElementNode
        {
            get { return _currentMasterElementNode; }
            private set
            {
                _currentMasterElementNode = value;
                NotifyPropertyChanged("CurrentMasterElement");
                NotifyPropertyChanged("Criteria");
                NotifyPropertyChanged("PairwiseComparisonMatrix");
                NotifyPropertyChanged("Weights");
                NotifyPropertyChanged("ConsistencyIndex");
            }
        }

        public VerbalMatrix PairwiseComparisonMatrix
        {
            get { return _crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].Matrix; }            
        }

        public double[] Weights
        {
            get { return _crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].Weights; }
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
                    _crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].WeightsResolutionStrategy = _container.Resolve<IWeightsResolutionStrategy>(_resolutionStrategyName);
                }
            }
        }

        private void UpdateResolutionStrategyName()
        {
            if (_crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].WeightsResolutionStrategy != null)
            {
                var type = _crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].WeightsResolutionStrategy.GetType();
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

        public bool HasAlphaData
        {
            get
            {
                return _crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].WeightsResolutionStrategy is FuzzyPreferenceProgramming
                    && _crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].Weights.Sum() != 0;
            }
        }

        public double[] AlphaData
        {
            get
            {
                double[] alphaData = new double[0];
                var fpp = _crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].WeightsResolutionStrategy as FuzzyPreferenceProgramming;

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
                var fpp = _crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].WeightsResolutionStrategy as FuzzyPreferenceProgramming;

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

        public double ConsistencyIndex
        {
            get { return _crossClusterDependency.ComparisonsMatrices[CurrentMasterElement].ConsistencyIndex; }
        }

        public ICommand ResolveCommand { get; set; }

        public ICommand NextMatrixCommand { get; set; }

        public ICommand PreviousMatrixCommand { get; set; }

        public string[] Alternatives
        {
            get { return _dependentElements; }
        }

        public string Criteria
        {
            get { return CurrentMasterElement.Name; }
        }
    }
}
