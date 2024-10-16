using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Categories.Requests
{
    public class DeleteCategoryRequest
    {
        /// <summary>
        /// Имя категории.
        /// </summary>
        [Required]
        public Guid? Id { get; set; }
        public Guid? NewCategoryId { get; set; }
        /// <summary>
        /// Имя категории.
        /// </summary>
        [Required]
        public Guid? NewItemId { get; set; }
    }
}
