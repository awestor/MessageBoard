using AutoMapper;
using Microsoft.AspNetCore.Http;
using OrderBoard.AppServices.Items.Services;
using OrderBoard.AppServices.Orders.Services;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.AppServices.Repository.Repository;
using OrderBoard.AppServices.Repository.Services;
using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Domain.Entities;
using System.Net;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<Guid?> CreateAsync(OrderItemCreateModel model, CancellationToken cancellationToken)
        {   
            var ItemTempModel = await _itemService.GetByIdAsync(model.ItemId, cancellationToken);
            if (ItemTempModel.Count >= model.Count)
            {
                var OrderTempModel = await _orderService.GetOrderIdByUserIdAsync(cancellationToken);
                if(OrderTempModel == null) 
                {
                    var tempId = await _orderService.CreateByAuthAsync(cancellationToken);
                    OrderTempModel = await _orderService.GetForUpdateAsync(tempId, cancellationToken);
                }
                
                OrderTempModel.OrderStatus = OrderStatus.Draft;
                await _orderService.UpdateAsync(OrderTempModel, cancellationToken);

                var modelTwo = await GetForAddAsync(model.ItemId, OrderTempModel.Id, cancellationToken);
                if (modelTwo == null)
                {
                    var entity = _mapper.Map<OrderItemCreateModel, OrderItem>(model);
                    entity.OrderId = OrderTempModel.Id;
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
            throw new Exception("Количество товара в наличае меньше чем указано в поле заказа.");
        }

        public Task<OrderItemInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var result =  _orderItemRepository.GetByIdAsync(id, cancellationToken);
            return result;
        }
        public Task<List<OrderItemInfoModel>> GetAllByOrderIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetAllByOrderIdAsync(id, cancellationToken);
        }
        public Task<List<OrderItemDataModel>> GetAllByOrderIdInDataModelAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetAllByOrderIdInDataModelAsync(id, cancellationToken);
        }
        public Task<OrderItemDataModel> GetForUpdateAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetForUpdateAsync(id, cancellationToken);
        }
        public Task<OrderItemDataModel> GetForAddAsync(Guid? ItemId, Guid? OrderId, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetForAddAsync(ItemId, OrderId, cancellationToken);
        }
        
        public async Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var OrderItemTempModel = await _orderItemRepository.GetForUpdateAsync(id, cancellationToken);
            if (OrderItemTempModel != null)
            {
                var entity = _mapper.Map<OrderItemDataModel, OrderItem>(OrderItemTempModel);
                await _orderItemRepository.DeleteByModelAsync(entity, cancellationToken);
                return;
            }
            throw new EntitiesNotFoundException ("Указанного поля заказа не существует или оно было уже удалёно!");
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

        public async Task SetCountAsync(List<OrderItemDataModel> orderItemList, CancellationToken cancellationToken)
        {
            List<ItemDataModel> ItemList = [];
            foreach (var orderItem in orderItemList)
            {
                var TempItemModel = await _itemService.GetForUpdateAsync(orderItem.ItemId, cancellationToken);
                if (TempItemModel.Count >= orderItem.Count)
                {
                    TempItemModel.Count -= orderItem.Count;
                    ItemList.Add(TempItemModel);
                }
                else
                {
                    throw new Exception("Количество товара в заказе больше, чем есть в наличе!");
                }
            }
            foreach (var item in ItemList)
            {
                await _itemService.UpdateAsync(item, cancellationToken);
            }
            return;
        }

        public async Task UpdateAsync(OrderItemUpdateModel model, CancellationToken cancellationToken)
        {
            var orderItemModel = await _orderItemRepository.GetForUpdateAsync(model.Id, cancellationToken) 
                ?? throw new EntitiesNotFoundException("Указанного поля заказа не существует или оно было уже удалёно!");
            orderItemModel.Count = model.Count;
            orderItemModel.OrderPrice = orderItemModel.OrderPrice / orderItemModel.Count * model.Count;
            var entity = _mapper.Map<OrderItemDataModel, OrderItem>(orderItemModel);
            await _orderItemRepository.UpdateAsync(entity, cancellationToken);
            return;
        }
    }
}
