using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Files
{
    public class FileCreateModel
    {
        /// <summary>
        /// Название файла
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Контент
        /// </summary>
        public byte[] Content { get; set; } = null!;

        /// <summary>
        /// Тип контента
        /// </summary>
        public string ContentType { get; set; } = null!;
    }
}
