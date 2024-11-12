namespace BuildingBlocks.Pagination;

public class PaginationResult<TEntity>(int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public int Count { get; } = count;
    public int TotalPages => (int)Math.Ceiling(Count / (double)PageSize);
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    public IEnumerable<TEntity> Data { get; } = data;

}