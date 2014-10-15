namespace AnalyticHierarchyProcessDSS.Entities
{
    public interface IMatrix<T>
    {
        T this[int i, int j] { get; set; }

        int Size { get; }
    }
}
