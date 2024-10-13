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

namespace OrderBoard.AppServices.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderSpecificationBuilder _orderSpecificationBuilder;

        public OrderService(IOrderRepository orderRepository, IMapper mapper,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository, IOrderSpecificationBuilder orderSpecificationBuilder)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _configuration = configuration;
            _orderSpecificationBuilder = orderSpecificationBuilder;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Добавление нового заказа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Guid?> CreateAsync(OrderCreateModel model, CancellationToken cancellationToken)
        {
            var OrderModel = _orderRepository.GetByUserIdAsync(model.UserId, cancellationToken);

            if (OrderModel != null)
            {
                throw new Exception("У данного пользователя уже существует наоплаченный заказ." +
                    " Прежде чем создавать новый заказ оплатите текущий.");
            }
            var entity = _mapper.Map<OrderCreateModel, Order>(model);

            return _orderRepository.AddAsync(entity, cancellationToken);
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
        public Task<OrderInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _orderRepository.GetByIdAsync(id, cancellationToken); ;
        }
        /// <summary>
        /// Получение модели заказа для обновления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<OrderDataModel> GetForUpdateAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _orderRepository.GetForUpdateAsync(id, cancellationToken);
        }
        /// <summary>
        /// Обновление полей заказа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Guid?> UpdateAsync(OrderDataModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<OrderDataModel, Order>(model);
            return _orderRepository.UpdateAsync(entity, cancellationToken);
        }
        public async Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var model = await _orderRepository.GetForUpdateAsync(id, cancellationToken);
            var entity = _mapper.Map<OrderDataModel, Order>(model);
            await _orderRepository.DeleteByModelAsync(entity, cancellationToken);
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
            var model = await _orderRepository.GetForUpdateAsync(id, cancellationToken);
            if (model == null)
            {
                throw new EntitiesNotFoundException("Заказ не найден.");
            }
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
                OrderStatus = Contracts.Enums.OrderStatus.Draft
            };
            var entity = _mapper.Map<OrderDataModel, Order>(ocm);

            return await _orderRepository.AddAsync(entity, cancellationToken);
        }

        public Task<List<OrderInfoModel>> GetOrderWithPaginationAsync(SearchOrderRequest request, CancellationToken cancellationToken)
        {
            var specification = _orderSpecificationBuilder.Build(request);
            return _orderRepository.GetBySpecificationWithPaginationAsync(specification, request.Take, request.Skip, cancellationToken);
        }
        public Task<List<OrderInfoModel>> GetOrderWithPaginationAuthAsync(SearchOrderAuthRequest request, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var modRequest = _mapper.Map<SearchOrderAuthRequest, SearchOrderRequest>(request);
            modRequest.UserId = new Guid(claimId);
            var specification = _orderSpecificationBuilder.Build(modRequest);
            return _orderRepository.GetBySpecificationWithPaginationAsync(specification, request.Take, request.Skip, cancellationToken);
        }
    }
}
