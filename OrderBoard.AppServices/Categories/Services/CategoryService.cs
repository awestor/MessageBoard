using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.Contracts.Categories;
using AutoMapper;
using OrderBoard.Domain.Entities;
using OrderBoard.AppServices.Other.Exceptions;

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

        public Task<Guid?> CreateAsync(CategoryCreateModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CategoryCreateModel, Category>(model);

            return _categoryRepository.AddAsync(entity, cancellationToken);
        }

        public Task<CategoryInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Guid?> UpdateAsync(CategoryDataModel model, CancellationToken cancellationToken)
        {
            var tempModel = await _categoryRepository.GetForUpdateAsync(model.Id, cancellationToken) 
                ?? throw new EntitiesNotFoundException("Категория не была найдена.");
            if(model.ParentId != null) { 
            tempModel = await _categoryRepository.GetForUpdateAsync(model.ParentId, cancellationToken)
                ?? throw new EntitiesNotFoundException("Категория родитель не была найдена.");
            }

            var entity = _mapper.Map<CategoryDataModel, Category>(model);
            await _categoryRepository.UpdateAsync(entity, cancellationToken);
            return model.Id;
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = await _categoryRepository.GetForUpdateAsync(id, cancellationToken);
            if (model == null)
            {
                throw new EntitiesNotFoundException("Категория не была найдена.");
            }
            var entity = _mapper.Map<CategoryDataModel, Category>(model);
            await _categoryRepository.DeleteByIdAsync(entity, cancellationToken);
            return;
        }
    }
}
