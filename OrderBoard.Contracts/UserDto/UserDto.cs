namespace OrderBoard.Contracts.UserDto
{
    internal class UserDto
    {
        /// <summary>
        /// Идентефикатор пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Логин профиля
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Описание профиля
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Дата создания профиля
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
