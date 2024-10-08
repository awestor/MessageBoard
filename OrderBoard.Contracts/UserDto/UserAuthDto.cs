namespace OrderBoard.Contracts.UserDto
{
    public class UserAuthDto
    {
        /// <summary>
        /// Логин профиля
        /// </summary>
        public string? Login { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public required string? Password { get; set; }
        
    }
}
