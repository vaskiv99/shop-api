using System;
using ShopService.Common.Enums;
using ShopService.Common.Exceptions;
using ShopService.Common.Interfaces;

namespace ShopService.Common.Infrastructure
{
    public class OperationResult<T> : IOperationResult<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsDataFull { get; set; }
        public IError Error { get; set; }
        public IPaging Paging { get; set; } = new Paging {NumberOfPages = 1};

        public OperationResult(ErrorType type, string errorSource)
        {
            Error = new Error(type, errorSource);
            Data = default(T);
            IsSuccess = false;
            IsDataFull = true;
        }

        public OperationResult(ErrorType type)
        {
            Error = new Error(type);
            Data = default(T);
            IsSuccess = false;
            IsDataFull = true;
        }

        public OperationResult(ErrorType type, object data)
        {
            Error = new Error(type, data);
            Data = default(T);
            IsSuccess = false;
            IsDataFull = true;
        }

        public OperationResult(ErrorType type, Exception exception)
        {
            Error = new Error(type, exception.Source, exception.Message);
            Data = default(T);
            IsSuccess = false;
            IsDataFull = true;
        }

        public OperationResult(ErrorType type, string errorSource, string description, object data)
        {
            Error = new Error(type, errorSource, description, data);
            Data = default(T);
            IsSuccess = false;
            IsDataFull = true;
        }

        public OperationResult(IError description)
        {
            Error = description;
            Data = default(T);
            IsSuccess = false;
            IsDataFull = true;
        }

        public OperationResult(T data = default(T), bool isDataFull = true, int numberOfPages = 1,
            long numberOfRecords = 0)
        {
            Data = data;
            IsSuccess = true;
            IsDataFull = isDataFull;
            Paging.NumberOfPages = numberOfPages;
            Paging.NumberOfRecords = numberOfRecords;
        }

        public T GetResult()
        {
            if (Error != null) throw new DomainException(Error);
            return Data;
        }

        public void TryThrow()
        {
            if (Error != null) throw new DomainException(Error);
        }
    }
}