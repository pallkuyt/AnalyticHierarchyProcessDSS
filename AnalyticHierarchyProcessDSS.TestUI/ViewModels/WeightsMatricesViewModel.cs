using AnalyticHierarchyProcessDSS.Core.Network;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.TestUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnalyticHierarchyProcessDSS.TestUI.ViewModels
{
    class WeightsMatricesViewModel : BaseViewModel, IViewModel
    {
        private readonly NetworkStructure _network;

        public WeightsMatricesViewModel(NetworkStructure network)
        {
            _network = network;
        }

        #region IViewModel members

        public ICommand MoveToCommand { get; set; }

        public ICommand BackToCommand { get; set; }

        public string Description { get; set; }

        #endregion

        public void UpdateState()
        {
            _network.BuildElementWeightsMatrix();
            _network.BuildClusterWeightsMatrix();

            _elements = _network.Clusters.SelectMany(c => c.Elements).Select(e => e.Name).ToArray();
            _clusters = _network.Clusters.Select(c => c.Name).ToArray();
        }

        public Matrix<double> ElementWeightsMatrix
        {
            get
            {
                return _network.ElementWeightsMatrix;
            }
        }

        public Matrix<double> ClusterWeightsMatrix
        {
            get
            {
                return _network.ClusterWeightsMatrix;
            }
        }

        private string[] _elements;

        public string[] Elements
        {
            get
            {
                return _elements;
            }
        }

        private string[] _clusters;

        public string[] Clusters
        {
            get
            {
                return _clusters;
            }
        }
    }
}
