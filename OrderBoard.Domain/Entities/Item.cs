using OrderBoard.Domain.Base;

namespace OrderBoard.Domain.Entities
{
    public class Item: BaseEntity
    {
        /// <summary>
        /// Имя товара
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Коментарий к товару
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Коментарий к товару
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
        /// Id пользователя что создал его
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Id заказа, за которым закреплён товар
        /// </summary>

    }
}
