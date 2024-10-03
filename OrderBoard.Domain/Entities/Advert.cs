using OrderBoard.Domain.Base;

namespace OrderBoard.Domain.Entities
{
    public class Advert : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CreatedAt { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
