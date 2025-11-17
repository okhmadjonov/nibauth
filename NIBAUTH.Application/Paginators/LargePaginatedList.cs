namespace NIBAUTH.Application.Paginators
{
    public class LargePaginatedList
    {
        public const int MaxPageSize = 1000;
    }

    public class LargePaginatedList<T> : PaginatedList
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public ICollection<T> Items { get; set; }

        //public bool HasPreviousPage => CurrentPage > 1;
        //public bool HasNextPage => CurrentPage < TotalPages;

    }
}
