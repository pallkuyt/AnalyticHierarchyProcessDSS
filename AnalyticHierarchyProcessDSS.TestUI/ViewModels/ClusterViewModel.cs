using AnalyticHierarchyProcessDSS.Core.Network;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnalyticHierarchyProcessDSS.TestUI.ViewModels
{
    class ClusterViewModel : NetworkElementViewModel
    {
        private readonly Cluster _cluster;

        public Cluster Cluster
        {
            get { return _cluster; }
        }

        private readonly ICommand _addElementCommand;

        public ICommand AddElementCommand
        {
            get { return _addElementCommand; }
        }

        public ClusterViewModel(Cluster cluster, ICommand parentCollectionRemoveCommand)
            : base(parentCollectionRemoveCommand)
        {
            _cluster = cluster;

            var removeChildElementCommand = new RelayCommand((p) => Elements.Remove(p as ClusterElementViewModel));
            _addElementCommand = new RelayCommand((p) => Elements.Add(new ClusterElementViewModel(new ClusterElement(), removeChildElementCommand)));

            Elements = new ObservableCollection<ClusterElementViewModel>(Cluster.Elements.Select(e => new ClusterElementViewModel(e, removeChildElementCommand)));

            Elements.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    var newItems = e.NewItems
                        .Cast<ClusterElementViewModel>()
                        .Select(cv => cv.ClusterElement);

                    _cluster.Elements.AddRange(newItems);
                }

                if (e.OldItems != null)
                {
                    var oldItems = e.OldItems
                        .Cast<ClusterElementViewModel>()
                        .Select(cv => cv.ClusterElement);

                    foreach (var clusterElement in oldItems)
                    {
                        _cluster.Elements.Remove(clusterElement);
                    }
                }
            };
        }

        public ObservableCollection<ClusterElementViewModel> Elements { get; set; }

        public string Name
        {
            get
            {
                return _cluster.Name;
            }

            set
            {
                if (_cluster.Name != value)
                {
                    _cluster.Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
    }
}
