using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Categories.Services;

namespace OrderBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
    }
}
