using AnalyticHierarchyProcessDSS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnalyticHierarchyProcessDSS.TestUI
{
    public class VerbalMatrixModifiabilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var judgement = value as VerbalJudgement;
            if (judgement != null && judgement.J > judgement.I)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
