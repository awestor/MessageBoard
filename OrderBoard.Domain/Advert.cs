namespace OrderBoard.Domain
{
    public class Advert
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CreatedAt { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
