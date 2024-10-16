using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.AppServices.Items.SpecificationContext.Builders;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.AppServices.Other.Services;
using OrderBoard.AppServices.Repository.Repository;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.Items.Requests;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Domain.Entities;
using System.Net;
using System.Security.Claims;


namespace OrderBoard.AppServices.Items.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IItemSpecificationBuilder _itemSpecificationBuilder;
        private readonly IStructuralLoggingService _structuralLoggingService;
        private readonly ILogger<ItemService> _logger;


        public ItemService(IItemRepository itemRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOrderItemRepository orderItemRepository, IItemSpecificationBuilder itemSpecificationBuilder,
            IStructuralLoggingService structuralLoggingService,
            ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _orderItemRepository = orderItemRepository;
            _itemSpecificationBuilder = itemSpecificationBuilder;
            _structuralLoggingService = structuralLoggingService;
            _logger = logger;
        }

        public Task<Guid?> CreateAsync(ItemCreateModel model, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? throw new Exception("Пожалуйста, авторизируйтесь заново.");
            var entity = _mapper.Map<ItemCreateModel, Item>(model);
            entity.UserId = Guid.Parse(claimId);
            _structuralLoggingService.PushProperty("CreateRequest", model);
            _logger.LogInformation("Товар был создан.");
            return _itemRepository.AddAsync(entity, cancellationToken);
        }
        public async Task<Guid?> UpdateAsync(ItemUpdateModel model, CancellationToken cancellationToken)
        {
            var newModel = await _itemRepository.GetForUpdateAsync(model.Id, cancellationToken) ?? throw new EntitiesNotFoundException("Товар не найден");

            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimsId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (newModel.UserId.ToString() != claimsId)
            {
                throw new Exception(HttpStatusCode.Forbidden.ToString() + "Отказано в праве доступа.");
            }

            if (model.Description != null) newModel.Description = model.Description;
            if (model.Price != null && model.Price > 0) newModel.Price = model.Price;
            if (model.Name != null) newModel.Name = model.Name;
            if (model.Count != null && model.Count > 0) newModel.Count = model.Count;
            if (model.Comment != null) newModel.Comment = model.Comment;

            var entity = _mapper.Map<ItemDataModel, Item>(newModel);
            _structuralLoggingService.PushProperty("UpdateRequest", model);
            _logger.LogInformation("Товар был обновлён.");
            return await _itemRepository.UpdateAsync(entity, cancellationToken);
        }
        public async Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var model = await _itemRepository.GetForUpdateAsync(id, cancellationToken);

            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimsId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (model.UserId.ToString() != claimsId)
            {
                throw new Exception(HttpStatusCode.Forbidden.ToString() + "Отказано в праве доступа.");
            }
            var check = await _orderItemRepository.GetAllDataByItemIdAsync(id, cancellationToken);
            if (check != null)
            {
                foreach (var orderItem in check) 
                {
                    orderItem.ItemId = null;
                    var TOI = _mapper.Map<OrderItemDataModel, OrderItem>(orderItem);
                    await _orderItemRepository.UpdateAsync(TOI,cancellationToken);
                }
            }

            var entity = _mapper.Map<ItemDataModel, Item>(model);
            await _itemRepository.DeleteAsync(entity, cancellationToken);

            _structuralLoggingService.PushProperty("DeleteRequest", id);
            _logger.LogInformation("Товар был удалён.");
            return;
        }

        public async Task<ItemInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var result = await _itemRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new EntitiesNotFoundException("Товар не найден");
            return result;
        }

        public async Task<ItemDataModel> GetForUpdateAsync(Guid? id, CancellationToken cancellationToken)
        {
            var result = await _itemRepository.GetForUpdateAsync(id, cancellationToken)
                ?? throw new EntitiesNotFoundException("Товар не найден");
            return result;
        }
        public async Task<Guid?> UpdateAsync(ItemDataModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ItemDataModel, Item>(model);
            return await _itemRepository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<List<ItemInfoModel>> GetAllItemAsync(SearchItemByUserIdRequest request, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimsId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? throw new Exception("Пожалуйста, авторизируйтесь заново.");
            var specification = _itemSpecificationBuilder.Build(Guid.Parse(claimsId), request);
            _structuralLoggingService.PushProperty("SearchRequest", request);
            _logger.LogInformation("Выборка товаров была получена.");
            return await _itemRepository.GetBySpecificationWithPaginationAsync(specification,
                request.Take, request.Skip, cancellationToken);
        }

        public async Task<List<ItemInfoModel>> GetItemWithPaginationAsync(SearchItemForPaginationRequest request, CancellationToken cancellationToken)
        {
            var specification = _itemSpecificationBuilder.Build(request);
            return await _itemRepository.GetBySpecificationWithPaginationAsync(specification, request.Take, request.Skip, cancellationToken);
        }

        public async Task<List<ItemInfoModel>> GetAllItemByNameAsync(SearchItemByNameRequest request, CancellationToken cancellationToken)
        {
            var specification = _itemSpecificationBuilder.Build(request);
            return await _itemRepository.GetBySpecificationWithPaginationAsync(specification, request.Take, request.Skip, cancellationToken);
        }
    }
}
