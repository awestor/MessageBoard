﻿using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.Categories;
using OrderBoard.Domain.Entities;
using System.Threading;

namespace OrderBoard.AppServices.Categories.Repositories
{
    /// <summary>
    /// Репозиторий для работы с категориями
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Добавить категорию в БД.
        /// </summary>
        /// <param name="model">Доменная сущность (доменная модель) категории.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор добавленной категории.</returns>
        Task<Guid?> AddAsync(Category model, CancellationToken cancellationToken);
        /// <summary>
        /// Получить модель категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель категории.</returns>
        //Task<CategoryInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken);
        Task<CategoryDataModel> GetDataByIdAsync(Guid? id, CancellationToken cancellationToken);
        Task<List<CategoryDataModel>> GetAllChildDataByIdAsync(Guid? id, CancellationToken cancellationToken);
        Task UpdateAsync(Category model, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Category model, CancellationToken cancellationToken);
        Task<CategoryInfoModel> GetBySpecificationAsync(ISpecification<Category> specification,
            CancellationToken cancellationToken);
        Task<List<CategoryInfoModel>> GetBySpecificationWithPaginationAsync(ISpecification<Category> specification,
            int Take, int? Skip, CancellationToken cancellationToken);
    }
}
