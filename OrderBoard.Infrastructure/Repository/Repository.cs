using Microsoft.EntityFrameworkCore;
using OrderBoard.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace OrderBoard.Infrastructure.Repository
{
    public class Repository<TEntity, TContext> : IRepository <TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        protected TContext DbContext;
        protected DbSet<TEntity> DbSet;

        public Repository(TContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }
        /// <summary>
        /// Возврат всех элементов
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }
        /// <summary>
        /// Возврат значения по предикату
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        IQueryable<TEntity> GetByPredicate(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Получение по идентификатору
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await DbSet.FindAsync(id, cancellationToken);
        }
        /// <summary>
        /// Добавление элемента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AddAsync(TEntity model, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(model, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// Обноваление элемента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task UpdateAsync(TEntity model, CancellationToken cancellationToken)
        {
            DbSet.Update(model);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteAsync(TEntity model, CancellationToken cancellationToken)
        {
            if (model != null)
            {
                DbSet.Remove(model);
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }
        IQueryable<TEntity> IRepository<TEntity, TContext>.GetAll()
        {
            return DbSet;
        }
        IQueryable<TEntity> IRepository<TEntity, TContext>.GetByPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }
    }
}
