using OrderBoard.Contracts.BasePagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Items.Requests
{
    public class SearchItemByUserIdRequest : BasePaginationRequest
    {
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
