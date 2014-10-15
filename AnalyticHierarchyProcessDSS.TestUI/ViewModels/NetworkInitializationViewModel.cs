using AnalyticHierarchyProcessDSS.Core.Network;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.TestUI.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnalyticHierarchyProcessDSS.TestUI.ViewModels
{
    class NetworkInitializationViewModel : BaseViewModel, IViewModel
    {
        private readonly NetworkStructure _network;

        private readonly ICommand _addClusterCommand;

        public ICommand AddClusterCommand
        {
            get { return _addClusterCommand; }
        }

        public ICommand ClusterDependecyChangedCommand
        {
            get
            {
                return _clusterDependecyChangedCommand;
            }
        }

        private readonly ICommand _clusterDependecyChangedCommand;

        private Matrix<Wrapper<bool>> _clusterDependencyMatrix;

        public Matrix<Wrapper<bool>> ClusterDependencyMatrix
        {
            get
            {
                return _clusterDependencyMatrix;
            }
            set
            {
                if (_clusterDependencyMatrix != value)
                {
                    _clusterDependencyMatrix = value;
                    NotifyPropertyChanged("ClusterDependencyMatrix");
                }
            }
        }

        private void InitializeClusterDependencyMatrix()
        {
            _network.ClusterDependencyMatrix = new Matrix<bool>(Clusters.Count);
            ClusterDependencyMatrix = _network.ClusterDependencyMatrix.ToWrappedMatrix();
        }

        public NetworkInitializationViewModel(NetworkStructure network, IUnityContainer container)
        {
            _network = network;

            var removeChildElementCommand = new RelayCommand((p) =>
            {
                Clusters.Remove(p as ClusterViewModel);
                InitializeClusterDependencyMatrix();
            });

            _addClusterCommand = new RelayCommand((p) =>
            {
                Clusters.Add(new ClusterViewModel(new Cluster(), removeChildElementCommand));
                InitializeClusterDependencyMatrix();
            });

            _clusterDependecyChangedCommand = new RelayCommand((p) =>
            {
                var dependency = p as Wrapper<bool>;
                if (dependency != null)
                {
                    _network.ClusterDependencyMatrix[dependency.I, dependency.J] = dependency.Value;
                    _network.UpdateNetwork();
                }
            });

            Clusters = new ObservableCollection<ClusterViewModel>(_network.Clusters.Select(c => new ClusterViewModel(c, removeChildElementCommand)));
            ClusterDependencyMatrix = _network.ClusterDependencyMatrix.ToWrappedMatrix();

            Clusters.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    var newItems = e.NewItems
                        .Cast<ClusterViewModel>()
                        .Select(cv => cv.Cluster);

                    _network.Clusters.AddRange(newItems);
                }

                if (e.OldItems != null)
                {
                    var oldItems = e.OldItems
                        .Cast<ClusterViewModel>()
                        .Select(cv => cv.Cluster);

                    foreach (var cluster in oldItems)
                    {
                        _network.Clusters.Remove(cluster);
                    }
                }
            };
        }

        public ObservableCollection<ClusterViewModel> Clusters { get; set; }

        public ICommand MoveToCommand { get; set; }

        public ICommand BackToCommand { get; set; }

        public string Description { get; set; }


        public void UpdateState()
        {

        }
    }
}
