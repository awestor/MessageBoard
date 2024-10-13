﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.AppServices.Items.Services;
using OrderBoard.AppServices.OrderItems.SpecificationContext.Builders;
using OrderBoard.AppServices.Orders.Services;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.AppServices.Repository.Repository;
using OrderBoard.AppServices.Repository.Services;
using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.Items.Requests;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.OrderItem.Requests;
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
        private readonly IOrderItemSpecificationBuilder _orderItemSpecificationBuilder;

        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper,
             IOrderService orderService, IItemService itemService,
             IOrderItemSpecificationBuilder orderItemSpecificationBuilder)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _itemService = itemService;
            _orderService = orderService;
            _orderItemSpecificationBuilder = orderItemSpecificationBuilder;
        }

        public async Task<Guid?> CreateAsync(OrderItemCreateModel model, CancellationToken cancellationToken)
        {   
            var ItemTempModel = await _itemService.GetByIdAsync(model.ItemId, cancellationToken);
            if (ItemTempModel.Count >= model.Count)
            {
                var orderId = await _orderService.GetOrderIdByUserIdAsync(cancellationToken);

                var specification = _orderItemSpecificationBuilder.Build(model.ItemId, orderId);
                var modelTwo = await _orderItemRepository.GetDataBySpecificationAsync(specification, cancellationToken);
                if (modelTwo == null)
                {
                    var entity = _mapper.Map<OrderItemCreateModel, OrderItem>(model);
                    entity.OrderId = orderId;
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

        public async Task<OrderItemInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var result = await _orderItemRepository.GetInfoByIdAsync(id, cancellationToken);
            if (result == null)
            {
                throw new EntitiesNotFoundException("Поле заказа не найдено.");
            }
            return result;
        }
        public async Task<List<OrderItemInfoModel>> GetAllByOrderIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var result = await _orderItemRepository.GetAllInfoByOrderIdAsync(id, cancellationToken);
            return result;
        }
        public Task<List<OrderItemDataModel>> GetAllByOrderIdInDataModelAsync(Guid? id, CancellationToken cancellationToken)
        {
            var result = _orderItemRepository.GetAllDataByOrderIdAsync(id, cancellationToken);
            return result;
        }
        
        public async Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var OrderItemTempModel = await _orderItemRepository.GetDataByIdAsync(id, cancellationToken);
            if (OrderItemTempModel != null)
            {
                var entity = _mapper.Map<OrderItemDataModel, OrderItem>(OrderItemTempModel);
                await _orderItemRepository.DeleteAsync(entity, cancellationToken);
                return;
            }
            throw new EntitiesNotFoundException ("Указанного поля заказа не существует или оно было уже удалёно.");
        }

        public async Task DeleteForOrderDeleteAsync(OrderItemDataModel OrderItemTempModel, CancellationToken cancellationToken)
        {
            if (OrderItemTempModel != null)
            {
                var entity = _mapper.Map<OrderItemDataModel, OrderItem>(OrderItemTempModel);
                await _orderItemRepository.DeleteAsync(entity, cancellationToken);
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
            var orderItemModel = await _orderItemRepository.GetDataByIdAsync(model.Id, cancellationToken) 
                ?? throw new EntitiesNotFoundException("Указанного поля заказа не существует или оно было уже удалёно.");
            orderItemModel.OrderPrice = orderItemModel.OrderPrice / orderItemModel.Count * model.Count;
            orderItemModel.Count = model.Count;
            var entity = _mapper.Map<OrderItemDataModel, OrderItem>(orderItemModel);
            await _orderItemRepository.UpdateAsync(entity, cancellationToken);
            return;
        }

        public Task<List<OrderItemInfoModel>> GetOrderItemWithPaginationAsync(SearchOrderItemFromOrderRequest request,
            CancellationToken cancellationToken)
        {
            var specification = _orderItemSpecificationBuilder.Build(request);
            return _orderItemRepository.GetBySpecificationWithPaginationAsync
                (specification, request.Take, request.Skip, cancellationToken);
        }
    }
}