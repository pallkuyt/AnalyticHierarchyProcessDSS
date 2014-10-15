using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Core.Precise;
using AnalyticHierarchyProcessDSS.Entities;
using System.Collections.ObjectModel;
using System.Collections;

namespace AnalyticHierarchyProcessDSS.Core.Network
{
    public class NetworkStructure
    {
        public List<Cluster> Clusters { get; set; }

        public Matrix<double> ClusterWeightsMatrix { get; set; }

        public Matrix<double> ElementWeightsMatrix { get; set; }

        public NetworkStructure()
        {
            Cluster criterias = new Cluster("Критерії")
            {
                new ClusterElement() {Name = "Вартість"},
                new ClusterElement() {Name = "Витрати"},
                new ClusterElement() {Name = "Довговічність"}
            };

            Cluster alternatives = new Cluster("Альтернативи")
            {
                new ClusterElement() {Name = "Американський"},
                new ClusterElement() {Name = "Європейський"},
                new ClusterElement() {Name = "Японський"}
            };

            Clusters = new List<Cluster>() { criterias, alternatives };
            ClusterDependencyMatrix = new Matrix<bool>(new[,] { { false, true }, { true, false } });  //3

            UpdateNetwork();
        }

        public void UpdateNetwork()
        {
            PrepereNetworkDependencies();

            CompareClusters();
        }

        private void PrepereNetworkDependencies()
        {
            ClusterDependencies = new List<CrossClusterDependency>();

            var dependencyIndices = (from i in Enumerable.Range(0, ClusterDependencyMatrix.Size)
                                     from j in Enumerable.Range(0, ClusterDependencyMatrix.Size)
                                     where ClusterDependencyMatrix[i, j]
                                     select new
                                     {
                                         I = i,
                                         J = j
                                     }).ToArray();

            foreach (var dependencyIndex in dependencyIndices)
            {
                ClusterDependencies.Add(new CrossClusterDependency(Clusters[dependencyIndex.I], Clusters[dependencyIndex.J]));
            }
        }

        private void CompareClusters()
        {
            ClusterComparisons = new Dictionary<Cluster, PairwiseComparisonTask>();

            foreach (var cluster in Clusters)
            {
                var clusterDependents = (from d in ClusterDependencies
                                         where d.Master.Equals(cluster)
                                         select d.Dependent).ToArray();

                ClusterComparisons.Add(cluster, new PairwiseComparisonTask(clusterDependents.Length));
            } 
        }

        public void BuildClusterWeightsMatrix()
        {
            // Initializing WC matrix
            ClusterWeightsMatrix = new Matrix<double>(Clusters.Count);

            if (ClusterComparisons != null)
            {
                // For each cluster that has dependent clusters, we are finding dependent clusters weights
                foreach (var cluster in ClusterComparisons.Keys)
                {
                    // Index of cluster
                    var clusterIndex = Clusters.IndexOf(cluster);

                    // Dependent clusters weights
                    //double[] weights = _weightsResolutionStrategy.GetWeights(ClusterComparisons[cluster].ToPairwiseComparisonMatrix());
                    var task = ClusterComparisons[cluster];
                    double[] weights;
                    if (task.Weights.Sum() == 0)
                    {
                        weights = ClusterComparisons[cluster].Resolve();
                    }
                    else
                    {
                        weights = task.Weights;
                    }

                    // Dependent clusters
                    var clusterDependents = (from d in ClusterDependencies
                                             where d.Master.Equals(cluster)
                                             select d.Dependent).ToArray();

                    // Combination of dependent cluster and it's weight
                    var clusterWeights = from i in Enumerable.Range(0, clusterDependents.Length)
                                         select new { Cluster = clusterDependents[i], Weight = weights[i] };

                    // For each combination of dependent cluster and it's weight
                    foreach (var clusterWeight in clusterWeights)
                    {
                        // We are finding those dependent cluster index in Clusters collection
                        var dependentClusterIndex = Clusters.IndexOf(clusterWeight.Cluster);

                        // Setting appropriate WC element with value of dependent cluster weight
                        ClusterWeightsMatrix[dependentClusterIndex, clusterIndex] = clusterWeight.Weight;
                    }
                }
            }
        }

        //private void BuildElementWeightsMatrix()
        //{
        //    // Initializing WE matrix
        //    ElementWeightsMatrix = new Matrix<double>(Clusters.SelectMany(c => c.Elements).Count());
            
        //    // All network (clusters) element collection 
        //    var clustersElements = Clusters.SelectMany(c => c.Elements).ToArray();

        //    // For each cluster
        //    foreach (var cluster in Clusters)
        //    {
        //        // Find dependent on it
        //        var clusterDependents = (from d in ClusterDependencies
        //                                 where d.Master.Equals(cluster)
        //                                 select d.Dependent).ToArray();

        //        // For each cluster element
        //        foreach (var clusterElement in cluster.Elements)
        //        {
        //            // We are building WE matrix column
        //            double[] clusterElementImpact = new double[0];

        //            // We do this through comparing our cluster impact on all network clusters
        //            foreach (var comparedCluster in Clusters)
        //            {
        //                double[] impactIncrement;
        //                // If this cluster isn't dependent on our cluster - impact is zero based vector
        //                if (!clusterDependents.Contains(comparedCluster))
        //                {
        //                    impactIncrement = new double[comparedCluster.Elements.Count];
        //                }
        //                else
        //                {
        //                    // If this cluster is dependent on our cluster - impact is weights from Pairwise Comparison Matrix based on compare the relative comparedCluster elements to clusterElement
        //                    var matrix = ClusterDependencies.Single(cd => cd.Master.Equals(cluster) && cd.Dependent.Equals(comparedCluster)).ComparisonsMatrices[clusterElement];
        //                    impactIncrement = _weightsResolutionStrategy.GetWeights(matrix.ToPairwiseComparisonMatrix());
        //                }

        //                // Adding this increment
        //                clusterElementImpact = clusterElementImpact.Concat(impactIncrement).ToArray();
        //            }

        //            // Setting WE matrix column for clusterElement
        //            int clusterElementIndex = Array.IndexOf(clustersElements, clusterElement);

        //            if (clusterElementImpact.Length == ElementWeightsMatrix.Size)
        //            {
        //                for (int i = 0; i < clusterElementImpact.Length; i++)
        //                {
        //                    ElementWeightsMatrix[i, clusterElementIndex] = clusterElementImpact[i];
        //                } 
        //            }
        //        }
        //    }
        //}

        public void BuildElementWeightsMatrix()
        {
            // Initializing WE matrix
            ElementWeightsMatrix = new Matrix<double>(Clusters.SelectMany(c => c.Elements).Count());

            // All network (clusters) element collection 
            var clustersElements = Clusters.SelectMany(c => c.Elements).ToArray();

            // For each cluster
            foreach (var cluster in Clusters)
            {
                // Find dependent on it
                var clusterDependents = (from d in ClusterDependencies
                                         where d.Master.Equals(cluster)
                                         select d.Dependent).ToArray();

                // For each cluster element
                foreach (var clusterElement in cluster.Elements)
                {
                    // We are building WE matrix column
                    double[] clusterElementImpact = new double[0];

                    // We do this through comparing our cluster impact on all network clusters
                    foreach (var comparedCluster in Clusters)
                    {
                        double[] impactIncrement;
                        // If this cluster isn't dependent on our cluster - impact is zero based vector
                        if (!clusterDependents.Contains(comparedCluster))
                        {
                            impactIncrement = new double[comparedCluster.Elements.Count];
                        }
                        else
                        {
                            // If this cluster is dependent on our cluster - impact is weights from Pairwise Comparison Matrix based on compare the relative comparedCluster elements to clusterElement
                            var task = ClusterDependencies.Single(cd => cd.Master.Equals(cluster) && cd.Dependent.Equals(comparedCluster)).ComparisonsMatrices[clusterElement];
                            if (task.Weights.Sum() == 0)
                            {
                                impactIncrement = task.Resolve();
                            }
                            else
                            {
                                impactIncrement = task.Weights;
                            }
                        }

                        // Adding this increment
                        clusterElementImpact = clusterElementImpact.Concat(impactIncrement).ToArray();
                    }

                    // Setting WE matrix column for clusterElement
                    int clusterElementIndex = Array.IndexOf(clustersElements, clusterElement);

                    if (clusterElementImpact.Length == ElementWeightsMatrix.Size)
                    {
                        for (int i = 0; i < clusterElementImpact.Length; i++)
                        {
                            ElementWeightsMatrix[i, clusterElementIndex] = clusterElementImpact[i];
                        }
                    }
                }
            }
        }

        private List<Matrix<double>> _wweArray;

        public void MultiplyWWE()
        {
            _wweArray = new List<Matrix<double>>() { SuperMatrix };

            for (int i = 0; i < 1010; i++)
            {
                _wweArray.Add(Multiplication(_wweArray.Last(), SuperMatrix)); 
            }

            _poweredWWE = _wweArray.Skip(10).Aggregate((a, b) => Addition(a, b));

            _poweredWWE = ConstantMultiplication(_poweredWWE, 0.001);
        }

        private Matrix<double> _poweredWWE;

        public Matrix<double> PoweredWWE
        {
            get
            {
                return _poweredWWE;
            }
        }

        private Matrix<double> Multiplication(Matrix<double> a, Matrix<double> b)
        {
            var matrix = new Matrix<double>(a.Size);

            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < matrix.Size; k++)
                    {
                        sum += a[i, k] * b[k, j];
                    }

                    matrix[i, j] = sum;
                }
            }

            return matrix;
        }

        private Matrix<double> ConstantMultiplication(Matrix<double> a, double c)
        {
            var matrix = new Matrix<double>(a.Size);

            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    matrix[i, j] = a[i, j] * c;
                }
            }

            return matrix;
        }

        private Matrix<double> Addition(Matrix<double> a, Matrix<double> b)
        {
            var matrix = new Matrix<double>(a.Size);

            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    matrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return matrix;
        }

        public Matrix<double> GetSuperMatrix()
        {
            var matrix = new Matrix<double>(ElementWeightsMatrix);

            var superMatrix = new Matrix<double>(ElementWeightsMatrix.Size);

            int[] sizes = new int[] { 0 };
            sizes = sizes.Concat(Clusters.Select(c => c.Elements.Count)).ToArray();

            for (int i = 1; i < sizes.Length; i++)
            {
                sizes[i] = sizes[i - 1] + sizes[i];
            }

            var intervals = (from i in Enumerable.Range(0, sizes.Length - 1)
                             select new
                             {
                                 Lower = sizes[i],
                                 Upper = sizes[i + 1]
                             }).ToArray();

            var limitedMultipliers = (from i in Enumerable.Range(0, intervals.Length)
                                      from j in Enumerable.Range(0, intervals.Length)
                                      select new
                                      {
                                          RowIndices = intervals[i],
                                          ColumnIndices = intervals[j],
                                          Multiplier = ClusterWeightsMatrix[i, j]
                                      }).ToArray();

            for (int k = 0; k < limitedMultipliers.Length; k++)
            {
                for (int i = limitedMultipliers[k].RowIndices.Lower; i < limitedMultipliers[k].RowIndices.Upper; i++)
                {
                    for (int j = limitedMultipliers[k].ColumnIndices.Lower; j < limitedMultipliers[k].ColumnIndices.Upper; j++)
                    {
                        superMatrix[i, j] = matrix[i, j] * limitedMultipliers[k].Multiplier;
                    }
                }
            }

            SuperMatrix = superMatrix;
            _poweredWWE = SuperMatrix;

            return superMatrix;
        }

        private Matrix<bool> _clusterDependencyMatrix;
        public Matrix<bool> ClusterDependencyMatrix
        {
            get { return _clusterDependencyMatrix; }
            set
            {
                _clusterDependencyMatrix = value;
                UpdateNetwork();
            }
        }

        public List<CrossClusterDependency> ClusterDependencies { get; set; }

        public Dictionary<Cluster, PairwiseComparisonTask> ClusterComparisons { get; set; }

        public Matrix<double> SuperMatrix { get; set; }
    }
}
