using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OrderBoard.Infrastructure.Repository
{
    public interface IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        /// <summary>
        /// Возврат всех элементов
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// Возврат значения по предикату
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Получение по идентификатору
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Добавление элемента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddAsync(TEntity model, CancellationToken cancellationToken);
        /// <summary>
        /// Обновление элемента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity model, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity model, CancellationToken cancellationToken);
    }
}
