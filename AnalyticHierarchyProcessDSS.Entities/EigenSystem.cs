using System.Collections.Generic;
using System.Linq;

namespace AnalyticHierarchyProcessDSS.Entities
{
    public class EigenSystem
    {
        private readonly IEnumerable<EigenVector> _vectors;
        private readonly double[] _values;

        public EigenSystem(double[] values, IEnumerable<EigenVector> vectors)
        {
            _values = values;
            _vectors = vectors;
        }

        public EigenVector[] Vectors
        {
            get { return _vectors.ToArray(); }
        }

        public double[] Values
        {
            get { return _values; }
        }
    }
}
