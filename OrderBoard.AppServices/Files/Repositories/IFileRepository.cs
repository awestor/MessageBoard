using OrderBoard.Contracts.Files;

namespace OrderBoard.AppServices.Files.Repositories
{
    /// <summary>
    /// Репозиторий для работы с файлами
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="fileContentDto">Объект передачи данных файла.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор файла.</returns>
        Task<Guid> CreateAsync(FileCreateModel file, CancellationToken cancellationToken);

        /// <summary>
        /// Получение одного файла по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Объект передачи данных файла для скачивания.</returns>
        Task<FileDataModel?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение информации о файле по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Объект передачи данных информации о файле.</returns>
        Task<FileInfoModel?> GetFileInfoByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
