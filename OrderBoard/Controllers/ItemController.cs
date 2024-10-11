using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Exceptions;
using OrderBoard.AppServices.Items.Services;
using OrderBoard.Contracts.Items;
using System.Net;

namespace OrderBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _itemService.CreateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ItemInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _itemService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                throw new EntitiesNotFoundException("Товар не найден.");
            }
            return Ok(result);
        }
    }
}
