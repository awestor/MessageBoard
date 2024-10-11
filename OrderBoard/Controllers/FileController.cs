using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Exceptions;
using OrderBoard.AppServices.Files.Services;
using OrderBoard.Contracts.Files;
using System.Net;

namespace OrderBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController(IFileService fileService) : ControllerBase
    {
        private readonly IFileService _fileService = fileService;

        /// <summary>
        /// Загрузка одного файла в систему
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор файла.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CreateAsync(IFormFile file, CancellationToken cancellationToken)
        {
            var fileId = await _fileService.CreateAsync(file, cancellationToken);

            return StatusCode((int)HttpStatusCode.Created, fileId);
        }

        /// <summary>
        /// Скачивание файла из базы по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Файл для скачивания.</returns>
        [HttpGet("download/{id}")]
        [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Nullable), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            var file = await _fileService.GetFileByIdAsync(id, cancellationToken);

            if(file == null)
            {
                throw new EntitiesNotFoundException("Файл не найден.");
            }
            Response.ContentLength = file?.Content.Length;

            return file is not null ? File(file.Content, file.ContentType, file.Name) : NoContent();
        }

        /// <summary>
        /// Получение информации о файле по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Объект передачи данных информации о файле.</returns>
        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(FileInfoModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Nullable), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetFileInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var fileInfo = await _fileService.GetFileInfoByIdAsync(id, cancellationToken);
            if (fileInfo == null)
            {
                throw new EntitiesNotFoundException("Файл не найден.");
            }

            return fileInfo is not null ? Ok(fileInfo) : NoContent();
        }
    }
}
