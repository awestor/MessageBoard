using OrderBoard.Contracts.BasePagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Categories.Requests
{
    /// <summary>
    /// Запрос для поиска категории с возможным идентефикатором родителя
    /// </summary>
    public class SearchCategoryRequest : BasePaginationRequest
    {
        public Guid? ParentId { get; set; }
    }
}
