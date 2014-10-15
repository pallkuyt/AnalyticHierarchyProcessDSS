using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.Entities
{
    public class Matrix<T> : IMatrix<T>, IEnumerable<T>, IEquatable<Matrix<T>>
    {
        //public event EventHandler<MatrixChangedEventArgs> MatrixChanged;
        protected T[,] _matrix;

        public Matrix()
        {
            
        }

        public Matrix(int size)
        {
            _matrix = new T[size, size];
        }

        public Matrix(Matrix<T> matrix)
        {
            _matrix = new T[matrix.Size, matrix.Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    _matrix[i, j] = matrix[i, j];
                }
            }
        }

        public Matrix(T[,] matrix)
        {
            _matrix = matrix;
        }

        public virtual T this[int i, int j]
        {
            get
            {
                return _matrix[i, j];
            }

            set
            {
                _matrix[i, j] = value;
            }
        }

        public int Size
        {
            get { return _matrix.GetLength(0); }
        }

        #region System.Object members
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append('{');

            for (int i = 0; i < Size; i++)
            {
                builder.Append('{');

                for (int j = 0; j < Size; j++)
                {
                    builder.Append(_matrix[i, j]);

                    if (j != Size - 1)
                        builder.Append(", ");
                }

                builder.Append('}');

                if (i != Size - 1)
                    builder.Append(',');
            }
            builder.Append('}');

            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Matrix<T>;
            if (other != null)
            {
                return Equals(other);
            }

            return false;
        }

        public bool Equals(Matrix<T> other)
        {
            if (Size != other.Size)
                return false;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!_matrix[i, j].Equals(other[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        } 
        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    yield return _matrix[i, j];
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //protected virtual void OnMatrixChanged(int i, int j)
        //{
        //    var handler = MatrixChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new MatrixChangedEventArgs() { I = i, J = j });

        //    }
        //}
    }

    public class MatrixChangedEventArgs : EventArgs
    {
        public int I { get; set; }

        public int J { get; set; }
    }
}
