using OrderBoard.Contracts.Categories;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Categories.Repositories
{
    public interface ICategoryRepository
    {
        Task<Guid> AddAsync(Category model, CancellationToken cancellationToken);
        Task<CategoryInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
