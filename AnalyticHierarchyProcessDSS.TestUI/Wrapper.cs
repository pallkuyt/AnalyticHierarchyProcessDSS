using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.TestUI
{
    public class Wrapper<T> : INotifyPropertyChanged, IMatrixObject
    {
        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        public static implicit operator Wrapper<T>(T value)
        {
            return new Wrapper<T> { _value = value };
        }
        public static implicit operator T(Wrapper<T> wrapper)
        {
            return wrapper._value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public int I { get; set; }
        public int J { get; set; }
    }

    interface IMatrixObject
    {
        int I { get; set; }
        int J { get; set; }
    }
}
