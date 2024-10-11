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
        /// <summary>
        /// Создание сущности.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор сохранённой сущности.</returns>
        Task<Guid> CreateAsync(CategoryCreateModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Получить модель категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель категории.</returns>
        Task<CategoryInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
