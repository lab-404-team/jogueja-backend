using Core.Domain.Primitives;
using MongoDB.Driver.Linq;

namespace Core.Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<(List<T> Items, bool HasNext)> ToPagedListAsync<T>(
            this IQueryable<T> queryable,
            Paging paging,
            CancellationToken cancellationToken = default)
        {
            var items = await queryable
                .Skip((paging.Number - 1) * paging.Size)
                .Take(paging.Size + 1)
                .ToListAsync(cancellationToken);

            var hasNext = items.Count > paging.Size;

            if (hasNext)
                items.RemoveAt(items.Count - 1);

            return (items, hasNext);
        }
    }
}
