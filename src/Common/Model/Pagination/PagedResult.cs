

namespace Shared.Library.Model.Pagination;

public class PagedResult<T>
{
    
    public int CurrentPage { get; set; }
    public int TotalCount { get; set; }
    public int RecordNumber { get; set; }
    public List<T> Items { get; set; }

    public int TotalPages => (int)(Math.Ceiling((double)TotalCount / (double)RecordNumber));
}
