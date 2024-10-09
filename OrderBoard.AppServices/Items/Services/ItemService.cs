using AutoMapper;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.AppServices.Orders.Repository;
using OrderBoard.AppServices.Repository.Repository;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Items.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public Task<Guid> CreateAsync(ItemCreateModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ItemCreateModel, Item>(model);

            return _itemRepository.AddAsync(entity, cancellationToken);
        }

        public Task<ItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _itemRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<ItemDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            return _itemRepository.GetForUpdateAsync(id, cancellationToken);
        }
        public Task<Guid> UpdateAsync(ItemDataModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ItemDataModel, Item>(model);
            return _itemRepository.UpdateAsync(entity, cancellationToken);
        }
        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = await _itemRepository.GetForUpdateAsync(id, cancellationToken);
            var entity = _mapper.Map<ItemDataModel, Item>(model);
            _itemRepository.DeleteByIdAsync(entity, cancellationToken);
            return;
        }
    }
}
