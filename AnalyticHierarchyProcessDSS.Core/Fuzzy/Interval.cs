using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.Core.Fuzzy
{
    public class Interval
    {
        private readonly double _upperLimit;

        private readonly double _lowerLimit;

        public Interval(double lowerLimit, double upperLimit)
        {
            _lowerLimit = lowerLimit;
            _upperLimit = upperLimit;
        }

        public double UpperLimit
        {
            get { return _upperLimit; }
        }

        public double LowerLimit
        {
            get { return _lowerLimit; }
        }

        public override string ToString()
        {
            return string.Format("({0}; {1})", _lowerLimit, _upperLimit);
        }
    }
}
