using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AnalyticHierarchyProcessDSS.TestUI.Converters
{
    class CrossClusterDependencyHeaderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var master = values[0];
            var dependent = values[1];

            return string.Format("Вплив кластеру '{0}' на кластер '{1}'", master, dependent);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
