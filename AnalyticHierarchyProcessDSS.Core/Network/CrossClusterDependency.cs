using System.Collections.Generic;

namespace AnalyticHierarchyProcessDSS.Core.Network
{
    public class CrossClusterDependency
    {
        public Cluster Master { get; set; }

        public Cluster Dependent { get; set; }

        public Dictionary<ClusterElement, PairwiseComparisonTask> ComparisonsMatrices { get; set; }

        public CrossClusterDependency(Cluster master, Cluster dependent)
        {
            Master = master;
            Dependent = dependent;

            Initialize();
        }

        public void Initialize()
        {
            ComparisonsMatrices = new Dictionary<ClusterElement, PairwiseComparisonTask>();

            foreach (var element in Master.Elements)
            {
                ComparisonsMatrices.Add(element, new PairwiseComparisonTask(Dependent.Elements.Count));
            }
        }

        public override string ToString()
        {
            return string.Format("Master:{0}; Dependent:{1}", Master.Name, Dependent.Name);
        }
    }
}