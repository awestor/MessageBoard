using OrderBoard.Contracts.Items;

namespace OrderBoard.AppServices.Items.Services
{
    public interface IItemService
    {
        Task<Guid> CreateAsync(ItemCreateModel model, CancellationToken cancellationToken);
        Task<ItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
