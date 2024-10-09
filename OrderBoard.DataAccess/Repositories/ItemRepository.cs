using AutoMapper.QueryableExtensions;
using AutoMapper;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.Domain.Entities;
using OrderBoard.Infrastructure.Repository;
using OrderBoard.Contracts.Items;
using Microsoft.EntityFrameworkCore;

namespace OrderBoard.DataAccess.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IRepository<Item, OrderBoardDbContext> _repository;
        private readonly IMapper _mapper;

        public ItemRepository(IRepository<Item, OrderBoardDbContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(Item model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }

        public Task<ItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<ItemInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public Task<ItemDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<ItemDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<Guid> UpdateAsync(Item model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model, cancellationToken);
            return model.Id;
        }
        public async Task DeleteByIdAsync(Item model, CancellationToken cancellationToken)
        {
            var result = _repository.DeleteAsync(model, cancellationToken);
            return;
        }
    }
}
