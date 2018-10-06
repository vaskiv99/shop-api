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
    public class CategoriesController : BaseCtrl
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region commamds

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CategoryResponse), 200)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return JsonResult(result);
        }

        [HttpPut]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CategoryResponse), 200)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return JsonResult(result);
        }

        [HttpDelete("id")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteCategory([FromRoute] long id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand { Id = id });

            return JsonResult(result);
        }

        #endregion

        #region queries

        [HttpGet("id")]
        [ValidateModel]
        [ProducesResponseType(typeof(CategoryResponse), 200)]
        public async Task<IActionResult> GetCategory([FromRoute] long id)
        {
            var result = await _mediator.Send(new GetCategoryById { Id = id });

            return JsonResult(result);
        }

        [HttpGet]
        [ValidateModel]
        [ProducesResponseType(typeof(QueryResult<CategoryResponse>), 200)]
        public async Task<IActionResult> GetCategories([FromQuery] GetCategories query)
        {
            var result = await _mediator.Send(query);

            return JsonResult(result);
        }

        #endregion
    }
}