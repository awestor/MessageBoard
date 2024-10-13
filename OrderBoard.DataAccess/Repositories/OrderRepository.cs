using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderBoard.AppServices.Orders.Repository;
using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;
using OrderBoard.Infrastructure.Repository;


namespace OrderBoard.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IRepository<Order, OrderBoardDbContext> _repository;
        private readonly IMapper _mapper;

        public OrderRepository(IRepository<Order, OrderBoardDbContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid?> AddAsync(Order model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }

        public async Task<List<OrderDataModel>> GetBySpecificationWithPaginationAsync(
            ISpecification<Order> specification, int take, int? skip, CancellationToken cancellationToken)
        {
            var query = _repository
                .GetAll()
                .OrderBy(order => order.Id)
                .Where(specification.PredicateExpression);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            return await query
                .Take(take)
                .ProjectTo<OrderDataModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<List<OrderInfoModel>> GetBySpecificationAsync(ISpecification<Order> specification, CancellationToken cancellationToken)
        {
            return await _repository
                .GetByPredicate(specification.PredicateExpression)
                .ProjectTo<OrderInfoModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<Guid?> UpdateAsync(Order model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model, cancellationToken);
            return model.Id;
        }
        public Task DeleteByModelAsync(Order model, CancellationToken cancellationToken)
        {
            _repository.DeleteAsync(model, cancellationToken);
            return Task.CompletedTask;
        }




        //------------------------- Под перенос на спецификацию ----------------------------
        public Task<OrderInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<OrderInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public Task<OrderDataModel> GetForUpdateAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<OrderDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public Task<OrderDataModel> GetByUserIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => (s.UserId == id) && (s.OrderStatus != Contracts.Enums.OrderStatus.Ordered))
                .OrderBy(orderItem => orderItem.CreatedAt)
                .ProjectTo<OrderDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
