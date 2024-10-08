using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Items.Repositories
{
    public interface IItemRepository
    {
        Task<Guid> AddAsync(Item model, CancellationToken cancellationToken);
        Task<ItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ItemDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> UpdateAsync(Item model, CancellationToken cancellationToken);
    }
}
