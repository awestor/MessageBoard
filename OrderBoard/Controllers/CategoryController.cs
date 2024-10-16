using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Categories.Services;
using OrderBoard.Contracts.BasePagination;
using OrderBoard.Contracts.Categories;
using OrderBoard.Contracts.Categories.Requests;
using OrderBoard.Contracts.Items;
using OrderBoard.Domain.Entities;
using System.Net;

namespace OrderBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Create category")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CategoryCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _categoryService.CreateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpGet("{id:guid}Get category by Id")]
        [ProducesResponseType(typeof(CategoryInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost("Get category by Name")]
        [ProducesResponseType(typeof(CategoryInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByNameAsync([FromBody] SearchCategoryByNameRequest name, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetByNameAsync(name, cancellationToken);
            return Ok(result);
        }

        [HttpPost("Get all category")]
        [ProducesResponseType(typeof(List<CategoryInfoModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromBody] SearchCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetAllByRequestAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("Update category by Id")]
        [ProducesResponseType(typeof(CategoryInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateById([FromBody] CategoryDataModel model, CancellationToken cancellationToken)
        {
            var result = await _categoryService.UpdateAsync(model, cancellationToken);
            return Ok(result);
        }
        [HttpPost("Delete category by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteById(DeleteCategoryRequest ids, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(ids, cancellationToken);
            return Ok("Категория была удалёна");
        }
    }
}
