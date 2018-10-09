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
        private readonly IHostingEnvironment _environment;
        private HttpStatusCode _statusCode = HttpStatusCode.OK;

        public GlobalExceptionFilter(
            IHostingEnvironment environment,
            ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger(GetType());

            _environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            PrepareResponseForException(out var message, context.Exception);
            context.ExceptionHandled = true;

            context.Result = new ObjectResult(message)
            {
                StatusCode = (int) _statusCode
            };
        }

        private void PrepareResponseForException(out Message message, Exception exception)
        {
            object errorData = _environment.IsDevelopment() || _environment.IsStaging() ? exception : null;

            if (exception is DomainException domainEx)
            {
                var error = new Error(domainEx.ErrorType, domainEx.Source, domainEx.ErrorDescription,
                    domainEx.ErrorData);

                message = new Message(error);

                if (domainEx.StatusCode.HasValue) _statusCode = domainEx.StatusCode.Value;
                _logger.LogWarning(exception, nameof(GlobalExceptionFilter));
            }
            else
            {
                var error = new Error(ErrorType.UnknownError, "unknown error", errorData);
                _statusCode = HttpStatusCode.InternalServerError;
                message = new Message(error);
                _logger.LogError(exception, nameof(GlobalExceptionFilter));
            }
        }
    }
}
