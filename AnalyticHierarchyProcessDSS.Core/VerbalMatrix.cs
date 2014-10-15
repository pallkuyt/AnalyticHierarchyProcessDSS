using AnalyticHierarchyProcessDSS.Core.Fuzzy;
using AnalyticHierarchyProcessDSS.Core.Network;
using AnalyticHierarchyProcessDSS.Core.Precise;
using AnalyticHierarchyProcessDSS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticHierarchyProcessDSS.Core
{
    public class VerbalMatrix : Matrix<VerbalJudgement>
    {
        public event EventHandler VerbalMatrixChanged;

        public VerbalMatrix(string[][] comparisons)
        {
            VerbalJudgement[,] matrix = new VerbalJudgement[comparisons.Length + 1, comparisons.Length + 1];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (i < j)
                    {
                        matrix[i, j] = new VerbalJudgement(comparisons[i][j - i - 1]);
                        matrix[j, i] = new VerbalJudgement(VerbalJudgement.JudgmentCombinations[comparisons[i][j - i - 1]]);
                    }
                }

                matrix[i, i] = new VerbalJudgement();
            }

            _matrix = matrix;
        }

        public VerbalMatrix(int size)
            : base(size)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    _matrix[i, j] = new VerbalJudgement() { I = i, J = j };
                    _matrix[i, j].VerbalJudgementChanged += (s, e) =>
                    {
                        _matrix[e.J, e.I].Value = VerbalJudgement.JudgmentCombinations[_matrix[e.I, e.J].Value];
                        OnVerbalMatrixChanged();
                    };
                }
            }
        }

        protected virtual void OnVerbalMatrixChanged()
        {
            var handler = VerbalMatrixChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public PairwiseComparisonMatrix ToPairwiseComparisonMatrix()
        {
            var judgementsArray = (from i in Enumerable.Range(0, Size - 1)
                                   let e = (from j in Enumerable.Range(i + 1, Size - i - 1)
                                            select VerbalJudgement.PreciseEvaluations[this[i, j].Value]).ToArray()
                                   select e).ToArray();

            return new PairwiseComparisonMatrix(judgementsArray);
        }

        public FuzzyPairwiseComparisonMatrix ToFuzzyPairwiseComparisonMatrix()
        {
            var judgementsArray = (from i in Enumerable.Range(0, Size - 1)
                                   let e = (from j in Enumerable.Range(i + 1, Size - i - 1)
                                            select VerbalJudgement.FuzzyEvaluations[this[i, j].Value]).ToArray()
                                   select e).ToArray();
            return new FuzzyPairwiseComparisonMatrix(judgementsArray);
        }
    }
}
