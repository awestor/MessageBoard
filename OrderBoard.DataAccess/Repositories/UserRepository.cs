using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderBoard.AppServices.Other.Specifications;
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

        public async Task<Guid?> AddAsync(EntUser model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }
        //------------------------- Методы получения по спецификации ----------------------------
        public async Task<List<UserInfoModel>> GetBySpecificationWithPaginationAsync(
            ISpecification<EntUser> specification, int take, int? skip, CancellationToken cancellationToken)
        {
            var query = _repository
                .GetAll()
                .OrderBy(item => item.Id)
                .Where(specification.PredicateExpression);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            return await query
                .Take(take)
                .ProjectTo<UserInfoModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<List<UserInfoModel>> GetBySpecificationAsync(ISpecification<EntUser> specification, CancellationToken cancellationToken)
        {
            return await _repository
                .GetByPredicate(specification.PredicateExpression)
                .ProjectTo<UserInfoModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<UserDataModel?> GetDataBySpecificationAsync(ISpecification<EntUser> specification, CancellationToken cancellationToken)
        {
            return await _repository
                .GetByPredicate(specification.PredicateExpression)
                .ProjectTo<UserDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<UserDataModel> GetDataByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<UserDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<Guid?> UpdateAsync(EntUser model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model, cancellationToken);
            return model.Id;
        }
        public Task DeleteAsync(EntUser model, CancellationToken cancellationToken)
        {
            _repository.DeleteAsync(model, cancellationToken);
            return Task.CompletedTask;
        }
        public async Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<EntUser>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            await _repository.DeleteAsync(model, cancellationToken);
            return;
        }



        //------------------------- Под перенос на спецификацию ----------------------------
        public Task<UserInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<UserInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<UserDataModel> GetByLoginOrEmailAndPasswordAsync(string login, string email, string password, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => (s.Login == login && s.Password == password) || (s.Email == email && s.Password == password))
                .ProjectTo<UserDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        
    }
}
