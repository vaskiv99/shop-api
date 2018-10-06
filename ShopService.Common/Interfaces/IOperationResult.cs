namespace ShopService.Common.Interfaces
{
    public interface IOperationResult
    {
        bool IsSuccess { get; }
        bool IsDataFull { get; }
        IError Error { get;  }
        IPaging Paging { get; set; }
    }
    public interface IOperationResult<out T> : IOperationResult
    {
        T Data { get; }
        T GetResult();
        void TryThrow();
    }
}