
namespace OrderBoard.Contracts.OrderItem
{
    /// <summary>
    /// Информационная модель полей заказа
    /// </summary>
    public class OrderItemInfoModel
    {
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
