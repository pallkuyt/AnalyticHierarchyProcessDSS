using AnalyticHierarchyProcessDSS.TestUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnalyticHierarchyProcessDSS.TestUI.ViewModels
{
    abstract class NetworkElementViewModel : BaseViewModel, IParentCollectionRemovable
    {
        private readonly ICommand _parentCollectionRemoveCommand;

        public NetworkElementViewModel(ICommand parentCollectionRemoveCommand)
        {
            _parentCollectionRemoveCommand = parentCollectionRemoveCommand;
        }

        public virtual string Name { get; set; }

        public ICommand RemoveCommand
        {
            get { return _parentCollectionRemoveCommand; }
        }
    }
}
