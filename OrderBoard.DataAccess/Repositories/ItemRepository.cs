using AutoMapper.QueryableExtensions;
using AutoMapper;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.Domain.Entities;
using OrderBoard.Infrastructure.Repository;
using OrderBoard.Contracts.Items;
using Microsoft.EntityFrameworkCore;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.AppServices.Other.Specifications;

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

        public async Task<Guid?> AddAsync(Item model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }

        public async Task<List<ItemInfoModel>> GetBySpecificationWithPaginationAsync(
            ISpecification<Item> specification, int take, int? skip, CancellationToken cancellationToken)
        {
            var query = _repository
                .GetAll()
                .OrderBy(item => item.Id)
                .Where(specification.PredicateExpression);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            return await query
                .Take(take)
                .ProjectTo<ItemInfoModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<List<ItemInfoModel>> GetBySpecificationAsync(ISpecification<Item> specification, CancellationToken cancellationToken)
        {
            return await _repository
                .GetByPredicate(specification.PredicateExpression)
                .ProjectTo<ItemInfoModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<Guid?> UpdateAsync(Item model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model, cancellationToken);
            return model.Id;
        }
        public Task DeleteAsync(Item model, CancellationToken cancellationToken)
        {
            _repository.DeleteAsync(model, cancellationToken);
            return Task.CompletedTask;
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
        public async Task<List<ItemDataModel>> GetAllItemAsync(Guid? id, CancellationToken cancellationToken)
        {
            var ItemList = await _repository.GetAll().Where(s => s.UserId == id)
                .ProjectTo<ItemDataModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return ItemList;
        }
    }
}
