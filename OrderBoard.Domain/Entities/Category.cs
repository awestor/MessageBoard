using OrderBoard.Domain.Base;

namespace OrderBoard.Domain.Entities
{
    public class Category:BaseEntity
    {
        public Guid? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        /// <summary>
        /// Id хранимых в данной категории "item"
        /// </summary>
        public virtual List<Item>? Items { get; set; }
    }
}
