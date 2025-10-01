using System.Collections;
using System.Linq.Expressions;

namespace GovConnect.Application.Utils {
    public static class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TQuery">The type of the initial query passed.</typeparam>
        /// <param name="query">The context of the intial query passed.</param>
        /// <param name="filterValue">The value that is searched.</param>
        /// <param name="predicate">The WHERE expression.</param>
        /// <returns></returns>
        public static IQueryable<TQuery> IfNotEmptyThen<TQuery>(this IQueryable<TQuery> query, object? filterValue, Expression<Func<TQuery, bool>> predicate) {
            return filterValue switch {
                null => query,
                string stringFilterValue when string.IsNullOrWhiteSpace(stringFilterValue) => query,
                DateTime dateFilterValue when dateFilterValue == DateTime.MinValue => query,
                ICollection { Count: 0 } => query,
                Array { Length: 0 } => query,
                _ => query.Where(predicate)
            };
        }
    }
}
