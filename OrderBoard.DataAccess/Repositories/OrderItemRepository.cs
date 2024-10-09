using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderBoard.AppServices.Repository.Repository;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.Orders;
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

        public async Task<Guid> AddAsync(OrderItem model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }

        public async Task<List<OrderItemInfoModel>> GetAllByOrderIdAsync(Guid id, CancellationToken cancellationToken)
        {
            List<OrderItemInfoModel> tempOrderItemList = new List<OrderItemInfoModel>();
            int i = 0;
            while (true) {
                var temp = await _repository.GetAll().Where(s => s.OrderId == id)
                    .ProjectTo<OrderItemInfoModel>(_mapper.ConfigurationProvider)
                    .Skip(i).FirstOrDefaultAsync(cancellationToken);
                if (temp != null) {tempOrderItemList.Add(temp); i++; }
                else break;
            }
            
            return tempOrderItemList;
        }

        public Task<OrderItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<OrderItemInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public Task<OrderItemDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<OrderItemDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<Guid> UpdateAsync(OrderItem model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model, cancellationToken);
            return model.Id;
        }
        public async Task DeleteByIdAsync(OrderItem model, CancellationToken cancellationToken)
        {
            var result = _repository.DeleteAsync(model, cancellationToken);
            return;
        }
    }
}
