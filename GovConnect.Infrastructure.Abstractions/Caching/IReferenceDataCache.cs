namespace GovConnect.Infrastructure.Abstractions.Caching {
    public interface IReferenceDataCache {
        IReadOnlySet<long> SubcategoryIds { get; }
        IReadOnlySet<long> BarangayIds { get; }
        IReadOnlySet<long> PriorityLevelIds { get; }
        Task LoadAsync();
    }
}
