using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Precise;

namespace AnalyticHierarchyProcessDSS.Core
{
    public static class Extensions
    {
        public static string ToMathematicaString(this double[] array)
        {
            string result = String.Empty;

            result += "{";

            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (i == 0)
                {
                    result += array[i].ToString("0.###");
                }
                else
                {
                    result += ", " + array[i].ToString("0.###");
                }
            }

            result += "}";

            return result;
        }

        public static string ToMathematicaString(this double[,] array)
        {
            string result = String.Empty;

            result += "{";
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        result += "{" + array[i, j].ToString().Replace(",", ".");
                    }
                    else
                    {
                        result += "," + array[i, j].ToString().Replace(",", ".");
                    }
                }
                result += "},";
            }

            result = result.Remove(result.Length - 1, 1);
            result += "}";


            return result;
        }
    }
}
