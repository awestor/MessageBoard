using OrderBoard.Contracts.Items;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Items.Repositories
{
    public interface IItemRepository
    {
        Task<Guid> AddAsync(Item model, CancellationToken cancellationToken);
        Task<ItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
