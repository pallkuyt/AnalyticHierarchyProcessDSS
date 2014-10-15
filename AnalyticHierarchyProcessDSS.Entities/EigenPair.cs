using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.Entities
{
    public class EigenPair
    {
        private readonly double _eigenValue;

        private readonly EigenVector _eigenVector;

        public EigenPair(double eigenValue, EigenVector eigenVector)
        {
            _eigenValue = eigenValue;
            _eigenVector = eigenVector;
        }

        public double EigenValue
        {
            get { return _eigenValue; }
        }

        public EigenVector EigenVector
        {
            get { return _eigenVector; }
        }
    }
}
