namespace OrderBoard.Contracts.Items
{
    /// <summary>
    /// Модель создания товара
    /// </summary>
    public class ItemCreateModel
    {
        /// <summary>
        /// Имя товара
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Количество товара
        /// </summary>
        public decimal? Count { get; set; }
        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// Описание товара
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Коментарий к товару
        /// </summary>
        public string? Comment { get; set; }
        /// <summary>
        /// Id категории за которой закреплён товар
        /// </summary>
        public Guid? CategoryId { get; set; }
    }
}
