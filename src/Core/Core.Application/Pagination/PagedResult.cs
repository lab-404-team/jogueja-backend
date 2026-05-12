using Core.Domain.Primitives;

namespace Core.Application.Pagination
{
    public record PagedResult<TItem>(IReadOnlyCollection<TItem> Items, Page Page) : IPagedResult<TItem>;
}
