using GovConnect.Shared.Models;
using GovConnect.Shared.Pagination;
using Microsoft.EntityFrameworkCore;

namespace GovConnect.Shared.Extensions {
    public static class QueryableExtensions {
        public static async Task<PaginatedResult<TQuery>> PaginateAsync<TQuery>(this IQueryable<TQuery> query, int takeSize, bool isDescending, Func<TQuery, DateTime> cursor) {
            var results = await query
                .Take(takeSize)
                .ToListAsync();

            var cursorItem = isDescending
                ? results.First()
                : results.Last();

            var nextCursor = results.Count > 0 
                ? CursorEncoder.Encode(cursor(cursorItem).ToString("O"))
                : null;

            var paginationResult = new PaginatedResult<TQuery> {
                Data = results,
                NextCursor = nextCursor
            };

            paginationResult.ResultsCount = paginationResult.Data.Count();
            return paginationResult;
        }
    }
}
