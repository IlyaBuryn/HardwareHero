using HardwareHero.Shared.Responses;

namespace HardwareHero.Shared.Repositories.Answers
{
    public class PagedDataAnswer<T> : Answer<T> where T : class
    {
        public IEnumerable<T?>? Values { get; set; } = Array.Empty<T>();
        public int Page { get; set; }
        public int Limit { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public PagedDataAnswer(
            IEnumerable<T?>? values,
            int currentPage,
            int limit,
            int totalItems) : base()
        {
            Values = values;
            Page = currentPage;
            Limit = limit;
            TotalItems = totalItems;
            TotalPages = TotalPages = (int)Math.Ceiling((double)totalItems / limit);
        }

        public PagedDataAnswer(Exception exception) : base(exception) { }

        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;

        public virtual PageResponse<T> ToPageResponse()
        {
            return new PageResponse<T>()
            {
                Items = Values,
                CurrentPageNumber = (uint)Page,
                CurrentPageSize = (uint)Limit,
                TotalPages = (uint)TotalPages,
                Count = (int)TotalItems,
            };
        }
    }
}
