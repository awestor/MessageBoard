using OrderBoard.Domain.Base;

namespace OrderBoard.Domain.Entities
{
    public class OrderItem: BaseEntity
    {
        /// <summary>
        /// Id товара
        /// </summary>
        public Guid ItemId { get; set; }
        /// <summary>
        /// Сущность товара
        /// </summary>
        public Item? Item { get; set; }
        /// <summary>
        /// Id заказа
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// Сущность заказа
        /// </summary>
        public Order? Order { get; set; }
        /// <summary>
        /// Количество товаров
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Итоговая цена за отдельный товар
        /// </summary>
        public decimal OrderPrice { get; set; }
    }
}
