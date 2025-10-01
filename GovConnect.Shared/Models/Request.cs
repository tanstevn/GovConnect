using GovConnect.Infrastructure.Abstractions.Mediator;

namespace GovConnect.Shared.Models {
    public class PaginatedRequest<TResponse> : IQuery<PaginatedResult<TResponse>> {
        public long? After { get; set; }
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
        public string? SearchValue { get; set; }
    }

    public enum SortDirection {
        Ascending,
        Descending
    }
}
