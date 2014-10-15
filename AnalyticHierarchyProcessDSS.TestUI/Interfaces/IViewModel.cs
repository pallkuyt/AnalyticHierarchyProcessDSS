using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnalyticHierarchyProcessDSS.TestUI.Interfaces
{
    public interface IViewModel
    {
        ICommand MoveToCommand { get; set; }

        ICommand BackToCommand { get; set; }

        string Description { get; set; }

        void UpdateState();
    }
}
