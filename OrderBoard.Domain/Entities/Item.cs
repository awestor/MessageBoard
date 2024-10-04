using OrderBoard.Domain.Base;

namespace OrderBoard.Domain.Entities
{
    public class Item: BaseEntity
    {
        /// <summary>
        /// Имя товара
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Описание товара
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Стоимость товара
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Коментарий к товару
        /// </summary>
        public string? Comment { get; set; }
        /// <summary>
        /// Количество товара
        /// </summary>
        public int Count { get; set; } 
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Id категории за которой закреплён товар
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// Категория за которой закреплён товар
        /// </summary>
        public Category? Category { get; set; }
        /// <summary>
        /// Id пользователя что создал его
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Пользователь за которым закреплён товар
        /// </summary>
        public EntUser? User { get; set; }
        /// <summary>
        /// Id заказа, за которым закреплён товар
        /// </summary>
        public virtual List<OrderItem>? OrderItem { get; set; }
    }
}
