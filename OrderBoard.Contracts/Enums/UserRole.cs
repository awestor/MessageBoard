namespace OrderBoard.Contracts.Enums
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Не авторизован
        /// </summary>
        NotAuthorized = 0,
        /// <summary>
        /// Авторизован
        /// </summary>
        Authorized = 1,
        /// <summary>
        /// Админ
        /// </summary>
        Admin = 2,
        /// <summary>
        /// Удалён
        /// </summary>
        Deleted = 3
    }
}
