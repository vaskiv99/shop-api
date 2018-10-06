using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ShopService.Common.Enums;
using ShopService.Common.Exceptions;
using ShopService.Common.Infrastructure;

namespace ShopService.Web.Helpers
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        private HttpStatusCode _statusCode = HttpStatusCode.OK;
        private readonly IHostingEnvironment _environment;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            PrepareResponseForException(out var message, context.Exception);

            context.Result = new ObjectResult(message)
            {
                StatusCode = (int)_statusCode
            };

            context.ExceptionHandled = true;
        }

        private void PrepareResponseForException(out Message message, Exception exception)
        {
            var data = _environment.IsDevelopment() || _environment.IsStaging() ? exception : null;

            if (exception is InvalidOperationException)
            {
                var error = new Error(ErrorType.InvalidOperation, exception.Message, data);
                message = new Message(error);
                _statusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(exception, exception.Message);
            }
            else if (exception is DomainException domainEx)
            {
                var error = new Error(domainEx.ErrorType, domainEx.ErrorDescription, data);
                message = new Message(error);
                if (domainEx.StatusCode.HasValue) _statusCode = domainEx.StatusCode.Value;
                _logger.LogWarning(exception, exception.Message);
            }
            else
            {
                _logger.LogError(exception, exception.Message);
                _statusCode = HttpStatusCode.InternalServerError;
                message = new Message(new Error(ErrorType.UnknownError, "unknown error", data));
            }
        }
    }
}
