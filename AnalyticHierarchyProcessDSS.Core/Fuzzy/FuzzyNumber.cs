using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.Core.Fuzzy
{
    public class FuzzyNumber
    {
        private readonly double _lower;
        private readonly double _middle;
        private readonly double _upper;

        public FuzzyNumber()
        {
            _lower = 1;
            _middle = 1;
            _upper = 1;
        }

        public FuzzyNumber(double lower, double middle, double upper)
        {
            _lower = lower;
            _middle = middle;
            _upper = upper;
        }

        public FuzzyNumber(FuzzyNumber number)
        {
            _lower = 1/number.Upper;
            _middle = 1/number.Middle;
            _upper = 1/number.Lower;
        }

        public double Lower
        {
            get { return _lower; }
        }

        public double Middle
        {
            get { return _middle; }
        }

        public double Upper
        {
            get { return _upper; }
        }

        public override string ToString()
        {
            return string.Format("({0}; {1}; {2})", _lower, _middle, _upper);
        }
    }
}
