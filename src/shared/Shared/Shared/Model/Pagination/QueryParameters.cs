

namespace Shared.Library.Model.Pagination;

public class QueryParameters
{
    private int _pagesize;
    private const int MaxPageSize = 30;
    public int StartIndex => _pagesize * (PageNumber - 1);
    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get => _pagesize;
        set => _pagesize = value <= MaxPageSize ? PageSize : _pagesize;
    }
}
