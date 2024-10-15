using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.Contracts.Categories;
using AutoMapper;
using OrderBoard.Domain.Entities;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.Contracts.BasePagination;
using OrderBoard.AppServices.Categories.SpecificationContext.Builders;
using OrderBoard.Contracts.Categories.Requests;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using OrderBoard.AppServices.Other.Services;

namespace OrderBoard.AppServices.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ICategorySpecificationBuilder _categorySpecificationBuilder;
        private readonly ILogger _logger;
        private readonly IStructuralLoggingService _structuralLoggingService;

        /// <summary>
        /// Инициализировать экземпляр
        /// </summary>
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper,
            ICategorySpecificationBuilder categorySpecificationBuilder,
            ILogger<Category> logger,
            IStructuralLoggingService structuralLoggingService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _categorySpecificationBuilder = categorySpecificationBuilder;
            _logger = logger;
            _structuralLoggingService = structuralLoggingService;
        }

        public async Task<Guid?> CreateAsync(CategoryCreateModel model, CancellationToken cancellationToken)
        {
            var specification = _categorySpecificationBuilder.Build(model.Title);
            var tempModel = await _categoryRepository.GetBySpecificationAsync
                (specification, cancellationToken);
            if (tempModel != null) throw new EntititysNotVaildException("Категория уже существует.");
            if (model.ParentId != null && model.ParentId != Guid.Empty)
            {
                var tempModel2 = await _categoryRepository.GetDataByIdAsync(model.ParentId, cancellationToken)
                    ?? throw new EntitiesNotFoundException("Категория родитель не была найдена.");
            }
            _structuralLoggingService.PushProperty("CreateRequest", model);
            _logger.LogInformation("Создана новая категория.");
            var entity = _mapper.Map<CategoryCreateModel, Category>(model);
            return await _categoryRepository.AddAsync(entity, cancellationToken);
        }

        public async Task<Guid?> UpdateAsync(CategoryDataModel model, CancellationToken cancellationToken)
        {
            var tempModel = await _categoryRepository.GetDataByIdAsync(model.Id, cancellationToken) 
                ?? throw new EntitiesNotFoundException("Категория не была найдена.");
            if(model.ParentId != null && model.ParentId != Guid.Empty) { 
            tempModel = await _categoryRepository.GetDataByIdAsync(model.ParentId, cancellationToken)
                ?? throw new EntitiesNotFoundException("Категория родитель не была найдена.");
            }

            _structuralLoggingService.PushProperty("UpdateRequest", model);
            _logger.LogInformation("Категория была обновлена.");
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
            List<CategoryDataModel> childList = [];
            childList = await _categoryRepository.GetAllChildDataByIdAsync(id, cancellationToken);
            foreach (var child in childList)
            {
                child.ParentId = null;
                var temp = _mapper.Map<CategoryDataModel, Category>(child);
                await _categoryRepository.UpdateAsync(temp, cancellationToken);
            }

            var entity = _mapper.Map<CategoryDataModel, Category>(model);

            await _categoryRepository.DeleteByIdAsync(entity, cancellationToken);
            _structuralLoggingService.PushProperty("DeleteRequest", model);
            _logger.LogInformation("Категория была удалена.");
            return;
        }
        public async Task<CategoryInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var specification = _categorySpecificationBuilder.Build(id);
            var result = await _categoryRepository.GetBySpecificationAsync
                (specification, cancellationToken)
                ?? throw new EntitiesNotFoundException("Категория не найдена или была удалена");

            _structuralLoggingService.PushProperty("SerchRequest", id);
            _logger.LogInformation("Категория была найдена.");
            return result;
        }

        public async Task<CategoryInfoModel> GetByNameAsync(SearchCategoryByNameRequest? name, CancellationToken cancellationToken)
        {
            var specification = _categorySpecificationBuilder.Build(name.Name);
            var result = await _categoryRepository.GetBySpecificationAsync
                (specification, cancellationToken)
                ?? throw new EntitiesNotFoundException("Категория не найдена или была удалена");

            _structuralLoggingService.PushProperty("SerchRequest", name);
            _logger.LogInformation("Категория была найдена.");
            return result;
        }

        public async Task<List<CategoryInfoModel>> GetAllByRequestAsync(SearchCategoryRequest request, CancellationToken cancellationToken)
        {
            var specification = _categorySpecificationBuilder.Build(request);

            _structuralLoggingService.PushProperty("SerchRequest", request);
            _logger.LogInformation("Список категорий подходящих под спецификацию был отображён.");
            return await _categoryRepository.GetBySpecificationWithPaginationAsync
                (specification, request.Take, request.Skip, cancellationToken);
        }
    }
}
