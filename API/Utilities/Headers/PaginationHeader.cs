using API.Interfaces;

namespace API.Utilities.Headers;

public sealed class PaginationHeader
{
    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    
    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }
    
    public PaginationHeader(IPagedList list)
    {
        CurrentPage = list.CurrentPage;
        ItemsPerPage = list.PageSize;
        TotalItems = list.TotalCount;
        TotalPages = list.TotalPages;
    }
}