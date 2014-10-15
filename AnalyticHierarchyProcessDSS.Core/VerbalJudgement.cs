using AnalyticHierarchyProcessDSS.Core.Fuzzy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.Core
{
    public class VerbalJudgement : INotifyPropertyChanged
    {
        #region Fields

        private string _value;

        #endregion

        #region Properties

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    NotifyPropertyChanged("Value");
                }
            }
        }

        public int I { get; set; }

        public int J { get; set; }

        #endregion

        #region Constructors

        public VerbalJudgement()
            : this(_defaultJudgement.Value)
        {
        }

        public VerbalJudgement(string value)
        {
            Value = value;

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Value")
                    OnVerbalJudgementChanged(I, J);
            };
        }

        #endregion

        #region Static members

        static VerbalJudgement()
        {
            InitializeJudgementCombinations();

            Judgements = JudgementsArray.Select(j => new VerbalJudgement() { Value = j }).ToArray();
        }

        private static VerbalJudgement _defaultJudgement;

        public static VerbalJudgement[] Judgements;

        private static string[] JudgementsArray = new string[]
        {
            "Рівна ~ Слабка", 
            "Слабка перевага", 
            "Слабка ~ Сильна", 
            "Сильна перевага",
            "Сильна ~ Дуже сильна", 
            "Дуже сильна перевага", 
            "Дуже сильна ~ Абсолютна", 
            "Абсолютна перевага", 
            "Рівна важливість",
            "Абсолютне недомінування",
            "Дуже сильне ~ Абсолютне", 
            "Дуже сильне недомінування",
            "Сильне ~ Дуже сильне", 
            "Сильне недомінування", 
            "Слабке ~ Сильне", 
            "Слабке недомінування", 
            "Рівне ~ Слабке"
        };

        public static Dictionary<string, string> JudgmentCombinations = new Dictionary<string, string>();

        private static void InitializeJudgementCombinations()
        {
            int i = 0, j = JudgementsArray.Length - 1;

            while (i != j)
            {
                JudgmentCombinations[JudgementsArray[i]] = JudgementsArray[j];
                JudgmentCombinations[JudgementsArray[j]] = JudgementsArray[i];

                // Precise eveluations initialization
                PreciseEvaluations[JudgementsArray[i]] = i + 2;
                PreciseEvaluations[JudgementsArray[j]] = 1.0 / (i + 2);

                // Fuzzy eveluations initialization
                FuzzyEvaluations[JudgementsArray[i]] = new FuzzyNumber(i + 1, i + 2, i + 3);
                FuzzyEvaluations[JudgementsArray[j]] = new FuzzyNumber(FuzzyEvaluations[JudgementsArray[i]]);

                i++;
                j--;
            }

            JudgmentCombinations[JudgementsArray[i]] = JudgementsArray[i];
            _defaultJudgement = new VerbalJudgement(JudgementsArray[i]);

            PreciseEvaluations[JudgementsArray[i]] = 1;
            FuzzyEvaluations[JudgementsArray[i]] = new FuzzyNumber();

        }

        public static Dictionary<string, double> PreciseEvaluations = new Dictionary<string, double>();

        public static Dictionary<string, FuzzyNumber> FuzzyEvaluations = new Dictionary<string, FuzzyNumber>();

        #endregion

        #region Events

        public event EventHandler<VerbalJudgementChangedEventArgs> VerbalJudgementChanged;

        protected virtual void OnVerbalJudgementChanged(int i, int j)
        {
            var handler = VerbalJudgementChanged;
            if (handler != null)
            {
                handler(this, new VerbalJudgementChangedEventArgs() { I = i, J = j });
            }
        }

        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public override string ToString()
        {
            return Value;
        }
    }
}
