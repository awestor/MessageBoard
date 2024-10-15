using OrderBoard.Domain.Base;

namespace OrderBoard.Domain.Entities
{
    /// <summary>
    /// Сущность категории
    /// </summary>
    public class Category:BaseEntity
    {
        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Id хранимых в данной категории "item"
        /// </summary>
        public virtual List<Item>? Items { get; set; }
    }
}
