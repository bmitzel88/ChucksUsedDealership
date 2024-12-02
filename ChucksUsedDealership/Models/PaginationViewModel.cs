namespace ChucksUsedDealership.Models
{
    public class PaginationViewModel<T>
    {
        public List<T>? Items { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public bool ShowPagination { get; set; }
    }
}
