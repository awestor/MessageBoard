using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.Contracts.Categories;
using AutoMapper;
using OrderBoard.Domain.Entities;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.Contracts.BasePagination;
using OrderBoard.AppServices.Categories.SpecificationContext.Builders;
using OrderBoard.Contracts.Categories.Requests;
using System.Xml.Linq;

namespace OrderBoard.AppServices.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ICategorySpecificationBuilder _categorySpecificationBuilder;

        /// <summary>
        /// Инициализировать экземпляр
        /// </summary>
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper,
            ICategorySpecificationBuilder categorySpecificationBuilder)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _categorySpecificationBuilder = categorySpecificationBuilder;
        }

        public async Task<Guid?> CreateAsync(CategoryCreateModel model, CancellationToken cancellationToken)
        {
            var specification = _categorySpecificationBuilder.Build(model.Title);
            var tempModel = await _categoryRepository.GetBySpecificationAsync
                (specification, cancellationToken);
            if (tempModel != null) throw new EntititysNotVaildException("Категория уже существует.");
            if (model.ParentId != null)
            {
                var tempModel2 = await _categoryRepository.GetDataByIdAsync(model.ParentId, cancellationToken)
                    ?? throw new EntitiesNotFoundException("Категория родитель не была найдена.");
            }

            var entity = _mapper.Map<CategoryCreateModel, Category>(model);
            return await _categoryRepository.AddAsync(entity, cancellationToken);
        }

        public async Task<Guid?> UpdateAsync(CategoryDataModel model, CancellationToken cancellationToken)
        {
            var tempModel = await _categoryRepository.GetDataByIdAsync(model.Id, cancellationToken) 
                ?? throw new EntitiesNotFoundException("Категория не была найдена.");
            if(model.ParentId != null) { 
            tempModel = await _categoryRepository.GetDataByIdAsync(model.ParentId, cancellationToken)
                ?? throw new EntitiesNotFoundException("Категория родитель не была найдена.");
            }

            var entity = _mapper.Map<CategoryDataModel, Category>(model);
            await _categoryRepository.UpdateAsync(entity, cancellationToken);
            return model.Id;
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = await _categoryRepository.GetDataByIdAsync(id, cancellationToken);
            if (model == null)
            {
                throw new EntitiesNotFoundException("Категория не была найдена.");
            }
            var entity = _mapper.Map<CategoryDataModel, Category>(model);
            await _categoryRepository.DeleteByIdAsync(entity, cancellationToken);
            return;
        }
        public async Task<CategoryInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var specification = _categorySpecificationBuilder.Build(id);
            var result = await _categoryRepository.GetBySpecificationAsync
                (specification, cancellationToken)
                ?? throw new EntitiesNotFoundException("Категория не найдена или была удалена");
            return result;
        }

        public async Task<CategoryInfoModel> GetByNameAsync(SearchCategoryByNameRequest? name, CancellationToken cancellationToken)
        {
            var specification = _categorySpecificationBuilder.Build(name.Name);
            var result = await _categoryRepository.GetBySpecificationAsync
                (specification, cancellationToken)
                ?? throw new EntitiesNotFoundException("Категория не найдена или была удалена");
            return result;
        }

        public async Task<List<CategoryInfoModel>> GetAllByRequestAsync(SearchCategoryRequest request, CancellationToken cancellationToken)
        {
            var specification = _categorySpecificationBuilder.Build(request);
            return await _categoryRepository.GetBySpecificationWithPaginationAsync
                (specification, request.Take, request.Skip, cancellationToken);
        }
    }
}
