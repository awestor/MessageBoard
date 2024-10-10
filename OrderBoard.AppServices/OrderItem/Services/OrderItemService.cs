using AutoMapper;
using Microsoft.AspNetCore.Http;
using OrderBoard.AppServices.Items.Services;
using OrderBoard.AppServices.Orders.Services;
using OrderBoard.AppServices.Repository.Repository;
using OrderBoard.AppServices.Repository.Services;
using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Domain.Entities;
using System.Net;
using System.Threading;

namespace OrderBoard.AppServices.Users.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper,
             IOrderService orderService, IItemService itemService)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _itemService = itemService;
            _orderService = orderService;
        }

        public async Task<Guid> CreateAsync(OrderItemCreateModel model, CancellationToken cancellationToken)
        {   
            var ItemTempModel = await _itemService.GetForUpdateAsync(model.ItemId, cancellationToken);
            if (ItemTempModel.Count >= model.Count)
            {
                var OrderTempModel = await _orderService.GetForUpdateAsync(model.OrderId, cancellationToken);

                OrderTempModel.TotalCount += model.Count;
                OrderTempModel.TotalPrice += model.Count * ItemTempModel.Price;
                OrderTempModel.OrderStatus = OrderStatus.Draft;
                await _orderService.UpdateAsync(OrderTempModel, cancellationToken);

                await SetCountAsync(ItemTempModel, model.Count, false, cancellationToken);

                var modelTwo = await GetForAddAsync(model.ItemId, model.OrderId, cancellationToken);
                if (modelTwo == null)
                {
                    var entity = _mapper.Map<OrderItemCreateModel, OrderItem>(model);
                    entity.OrderPrice = model.Count * ItemTempModel.Price;
                    return await _orderItemRepository.AddAsync(entity, cancellationToken);
                }
                else
                {
                    modelTwo.Count += model.Count;
                    modelTwo.OrderPrice += model.Count * ItemTempModel.Price;
                    var entity = _mapper.Map<OrderItemDataModel, OrderItem>(modelTwo);
                    return await _orderItemRepository.UpdateAsync(entity, cancellationToken);
                }
            }
            throw new Exception("При попытке зарезервировать товары произошла ошибка!");          
        }

        public Task<OrderItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result =  _orderItemRepository.GetByIdAsync(id, cancellationToken);
            return result;
        }
        public Task<List<OrderItemInfoModel>> GetAllByOrderIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetAllByOrderIdAsync(id, cancellationToken);
        }
        public Task<List<OrderItemDataModel>> GetAllByOrderIdInDataModelAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetAllByOrderIdInDataModelAsync(id, cancellationToken);
        }
        public Task<OrderItemDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetForUpdateAsync(id, cancellationToken);
        }
        public Task<OrderItemDataModel> GetForAddAsync(Guid ItemId, Guid OrderId, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetForAddAsync(ItemId, OrderId, cancellationToken);
        }
        
        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var OrderItemTempModel = await _orderItemRepository.GetForUpdateAsync(id, cancellationToken);
            if (OrderItemTempModel != null)
            {
                var ItemTempModel = await _itemService.GetForUpdateAsync(OrderItemTempModel.ItemId, cancellationToken);
                var OrderTempModel = await _orderService.GetForUpdateAsync(OrderItemTempModel.OrderId, cancellationToken);

                if (OrderTempModel != null) {
                    OrderTempModel.TotalCount -= OrderItemTempModel.Count;
                    OrderTempModel.TotalPrice -= OrderItemTempModel.OrderPrice;
                    await _orderService.UpdateAsync(OrderTempModel, cancellationToken);
                }

                await SetCountAsync(ItemTempModel, OrderItemTempModel.Count, true, cancellationToken);

                var entity = _mapper.Map<OrderItemDataModel, OrderItem>(OrderItemTempModel);
                await _orderItemRepository.DeleteByModelAsync(entity, cancellationToken);
                return;
            }
            throw new Exception("Указанного поля заказа не существует или он был уже удалён!");
        }

        public async Task DeleteForOrderDeleteAsync(OrderItemDataModel OrderItemTempModel, CancellationToken cancellationToken)
        {
            if (OrderItemTempModel != null)
            {
                var entity = _mapper.Map<OrderItemDataModel, OrderItem>(OrderItemTempModel);
                await _orderItemRepository.DeleteByModelAsync(entity, cancellationToken);
            }
            return;
        }
        public async Task SetCountAsync(ItemDataModel ItemTempModel, decimal count, bool check, CancellationToken cancellationToken)
        {
            if (ItemTempModel != null)
            {
                if (check != false) 
                {
                    ItemTempModel.Count += count;
                }
                else ItemTempModel.Count -= count;
                await _itemService.UpdateAsync(ItemTempModel, cancellationToken);
                return;
            }
            throw new Exception("Указанного товара не существует или он был удалён за время оформления заказа!");
        }
        public async Task<ItemDataModel> GetItemClassAsync(Guid ItemId, CancellationToken cancellationToken)
        {
            return await _itemService.GetForUpdateAsync(ItemId, cancellationToken);
        }
    }
}
