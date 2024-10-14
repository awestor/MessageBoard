using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OrderBoard.AppServices.Orders.Repository;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.AppServices.Users.Repository;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;
using System.Security.Claims;
using OrderBoard.AppServices.Orders.SpecificationContext.Builders;
using OrderBoard.Contracts.Orders.Requests;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.AppServices.Repository.Repository;

namespace OrderBoard.AppServices.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderSpecificationBuilder _orderSpecificationBuilder;

        public OrderService(IOrderRepository orderRepository, IMapper mapper,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository, IOrderSpecificationBuilder orderSpecificationBuilder,
            IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _configuration = configuration;
            _orderSpecificationBuilder = orderSpecificationBuilder;
            _httpContextAccessor = httpContextAccessor;
            _orderItemRepository = orderItemRepository;
        }
        /// <summary>
        /// Добавление нового заказа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Guid?> CreateAsync(OrderCreateModel model, CancellationToken cancellationToken)
        {
            var OrderModel = await _orderRepository.GetByUserIdAsync(model.UserId, cancellationToken);

            if (OrderModel != null)
            {
                throw new Exception("У данного пользователя уже существует наоплаченный заказ." +
                    " Прежде чем создавать новый заказ оплатите текущий.");
            }
            var entity = _mapper.Map<OrderCreateModel, Order>(model);

            return await _orderRepository.AddAsync(entity, cancellationToken);
        }
        public async Task<Guid?> CreateByAuthAsync(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = (claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value) 
                ?? throw new Exception("Авторизуйтесь повторно");
            var userId = Guid.Parse(claimId);
            var OrderModel = await _orderRepository.GetByUserIdAsync(userId, cancellationToken);
            if (OrderModel != null)
            {
                throw new Exception("У данного пользователя уже существует наоплаченный заказ." +
                    " Прежде чем создавать новый заказ оплатите текущий.");
            }
            var ocm = new OrderCreateModel
            {
                UserId = userId
            };
            var orderId = await CreateAsync(ocm, cancellationToken);

            return orderId;
        }
        /// <summary>
        /// Получение заказа по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<OrderInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var model = await _orderRepository.GetDataByIdAsync(id, cancellationToken)
            ?? throw new EntitiesNotFoundException("Заказ не найден.");
            var result = await SetTotalInfoAsync(model, id, cancellationToken);
            return result;
        }
        public async Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var model = await _orderRepository.GetDataByIdAsync(id, cancellationToken)
                ?? throw new EntitiesNotFoundException("Заказ не найден");
            await DeleteChildByIdAsync(id, cancellationToken);

            var entity = _mapper.Map<OrderDataModel, Order>(model);
            await _orderRepository.DeleteByModelAsync(entity, cancellationToken);
            return;
        }
        public async Task DeleteChildByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            List<OrderItemDataModel> OrderItemList = [];
            OrderItemList = await _orderItemRepository.GetAllDataByOrderIdAsync(id, cancellationToken);

            foreach (var OrderItems in OrderItemList)
            {
                var tempMapedModel = _mapper.Map<OrderItemDataModel, OrderItem>(OrderItems);
                await _orderItemRepository.DeleteAsync(tempMapedModel, cancellationToken);
            }
            return;
        }
        /// <summary>
        /// Подтверждение статуса заказа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Guid?> ConfrimOrderById(Guid? id, CancellationToken cancellationToken)
        {
            var model = await _orderRepository.GetDataByIdAsync(id, cancellationToken)
                ?? throw new EntitiesNotFoundException("Заказ не найден.");
            var entity = _mapper.Map<OrderDataModel, Order>(model);
            entity.PaidAt = DateTime.UtcNow;
            entity.OrderStatus = Contracts.Enums.OrderStatus.Ordered;
            var result = await _orderRepository.UpdateAsync(entity, cancellationToken);
            return result;
        }

        public async Task<Guid?> GetOrderIdByUserIdAsync(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = (claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value) 
                ?? throw new Exception("Авторизуйтесь повторно");
            var OrderModel = await _orderRepository.GetByUserIdAsync(new Guid(claimId), cancellationToken);
            if (OrderModel == null)
            {
                var newResult = await CreateForOrderItemAsync(new Guid(claimId), cancellationToken);
                return newResult;
            }
            var result = OrderModel.Id;
            return result;
        }
        public async Task<Guid?> CreateForOrderItemAsync(Guid? userId, CancellationToken cancellationToken)
        {
            var ocm = new OrderDataModel
            {
                UserId = userId,
                OrderStatus = Contracts.Enums.OrderStatus.Draft,
                CreatedAt = DateTime.UtcNow
            };
            var entity = _mapper.Map<OrderDataModel, Order>(ocm);

            return await _orderRepository.AddAsync(entity, cancellationToken);
        }

        public async Task<List<OrderInfoModel>> GetOrderWithPaginationAsync(SearchOrderRequest request, CancellationToken cancellationToken)
        {
            var specification = _orderSpecificationBuilder.Build(request);
            var modelList = await _orderRepository.GetBySpecificationWithPaginationAsync(specification, request.Take, request.Skip, cancellationToken);
            List<OrderInfoModel> result = [];

            foreach (var order in modelList)
            {
                result.Insert(0, await SetTotalInfoAsync(order, order.Id, cancellationToken));
            }

            return result;
        }
        public async Task<List<OrderInfoModel>> GetOrderWithPaginationAuthAsync(SearchOrderAuthRequest request, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var modRequest = _mapper.Map<SearchOrderAuthRequest, SearchOrderRequest>(request);
            modRequest.UserId = new Guid(claimId);
            var specification = _orderSpecificationBuilder.Build(modRequest);
            var modelList = await _orderRepository.GetBySpecificationWithPaginationAsync(specification, request.Take, request.Skip, cancellationToken);
            List<OrderInfoModel> result = [];
            
            foreach (var order in modelList)
            {
                result.Insert(0, await SetTotalInfoAsync(order, order.Id, cancellationToken));
            }

            return result;
        }

        public async Task<OrderInfoModel> SetTotalInfoAsync(OrderDataModel? model, Guid? id, CancellationToken cancellationToken)
        {
            List<OrderItemDataModel> OrderItemList = [];
            OrderItemList = await _orderItemRepository.GetAllDataByOrderIdAsync(id, cancellationToken);
            var result = _mapper.Map<OrderDataModel, OrderInfoModel>(model); ;
            result.TotalCount  = 0;
            result.TotalPrice  = 0;
            foreach (var item in OrderItemList)
            {
                result.TotalPrice += item.OrderPrice;
                result.TotalCount += item.Count;
            }
            return result;
        }
    }
}
