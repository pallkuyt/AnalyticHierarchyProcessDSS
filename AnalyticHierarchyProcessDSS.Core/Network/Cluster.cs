using System;
using System.Collections.Generic;
using AnalyticHierarchyProcessDSS.Core.Common;

namespace AnalyticHierarchyProcessDSS.Core.Network
{
    public class Cluster : IList<ClusterElement>
    {
        private List<ClusterElement> _elements = new List<ClusterElement>();

        public List<ClusterElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        public List<Cluster> Dependencies { get; set; }

        public string Name { get; set; }

        public Cluster(string name)
        {
            Name = name;
            Dependencies = new List<Cluster>();
        }

        public Cluster()
        {
            Dependencies = new List<Cluster>();
        }

        private Dictionary<Cluster, double> _dependencyWeights = new Dictionary<Cluster, double>();
        private Dictionary<Cluster, VerbalMatrix> _dependencyMatrices = new Dictionary<Cluster, VerbalMatrix>();

        public void CompareDependencies()
        {

        }

        public Dictionary<Cluster, double> GetDependencyWeigths()
        {
            return null;
        }

        private IWeightsResolutionStrategy _weightsResolutionStrategy;// = new MainEigenvectorResolutionStrategy();

        #region IList members

        public int IndexOf(ClusterElement item)
        {
            return _elements.IndexOf(item);
        }

        public void Insert(int index, ClusterElement item)
        {
            _elements.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _elements.RemoveAt(index);
        }

        public ClusterElement this[int index]
        {
            get
            {
                return _elements[index];
            }
            set
            {
                _elements[index] = value;
            }
        }

        public void Add(ClusterElement item)
        {
            _elements.Add(item);
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public bool Contains(ClusterElement item)
        {
            return _elements.Contains(item);
        }

        public void CopyTo(ClusterElement[] array, int arrayIndex)
        {
            _elements.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _elements.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(ClusterElement item)
        {
            return _elements.Remove(item);
        }

        public IEnumerator<ClusterElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}