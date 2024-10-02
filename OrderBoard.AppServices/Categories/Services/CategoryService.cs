using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.Contracts.Categories;
using OrderBoard.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<Guid> CreateAsync(CategoryCreateModel model, CancellationToken cancellationToken)
        {
            var entity = new Category
            {
                Name = model.Title,
                Created = DateTime.UtcNow
            };

            return _categoryRepository.AddAsync(entity, cancellationToken);
        }

        public Task<CategoryInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}
