using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.Orders;

namespace OrderBoard.AppServices.Items.Services
{
    public interface IItemService
    {
        Task<Guid> CreateAsync(ItemCreateModel model, CancellationToken cancellationToken);
        Task<ItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ItemDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> UpdateAsync(ItemDataModel model, CancellationToken cancellationToken);
    }
}
