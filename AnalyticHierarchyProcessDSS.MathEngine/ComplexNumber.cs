using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.WolframEngine
{
    public struct ComplexNumber
    {
        private readonly double _real;

        private readonly double _imaginary;

        public ComplexNumber(double real, double imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

        public ComplexNumber(string complexNumber) :this()
        {
            var parts = complexNumber.Split(new[] {'-', '+'}, StringSplitOptions.RemoveEmptyEntries)
                .Where(p => !string.IsNullOrWhiteSpace(p));

            string real = parts.FirstOrDefault(i => !i.Contains('I'));
            string imaginary = parts.FirstOrDefault(i => i.Contains('I'));

            if (real != null)
                _real = double.Parse(real, CultureInfo.InvariantCulture);

            if (imaginary != null)
                _imaginary = double.Parse(imaginary.Replace("*I", ""), CultureInfo.InvariantCulture);

            var minusCount = complexNumber.Count(c => c == '-');

            switch (minusCount)
            {
                case 1:
                {
                    if (Imaginary == 0)
                        _real = -1*Real;

                    if (Real == 0)
                        _imaginary = -1*Imaginary;

                    break;
                }

                case 2:
                {
                    _imaginary = -1*Imaginary;
                    _real = -1*Real;
                    break;
                }
            }
        }

        public double Real
        {
            get { return _real; }
        }

        public double Imaginary
        {
            get { return _imaginary; }
        }
    }
}
