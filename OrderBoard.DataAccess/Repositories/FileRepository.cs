using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderBoard.AppServices.Files.Repositories;
using OrderBoard.Contracts.Files;
using OrderBoard.Domain.Entities;
using OrderBoard.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.DataAccess.Repositories
{
    public class FileRepository: IFileRepository
    {
        private readonly IRepository<FileContent, OrderBoardDbContext> _repository;
        private readonly IMapper _mapper;

        public FileRepository(IRepository<FileContent, OrderBoardDbContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(FileCreateModel fileCreateModel, CancellationToken cancellationToken)
        {
            var fileContent = _mapper.Map<FileCreateModel, FileContent>(fileCreateModel);
            await _repository.AddAsync(fileContent, cancellationToken);

            return fileContent.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<FileDataModel?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetAll()
                .Where(x => x.Id == id)
                .ProjectTo<FileDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<FileInfoModel?> GetFileInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetAll()
                .Where(x => x.Id == id)
                .ProjectTo<FileInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
