using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.Contracts.Categories;
using AutoMapper;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализировать экземпляр
        /// </summary>
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public Task<Guid> CreateAsync(CategoryCreateModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CategoryCreateModel, Category>(model);

            return _categoryRepository.AddAsync(entity, cancellationToken);
        }

        public Task<CategoryInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}
