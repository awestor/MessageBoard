namespace OrderBoard.Contracts.OrderItem
{
    /// <summary>
    /// Модель создания полей заказа
    /// </summary>
    public class OrderItemCreateModel
    {
        /// <summary>
        /// Id пользователя для создания Order
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// Id товара
        /// </summary>
        public Guid ItemId { get; set; }
        /// <summary>
        /// Количество товаров
        /// </summary>
        public decimal Count { get; set; }
    }
}
