using AutoMapper.QueryableExtensions;
using AutoMapper;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.Domain.Entities;
using OrderBoard.Infrastructure.Repository;
using OrderBoard.Contracts.Items;
using Microsoft.EntityFrameworkCore;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.Contracts.OrderItem;

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

        public Task<ItemInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<ItemInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public Task<ItemDataModel> GetForUpdateAsync(Guid? id, CancellationToken cancellationToken)
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
        public async Task<Decimal?> GetPriceAsync(Guid? id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<ItemDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if (result == null)
            {
                throw new EntitiesNotFoundException("Товар не найден.");
            }
            return result.Price;
        }
        public async Task<List<ItemInfoModel>> GetAllItemAsync(Guid? id, CancellationToken cancellationToken)
        {
            List<ItemInfoModel> ItemList = [];
            int i = 0;
            while (true)
            {
                var temp = await _repository.GetAll().Where(s => s.UserId == id)
                    .ProjectTo<ItemInfoModel>(_mapper.ConfigurationProvider)
                    .Skip(i).FirstOrDefaultAsync(cancellationToken);
                if (temp != null) { ItemList.Add(temp); i++; }
                else break;
            }

            return ItemList;
        }
    }
}
