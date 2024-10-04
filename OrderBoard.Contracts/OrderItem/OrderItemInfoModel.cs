namespace OrderBoard.Contracts.OrderItem
{
    public class OrderItemInfoModel
    {
        /// <summary>
        /// Id товара
        /// </summary>
        public Guid ItemId { get; set; }
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
