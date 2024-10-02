using OrderBoard.Contracts.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Categories.Services
{
    public interface ICategoryService
    {
        Task<Guid> CreateAsync(CategoryCreateModel model, CancellationToken cancellationToken);
        Task<CategoryInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
