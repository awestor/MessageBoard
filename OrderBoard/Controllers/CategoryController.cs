using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Categories.Services;
using OrderBoard.Contracts.Categories;
using OrderBoard.Contracts.Items;
using OrderBoard.Domain.Entities;
using System.Net;

namespace OrderBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Create category")]
        public async Task<IActionResult> Post([FromBody] CategoryCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _categoryService.CreateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpGet("{id:guid}Get by Id")]
        [ProducesResponseType(typeof(CategoryInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        [HttpPost("Update category by Id")]
        [ProducesResponseType(typeof(CategoryInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateById([FromBody] CategoryDataModel model, CancellationToken cancellationToken)
        {
            var result = await _categoryService.UpdateAsync(model, cancellationToken);
            return Ok(result);
        }
        [HttpPost("{id:guid}Delete category by Id")]
        [ProducesResponseType(typeof(CategoryInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(id, cancellationToken);
            return Ok("Категория была удалёна");
        }
    }
}
