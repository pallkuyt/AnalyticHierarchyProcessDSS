using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.TestUI.Commands
{
    class NavigationCommand : RelayCommand
    {
        public NavigationCommand(Action<object> executeAction)
            : base(executeAction) { }

        public string Description { get; set; }
    }
}
