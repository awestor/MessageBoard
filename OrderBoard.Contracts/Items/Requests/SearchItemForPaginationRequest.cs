using OrderBoard.Contracts.BasePagination;

namespace OrderBoard.Contracts.Items.Requests
{
    public class SearchItemForPaginationRequest : BasePaginationRequest
    {
        /// <summary>
        /// Запрос для поиска
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Минимальная цена товаров, учавствующих в поиске
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Максимальная цена товаров, учавствующих в поиске
        /// </summary>
        public decimal? MaxPrice { get; set; }
    }
}
