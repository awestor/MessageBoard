using OrderBoard.Contracts.BasePagination;
using OrderBoard.Contracts.Categories;
using OrderBoard.Contracts.Categories.Requests;

namespace OrderBoard.AppServices.Categories.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Создание сущности.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор сохранённой сущности.</returns>
        Task<Guid?> CreateAsync(CategoryCreateModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Получить модель категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель категории.</returns>
        Task<CategoryInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken);
        Task<CategoryInfoModel> GetByNameAsync(SearchCategoryByNameRequest? name, CancellationToken cancellationToken);
        Task<List<CategoryInfoModel>> GetAllByRequestAsync(SearchCategoryRequest request, CancellationToken cancellationToken);
        Task<Guid?> UpdateAsync(CategoryDataModel model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
