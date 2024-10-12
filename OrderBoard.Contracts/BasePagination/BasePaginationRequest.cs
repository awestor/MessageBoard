using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.BasePagination
{
    /// <summary>
    /// Базовый запрос с пагинацией ответа.
    /// </summary>
    public class BasePaginationRequest
    {
        /// <summary>
        /// Кол-во элементов для получения.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Кол-во элементов для пропуска перед получением.
        /// </summary>
        public int? Skip { get; set; }
    }
}
