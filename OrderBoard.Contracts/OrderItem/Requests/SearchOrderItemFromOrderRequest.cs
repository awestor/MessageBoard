using OrderBoard.Contracts.BasePagination;

namespace OrderBoard.Contracts.OrderItem.Requests
{
    public class SearchOrderItemFromOrderRequest : BasePaginationRequest
    {
        /// <summary>
        /// Id заказа по которому осуществляется поиск
        /// </summary>
        public Guid? OrderId { get; set; }

        /// <summary>
        /// Минимальная цена полей заказа, учавствующих в поиске
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Максимальная цена полей заказа, учавствующих в поиске
        /// </summary>
        public decimal? MaxPrice { get; set; }
    }
}
