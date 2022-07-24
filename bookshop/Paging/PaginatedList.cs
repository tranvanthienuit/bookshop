using Microsoft.EntityFrameworkCore;

namespace bookshop.Paging;
public class PaginatedList<T> : List<T>
{
    public PaginatedList(List<T> items)
    {
        AddRange(items);
    }
    public static PaginatedList<T> CreateAsync(List<T> source, int pageIndex, int pageSize)
    {
        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items);
    }
}