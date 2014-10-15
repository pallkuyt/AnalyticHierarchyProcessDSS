using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnalyticHierarchyProcessDSS.TestUI
{
    public class RelayCommand : ICommand
    {
        private Action<object> _executeAction;

        public RelayCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }
}
