using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyticHierarchyProcessDSS.Entities
{
    public struct EigenVector : IEnumerable<double>
    {
        private readonly double[] _data;

        public EigenVector(params double[] data)
        {
            _data = data;
        }

        public double this[int i]
        {
            get
            {
                return _data[i];
            }
        }

        public IEnumerator<double> GetEnumerator()
        {
            foreach (var item in _data)
            {
                yield return item / _data.Sum();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append('{');

            if (_data != null && _data.Length > 0)
            {
                for (int i = 0; i < _data.Length - 1; i++)
                {
                    builder.AppendFormat("{0}, ", _data[i]);
                }

                builder.Append(_data[_data.Length - 1]);
            }

            builder.Append('}');

            return builder.ToString();
        }
    }
}
