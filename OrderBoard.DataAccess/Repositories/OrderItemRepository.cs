using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.AppServices.Repository.Repository;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Domain.Entities;
using OrderBoard.Infrastructure.Repository;

namespace OrderBoard.DataAccess.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IRepository<OrderItem, OrderBoardDbContext> _repository;
        private readonly IMapper _mapper;

        public OrderItemRepository(IRepository<OrderItem, OrderBoardDbContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid?> AddAsync(OrderItem model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }
        //------------------------- Методы получения по спецификации ----------------------------
        public async Task<List<OrderItemInfoModel>> GetBySpecificationWithPaginationAsync(
            ISpecification<OrderItem> specification, int take, int? skip, CancellationToken cancellationToken)
        {
            var query = _repository
                .GetAll()
                .OrderBy(orderItem => orderItem.Id)
                .Where(specification.PredicateExpression);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            return await query
                .Take(take)
                .ProjectTo<OrderItemInfoModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<OrderItemDataModel?> GetDataBySpecificationAsync(ISpecification<OrderItem> specification,
            CancellationToken cancellationToken)
        {
            return await _repository
                .GetByPredicate(specification.PredicateExpression)
                .ProjectTo<OrderItemDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        //------------------------- Методы для получения вхех полей ----------------------------
        public async Task<List<OrderItemInfoModel>> GetAllInfoByOrderIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(s => s.OrderId == id)
                .OrderBy(s => s.Id)
                .ProjectTo<OrderItemInfoModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        }
        public async Task<List<OrderItemDataModel>> GetAllDataByOrderIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(s => s.OrderId == id)
                .OrderBy(s => s.Id)
                .ProjectTo<OrderItemDataModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        //------------------------- Методы для получения одного поля ----------------------------
        public Task<OrderItemInfoModel?> GetInfoByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<OrderItemInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public Task<OrderItemDataModel?> GetDataByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<OrderItemDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        //------------------------- Методы для обновления и удаления ----------------------------
        public async Task<Guid?> UpdateAsync(OrderItem model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model, cancellationToken);
            return model.Id;
        }
        public Task DeleteAsync(OrderItem model, CancellationToken cancellationToken)
        {
            _repository.DeleteAsync(model, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
