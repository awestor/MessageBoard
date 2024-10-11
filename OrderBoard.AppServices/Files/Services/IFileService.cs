using Microsoft.AspNetCore.Http;
using OrderBoard.Contracts.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Files.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="file">Файл, отправленный с HTTP запросом.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор файла.</returns>
        Task<Guid> CreateAsync(IFormFile file, CancellationToken cancellationToken);

        /// <summary>
        /// Получение файла по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Объект передачи данных файла для скачивания.</returns>
        Task<FileDataModel?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение информации о файле по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Объект передачи данных информации о файле.</returns>
        Task<FileInfoModel?> GetFileInfoByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
