using AnalyticHierarchyProcessDSS.Core.Network;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.TestUI.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnalyticHierarchyProcessDSS.TestUI.ViewModels
{
    class ClusterElementViewModel : NetworkElementViewModel
    {
        private readonly ClusterElement _clusterElement;

        public ClusterElement ClusterElement
        {
            get { return _clusterElement; }
        }

        public ClusterElementViewModel(ClusterElement clusterElement, ICommand parentCollectionRemoveCommand)
            : base(parentCollectionRemoveCommand)
        {
            _clusterElement = clusterElement;
        }

        public override string Name
        {
            get
            {
                return _clusterElement.Name;
            }

            set
            {
                if (_clusterElement.Name != value)
                {
                    _clusterElement.Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
    }
}
