using FluentValidation;
using GovConnect.Data;
using GovConnect.Infrastructure.Abstractions.Mediator;
using GovConnect.Shared.Extensions;
using GovConnect.Shared.Models;
using GovConnect.Shared.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GovConnect.Application.Requests.Queries {
    public class GetPaginatedRequestsQuery : PaginatedRequest<GetPaginatedRequestsQueryRow> { }

    public class GetPaginatedRequestsQueryRow {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class GetPaginatedRequestsQueryRowValidator : AbstractValidator<GetPaginatedRequestsQuery> {
        public GetPaginatedRequestsQueryRowValidator() {
            RuleFor(prop => prop.After)
                .Must(BeValidIsoDateTime)
                .When(prop => !string.IsNullOrWhiteSpace(prop.After))
                .WithMessage(prop => $"{nameof(prop.After)} must be a valid ISO DateTime format in string.");
        }

        private bool BeValidIsoDateTime(string? after) {
            return DateTime.TryParseExact(after, "O", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }

    public class GetPaginatedRequestsQueryHandler : IRequestHandler<GetPaginatedRequestsQuery, PaginatedResult<GetPaginatedRequestsQueryRow>> {
        private readonly ApplicationDbContext _dbContext;

        public GetPaginatedRequestsQueryHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<GetPaginatedRequestsQueryRow>> HandleAsync(GetPaginatedRequestsQuery request, CancellationToken cancellationToken = default) {
            var query = _dbContext
                .Requests
                .Select(req => new GetPaginatedRequestsQueryRow {
                    Id = req.Id,
                    Title = req.Title,
                    Description = req.Description,
                    CreatedAt = req.CreatedAt
                });

            if (!string.IsNullOrWhiteSpace(request.SearchValue)) {
                    var wildcardSearch = $"%{request.SearchValue}%";

                query = query.Where(req =>
                    EF.Functions.Like(req.Title, wildcardSearch) ||
                    EF.Functions.Like(req.Description, wildcardSearch));
            }

            var isDescending = request.SortDirection == SortDirection.Descending;

            if (!string.IsNullOrWhiteSpace(request.After)) {
                var decodedAfter = CursorEncoder.Decode(request.After);
                _ = DateTime.TryParse(decodedAfter, out var afterDate);

                query = isDescending
                    ? query.Where(req => req.CreatedAt < afterDate)
                    : query.Where(req => req.CreatedAt > afterDate);
            }

            var finalQuery = isDescending
                ? query.OrderByDescending(req => req.CreatedAt)
                : query.OrderBy(req => req.CreatedAt);

            return await finalQuery.PaginateAsync(request.TakeSize, isDescending, item => item.CreatedAt);
        }
    }
}
