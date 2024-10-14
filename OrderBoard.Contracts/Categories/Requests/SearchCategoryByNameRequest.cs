using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Categories.Requests
{
    public class SearchCategoryByNameRequest
    {
        /// <summary>
        /// Имя категории.
        /// </summary>
        [Required]
        public string? Name { get; set; }
    }
}
