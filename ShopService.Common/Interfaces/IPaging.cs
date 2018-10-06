namespace ShopService.Common.Interfaces
{
    public interface IPaging
    {
        long PageIndex { get; set; }
        long NumberOfPages { get; set; }
        long NumberOfRecords { get; set; }
    }
}