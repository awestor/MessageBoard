using OrderBoard.Contracts.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Categories.Repositories
{
    public interface ICategoryRepository
    {
        Task<Guid> AddAsync(Category model, CancellationToken cancellationToken);
        Task<CategoryInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
