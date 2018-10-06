using ShopService.Common.Interfaces;

namespace ShopService.Common.Infrastructure
{
    public class Paging : IPaging
    {
        public long PageIndex { get; set; }
        public long NumberOfPages { get; set; }
        public long NumberOfRecords { get; set; }
    }
}