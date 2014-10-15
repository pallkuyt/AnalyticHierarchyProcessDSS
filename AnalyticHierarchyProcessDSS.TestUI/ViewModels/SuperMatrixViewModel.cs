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
    class SuperMatrixViewModel : BaseViewModel, IViewModel
    {
        private readonly NetworkStructure _network;

        public SuperMatrixViewModel(NetworkStructure network)
        {
            _network = network;

            PowerWWECommand = new RelayCommand((parameter) =>
            {
                _network.MultiplyWWE();
                NotifyPropertyChanged("PoweredWWE");
                NotifyPropertyChanged("GlobalWeights");
            });
        }

        #region IViewModel members

        public ICommand MoveToCommand { get; set; }

        public ICommand BackToCommand { get; set; }

        public string Description { get; set; }

        #endregion

        public void UpdateState()
        {
            _network.GetSuperMatrix();
            _elements = _network.Clusters.SelectMany(c => c.Elements).Select(e => e.Name).ToArray();
            NotifyPropertyChanged("PoweredWWE");
        }

        public Matrix<double> SuperMatrix
        {
            get
            {
                return _network.SuperMatrix;
            }
        }

        public Matrix<double> PoweredWWE
        {
            get
            {
                return _network.PoweredWWE;
            }
        }

        public ICommand PowerWWECommand { get; set; }

        private string[] _elements;

        public string[] Elements
        {
            get
            {
                return _elements;
            }
        }

        public double[] GlobalWeights
        {
            get
            {
                List<double> weights = new List<double>();

                var count = _network.Clusters.Last().Elements.Count;

                for (int i = count; i > 0; i--)
                    weights.Add(_network.PoweredWWE[_elements.Length - i, 0]);

                var sum = weights.Sum();

                return weights.Select(w => w / sum).ToArray();
            }
        }

        public string[] Alternatives
        {
            get
            {
                return _network.Clusters.Last().Elements.Select(e => e.Name).ToArray();
            }
        }
    }
}
