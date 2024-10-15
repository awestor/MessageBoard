using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Items.Services;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.Items.Requests;
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
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] ItemCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _itemService.CreateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ItemInfoModel), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _itemService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                throw new EntitiesNotFoundException("Товар не найден.");
            }
            return Ok(result);
        }
        [HttpPost("Get all your Item")]
        [ProducesResponseType(typeof(List<ItemInfoModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllItem(SearchItemByUserIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _itemService.GetAllItemAsync(request, cancellationToken)
                ?? throw new EntitiesNotFoundException("Вы не продаёте никаких товаров.");
            return Ok(result);
        }
        [HttpPost("Get with pagination")]
        [ProducesResponseType(typeof(List<ItemInfoModel>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetItemWithPaginationAsync(SearchItemForPaginationRequest request, CancellationToken cancellationToken)
        {
            var result = await _itemService.GetItemWithPaginationAsync(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("Get by name with pagination")]
        [ProducesResponseType(typeof(List<ItemInfoModel>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllItemByNameAsync(SearchItemByNameRequest request, CancellationToken cancellationToken)
        {
            var result = await _itemService.GetAllItemByNameAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("Update Item")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(ItemUpdateModel model, CancellationToken cancellationToken)
        {
            var result = await _itemService.UpdateAsync(model, cancellationToken);
            return Ok(result + "\n Обновление успешно.");
        }
        [HttpPost("Delete Item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _itemService.DeleteByIdAsync(id, cancellationToken);
            return Ok("Товар был удалён.");
        }
    }
}
