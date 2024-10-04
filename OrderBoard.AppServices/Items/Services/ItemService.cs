using AutoMapper;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.Contracts.Items;
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
    }
}
