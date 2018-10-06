using ShopService.Common.Interfaces;

namespace ShopService.Web.Helpers
{
    public interface IMessage
    {
        object Data { get; }
        string Description { get; }
        IError Error { get; }
        bool IsSuccess { get; }
        IPaging Paging { get; }
    }
}
