using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopService.Common.Infrastructure;
using ShopService.Services.Commands;
using ShopService.Services.Query;
using ShopService.Services.Responses;
using ShopService.Web.Helpers;

namespace ShopService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class GoodsController : BaseCtrl
    {
        private readonly IMediator _mediator;

        public GoodsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region commands

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(GoodsResponse), 200)]
        public async Task<IActionResult> CreateGoods([FromBody] CreateGoodsCommand command)
        {
            var result = await _mediator.Send(command);

            return JsonResult(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(GoodsResponse), 200)]
        public async Task<IActionResult> UpdateGoods([FromBody] UpdateGoodsCommand command)
        {
            var result = await _mediator.Send(command);

            return JsonResult(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteGoods([FromRoute] long id)
        {
            var result = await _mediator.Send(new DeleteGoodsCommand { Id = id });

            return JsonResult(result);
        }

        #endregion

        #region queries

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GoodsResponse), 200)]
        public async Task<IActionResult> GetGoodsById([FromRoute] long id)
        {
            var result = await _mediator.Send(new GetGoodsById { Id = id });

            return JsonResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(QueryResult<GoodsResponse>), 200)]
        public async Task<IActionResult> GetGoodsByQuery([FromQuery] GetGoodsQuery query)
        {
            var result = await _mediator.Send(query);

            return JsonResult(result);
        }

        #endregion
    }
}