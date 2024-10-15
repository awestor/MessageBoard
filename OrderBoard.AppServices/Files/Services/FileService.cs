using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrderBoard.AppServices.Files.Repositories;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.AppServices.Other.Services;
using OrderBoard.Contracts.Files;


namespace OrderBoard.AppServices.Files.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IStructuralLoggingService _structuralLoggingService;

        public FileService(IFileRepository fileRepository, IMapper mapper,
            IStructuralLoggingService structuralLoggingService,
            ILogger logger)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _structuralLoggingService = structuralLoggingService;
            _logger = logger;
        }
        public async Task<Guid> CreateAsync(IFormFile file, CancellationToken cancellationToken)
        {
            if(file == null)
            {
                throw new EntititysNotVaildException("Просьба выбрать файлю");
            }
            var bytes = await GetPayloadAsync(file, cancellationToken);
            var fileCreateModel = new FileCreateModel
            {
                Name = file.FileName,
                ContentType = file.ContentType,
                Content = bytes
            };
            _structuralLoggingService.PushProperty("CreateRequest", fileCreateModel);
            _logger.LogInformation("Файл был создан.");
            return await _fileRepository.CreateAsync(fileCreateModel, cancellationToken);
        }

        public async Task<FileDataModel?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _fileRepository.GetFileByIdAsync(id, cancellationToken)
                ?? throw new EntitiesNotFoundException("Файл не был найден.");
            
            _structuralLoggingService.PushProperty("SerchRequest", result);
            _logger.LogInformation("Файл был отображён.");
            return result;
        }

        public async Task<FileInfoModel?> GetFileInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _fileRepository.GetFileInfoByIdAsync(id, cancellationToken)
                 ?? throw new EntitiesNotFoundException("Файл не был найден.");
            _structuralLoggingService.PushProperty("SerchInfoRequest", result);
            _logger.LogInformation("Информация о файле была отображена.");
            return result;
        }

        private async Task<byte[]> GetPayloadAsync(IFormFile file, CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);

            _structuralLoggingService.PushProperty("GetRequest", file);
            _logger.LogInformation("Полезная нагрузка файла была получена.");
            return memoryStream.ToArray();
        }
    }
}
