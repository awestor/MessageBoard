using AutoMapper;
using Microsoft.AspNetCore.Http;
using OrderBoard.AppServices.Files.Repositories;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.Contracts.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Files.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public FileService(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        public async Task<Guid> CreateAsync(IFormFile file, CancellationToken cancellationToken)
        {
            var bytes = await GetPayloadAsync(file, cancellationToken);
            var fileCreateModel = new FileCreateModel
            {
                Name = file.FileName,
                ContentType = file.ContentType,
                Content = bytes
            };
            return await _fileRepository.CreateAsync(fileCreateModel, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<FileDataModel?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _fileRepository.GetFileByIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<FileInfoModel?> GetFileInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _fileRepository.GetFileInfoByIdAsync(id, cancellationToken);
        }

        private static async Task<byte[]> GetPayloadAsync(IFormFile file, CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);

            return memoryStream.ToArray();
        }
    }
}
