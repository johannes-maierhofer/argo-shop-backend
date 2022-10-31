using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Argo.Shop.Application.Common.Persistence
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResult<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
            where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItemCount = await query.CountAsync()
            };

            var pageCount = (double)result.TotalItemCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Items = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
    }
}
