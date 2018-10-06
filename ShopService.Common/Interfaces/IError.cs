using ShopService.Common.Enums;

namespace ShopService.Common.Interfaces
{
    public interface IError
    {
        ErrorType ErrorType { get; set; }
        string ErrorDescription { get; set; }
        string ErrorSource { get; set; }
    }
}