namespace MessageBoard.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Возврат всех элементов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> getAll();
        /// <summary>
        /// Возврат значения по предикату
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> getByPredicate(Func<TEntity, bool> predicate);
        /// <summary>
        /// Добавление элемента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Add(TEntity model);
        /// <summary>
        /// Обновление элемента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Update(TEntity model);
        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Delete(Guid id);
    }
}
