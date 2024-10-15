namespace OrderBoard.Contracts.Categories
{
    /// <summary>
    /// Модель создания категории.
    /// </summary>
    public class CategoryCreateModel
    {
        /// <summary>
        /// Имя категории.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
