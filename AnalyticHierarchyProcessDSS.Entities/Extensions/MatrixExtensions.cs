using System;
using System.Linq;

namespace AnalyticHierarchyProcessDSS.Entities.Extensions
{
    public static class MatrixExtensions
    {
        #region TruncatedMatrix Reproach Extensions

        public static IMatrix<T> TruncatedMatrix<T>(this Matrix<T> matrix, int k)
        {
            Matrix<T> truncatedMatrix = new Matrix<T>(matrix.Size - 1);

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    truncatedMatrix[i, j] = matrix[i, j];
                }
            }

            for (int i = k + 1; i < matrix.Size; i++)
            {
                for (int j = k + 1; j < matrix.Size; j++)
                {
                    truncatedMatrix[i - 1, j - 1] = matrix[i, j];
                }
            }

            for (int i = 0; i < k; i++)
            {
                for (int j = k + 1; j < matrix.Size; j++)
                {
                    truncatedMatrix[i, j - 1] = matrix[i, j];
                }
            }

            for (int i = k + 1; i < matrix.Size; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    truncatedMatrix[i - 1, j] = matrix[i, j];
                }
            }

            return truncatedMatrix;
        } 
        #endregion

        #region X2 Reproach Extensions

        public static Matrix<double> EmpiricalMatrix(this Matrix<double> matrix)
        {
            Matrix<double> empiricalMatrix = new Matrix<double>(matrix.Size); 

            double sum = matrix.Sum();

            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    empiricalMatrix[i, j] = matrix.MatrixRow(i).Sum() * matrix.MatrixColumn(j).Sum() / sum;
                }
            }

            return empiricalMatrix;
        }

        public static Matrix<double> DeltaMatrix(this Matrix<double> matrix)
        {
            Matrix<double> deltaMatrix = new Matrix<double>(matrix.Size);

            IMatrix<double> T = matrix.EmpiricalMatrix();

            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    deltaMatrix[i, j] = Math.Pow(matrix[i, j] - T[i, j], 2) / T[i, j];
                }
            }

            return deltaMatrix;
        }

        public static double Variance(this Matrix<double> matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    sum += Math.Pow(matrix[i, j] - Mean(matrix), 2);
                }
            }

            return sum / (matrix.Count() - 1);
        }

        public static double Mean(this Matrix<double> matrix)
        {
            return matrix.Sum() / matrix.Count();
        }

        public static double[] MatrixRow(this Matrix<double> matrix, int k)
        {
            double[] row = (from i in Enumerable.Range(0, matrix.Size)
                from j in Enumerable.Range(0, matrix.Size)
                where i == k
                select matrix[k, j]).ToArray();
            
            return row;
        }

        public static double[] MatrixColumn(this Matrix<double> matrix, int k)
        {
            double[] column = (from i in Enumerable.Range(0, matrix.Size)
                            from j in Enumerable.Range(0, matrix.Size)
                            where j == k
                            select matrix[i, k]).ToArray();

            return column;
        }

        #endregion

        #region Vector Extensions

        public static double Mean(this double[] vector)
        {
            return vector.Sum() / vector.Length;
        }

        public static double Variance(this double[] vector)
        {
            double sum = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += Math.Pow(vector[i] - Mean(vector), 2);
            }

            return sum / (vector.Length - 1);
        }

        public static double Cov(this double[] x, double[] y)
        {
            double sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += (x[i] - x.Mean()) * (y[i] - y.Mean());
            }
            return sum / ((x.Length - 1) * Math.Pow(x.Variance() * y.Variance(), 0.5));
        }

        #endregion
    }
}
