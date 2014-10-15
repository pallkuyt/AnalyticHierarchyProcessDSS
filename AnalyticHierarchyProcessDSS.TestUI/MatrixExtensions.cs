using AnalyticHierarchyProcessDSS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.TestUI
{
    public static class MatrixExtensions
    {
        public static Matrix<Wrapper<T>> ToWrappedMatrix<T>(this Matrix<T> matrix)
        {
            var result = new Matrix<Wrapper<T>>(matrix.Size);
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    result[i, j] = new Wrapper<T> { Value = matrix[i, j], I = i, J = j };
                }
            }

            return result;
        }
        public static Matrix<T> ToMatrix<T>(this Matrix<Wrapper<T>> wrapper)
        {
            var result = new Matrix<T>(wrapper.Size);
            for (int i = 0; i < wrapper.Size; i++)
            {
                for (int j = 0; j < wrapper.Size; j++)
                {
                    result[i, j] = wrapper[i, j].Value;
                }
            }

            return result;
        }
    }
}
