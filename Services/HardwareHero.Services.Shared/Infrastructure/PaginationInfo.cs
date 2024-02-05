using HardwareHero.Filter.RequestsModels;

namespace HardwareHero.Services.Shared.Infrastructure
{
    public class PaginationInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationInfo() { }

        public PaginationInfo(int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public static PaginationInfo ConvertFromFilterPagination(PageRequestInfo pageRequestInfo)
        {
            return new()
            {
                PageSize = pageRequestInfo.PageSize,
                PageNumber = pageRequestInfo.PageNumber
            };
        }
    }
}
