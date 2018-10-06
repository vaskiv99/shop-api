using Newtonsoft.Json;
using ShopService.Common.Enums;
using ShopService.Common.Interfaces;
using ShopService.Common.Utils;

namespace ShopService.Common.Infrastructure
{
    public class Error : IError
    {
        [JsonConverter(typeof(ErrorTypeConverter))]
        public ErrorType ErrorType { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorSource { get; set; }
        public object ErrorData { get; set; }

        public Error()
        {
            
        }

        public Error(ErrorType errorType)
        {
            ErrorType = errorType;
            ErrorDescription = EnumUtil.GetErrorTypeDescription(errorType);
        }
        
        public Error(ErrorType errorType, object data)
        {
            ErrorType = errorType;
            ErrorData = data;
            ErrorDescription = EnumUtil.GetErrorTypeDescription(errorType);
        }
        
        public Error(ErrorType errorType, string errorSource)
        {
            ErrorType = errorType;
            ErrorDescription = EnumUtil.GetErrorTypeDescription(errorType);
            ErrorSource = errorSource;
        }

        public Error(ErrorType errorType, string errorSource, string description)
        {
            ErrorType = errorType;
            ErrorDescription = description;
            ErrorSource = errorSource;
        }

        public Error(ErrorType errorType, string errorSource, object data)
        {
            ErrorType = errorType;
            ErrorDescription = EnumUtil.GetErrorTypeDescription(errorType);
            ErrorSource = errorSource;
            ErrorData = data;
        }

        public Error(ErrorType errorType, string errorSource, string description, object data)
        {
            ErrorType = errorType;
            ErrorDescription = description;
            ErrorSource = errorSource;
            ErrorData = data;
        }

    }
}