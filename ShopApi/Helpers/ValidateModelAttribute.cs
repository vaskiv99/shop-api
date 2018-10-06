using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShopService.Common.Enums;
using ShopService.Common.Infrastructure;
using ShopService.Common.Interfaces;

namespace ShopService.Web.Helpers
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            var result = new ContentResult();

            if (modelState == null || !modelState.IsValid)
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                result.Content = JsonConvert.SerializeObject(ErrorResponse(GetErrors(modelState).Error), settings);
                context.Result = result;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";
            }
        }

        private IOperationResult<string> GetErrors(ModelStateDictionary dictionary)
        {
            var modelErrors = dictionary?.Values.SelectMany(modelState => modelState.Errors);
            if (modelErrors == null)
                return new OperationResult<string>(ErrorType.InvalidRequestModel);

            try
            {
                JsonConvert.SerializeObject(modelErrors);
            }
            catch (JsonSerializationException)
            {
                return new OperationResult<string>(ErrorType.InvalidRequestModel);
            }

            return new OperationResult<string>(ErrorType.InvalidRequestModel, modelErrors);
        }

        private static IMessage ErrorResponse(IError error)
        {
            return new Message()
            {
                Data = null,
                Description = "Error",
                IsSuccess = false,
                Error = error
            };
        }
    }
}
