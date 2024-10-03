﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderBoard.AppServices.Users.Repository;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Domain.Entities;
using OrderBoard.Infrastructure.Repository;


namespace OrderBoard.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<EntUser, OrderBoardDbContext> _repository;
        private readonly IMapper _mapper;

        public UserRepository(IRepository<EntUser, OrderBoardDbContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(EntUser model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }

        public Task<UserInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<UserInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
