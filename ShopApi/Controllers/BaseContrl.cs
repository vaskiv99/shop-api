using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ShopService.Common.Infrastructure;
using ShopService.Web.Helpers;

namespace ShopService.Web.Controllers
{
    public class BaseCtrl : ControllerBase
    {
        #region messages

        [NonAction]
        protected IActionResult JsonResult(QueryResult query)
        {
            var message = new Message(query);
            return Ok(message);
        }

        [NonAction]
        protected IActionResult JsonResult(object data)
        {
            var message = new Message(data);
            return Ok(message);
        }

        #endregion messages

        #region properties

        protected string UserId
        {
            get
            {
                return User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value;
            }
        }

        #endregion

    }
}