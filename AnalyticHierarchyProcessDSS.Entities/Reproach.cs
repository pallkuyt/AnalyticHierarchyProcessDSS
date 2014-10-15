using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.Entities
{
    public class Reproach
    {
        private readonly int _i;

        private readonly int _j;

        public Reproach(int i, int j)
        {
            _i = i;
            _j = j;
        }

        public int I
        {
            get { return _i; }
        }

        public int J
        {
            get { return _j; }
        }
    }
}
