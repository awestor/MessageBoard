namespace OrderBoard.Contracts.UserDto
{
    internal class UserRegisterRequestDto
    {
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
        /// <summary>
        /// Пароль
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }
    }
}
