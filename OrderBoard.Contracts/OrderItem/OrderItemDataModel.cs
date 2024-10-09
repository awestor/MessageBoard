namespace OrderBoard.Contracts.OrderItem
{
    public class OrderItemDataModel
    {
        /// <summary>
        /// Идентефикатор поля заказа
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Id товара
        /// </summary>
        public Guid ItemId { get; set; }
        /// <summary>
        /// Id заказа
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// Количество товаров
        /// </summary>
        public decimal Count { get; set; }
        /// <summary>
        /// Итоговая цена за отдельный товар
        /// </summary>
        public decimal? OrderPrice { get; set; }
    }
}
