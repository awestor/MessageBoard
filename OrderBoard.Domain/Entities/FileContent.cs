using OrderBoard.Domain.Base;

namespace OrderBoard.Domain.Entities
{
    public class FileContent : BaseEntity
    {
        /// <summary>
        /// Название файла
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Дата создания файла
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Контент
        /// </summary>
        public byte[] Content { get; set; } = null!;

        /// <summary>
        /// Тип контента
        /// </summary>
        public string ContentType { get; set; } = null!;

        /// <summary>
        /// Размер файла
        /// </summary>
        public int Length { get; set; }
    }
}
