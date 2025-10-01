using GovConnect.Application.Utils;
using GovConnect.Data;
using GovConnect.Data.Entities;
using GovConnect.Infrastructure.Abstractions.Mediator;
using GovConnect.Shared.Models;

namespace GovConnect.Application.Requests.Queries {
    public class GetPaginatedRequestsQuery : PaginatedRequest<GetPaginatedRequestsQueryRow> { }

    public class GetPaginatedRequestsQueryRow {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class GetPaginatedRequestsQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<GetPaginatedRequestsQuery, PaginatedResult<GetPaginatedRequestsQueryRow>> {
        public Task<PaginatedResult<GetPaginatedRequestsQueryRow>> HandleAsync(GetPaginatedRequestsQuery request, CancellationToken cancellationToken = default) {
            var isDescending = request.SortDirection is SortDirection.Descending;

            if (string.IsNullOrWhiteSpace(request.SortBy)) {
                isDescending = true;
                request.SortBy = nameof(Request.CreatedAt);
            }

            //var query = dbContext
            //    .Requests
            //    .Select(request => new GetPaginatedRequestsQueryRow {
            //        Id = request.Id,
            //        Title = request.Title,
            //        Description = request.Description,
            //        CreatedAt = request.CreatedAt
            //    })
            //    .OrderBy(request => request.)

            return default;
        }
    }
}
