using System;
using System.Net;
using ShopService.Common.Enums;
using ShopService.Common.Interfaces;
using ShopService.Common.Utils;

namespace ShopService.Common.Exceptions
{
    public sealed class DomainException : Exception
    {
        public ErrorType ErrorType { get; }
        public string ErrorDescription { get; }
        public object ErrorData { get; }
        public HttpStatusCode? StatusCode { get; set; }

        public DomainException(ErrorType errorType, string description, object data = null) : base(description)
        {
            ErrorType = errorType;
            ErrorDescription = description;
            ErrorData = data;
        }

        public DomainException(ErrorType errorType, string description, HttpStatusCode code) : base(description)
        {
            ErrorType = errorType;
            ErrorDescription = description;
            StatusCode = code;
        }
        
        public DomainException(ErrorType errorType, HttpStatusCode code) : base(EnumUtil.GetErrorTypeDescription(errorType))
        {
            ErrorType = errorType;
            ErrorDescription = Message;
            StatusCode = code;
        }
        
        public DomainException(ErrorType errorType) : base(EnumUtil.GetErrorTypeDescription(errorType))
        {
            ErrorType = errorType;
            ErrorDescription = Message;
        }
        
        public DomainException(ErrorType errorType, object data) : base(EnumUtil.GetErrorTypeDescription(errorType))
        {
            ErrorType = errorType;
            ErrorDescription = Message;
            ErrorData = data;
        }
        
        public DomainException(IError error)
        {
            ErrorType = error.ErrorType;
            ErrorDescription = error.ErrorDescription;
            ErrorData = error.ErrorSource;
        }     
                
        public DomainException(IError error, HttpStatusCode code)
        {
            ErrorType = error.ErrorType;
            ErrorDescription = error.ErrorDescription;
            ErrorData = error.ErrorSource;
            StatusCode = code;
        }
    }
}