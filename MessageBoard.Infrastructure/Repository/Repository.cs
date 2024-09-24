namespace MessageBoard.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Возврат всех элементов
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<TEntity> getAll()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Возврат значения по предикату
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<TEntity> getByPredicate(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Добавление элемента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<TEntity> Add(TEntity model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Обноваление элемента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<TEntity> Update(TEntity model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<TEntity> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
