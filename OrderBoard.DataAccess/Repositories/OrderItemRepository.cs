using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Guid> AddAsync(OrderItem model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }

        public Task<OrderItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<OrderItemInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
