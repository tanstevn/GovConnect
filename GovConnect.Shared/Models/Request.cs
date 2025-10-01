using GovConnect.Infrastructure.Abstractions.Mediator;

namespace GovConnect.Shared.Models {
    public class PaginatedRequest<TResponse> : IQuery<PaginatedResult<TResponse>> {
        public string? After { get; set; }
        public int TakeSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
        public string? SearchValue { get; set; }
    }

    public enum SortDirection {
        Ascending,
        Descending
    }
}
