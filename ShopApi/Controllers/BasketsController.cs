using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopService.Services.Commands;
using ShopService.Services.Query;
using ShopService.Services.Responses;

namespace ShopService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : BaseCtrl
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region commands

        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        [ProducesResponseType(typeof(Boolean), 200)]
        public async Task<IActionResult> AddItemToBasket([FromBody] AddItemToBasket command)
        {
            command.UserId = UserId;
            var result = await _mediator.Send(command);

            return JsonResult(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Customer")]
        [ProducesResponseType(typeof(Boolean), 200)]
        public async Task<IActionResult> RemoveItemFromBasket([FromBody] DeleteItemFromBasket command)
        {
            command.UserId = UserId;
            var result = await _mediator.Send(command);

            return JsonResult(result);
        }

        #endregion

        #region queries

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        [ProducesResponseType(typeof(UserBasketResponse), 200)]
        public async Task<IActionResult> GetBasketByUser([FromQuery] GetBasketByUserId query)
        {
            query.UserId = UserId;
            var result = await _mediator.Send(query);

            return JsonResult(result);
        }

        #endregion
    }
}