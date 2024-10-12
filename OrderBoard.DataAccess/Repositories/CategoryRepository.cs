using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.Infrastructure.Repository;
using OrderBoard.Contracts.Categories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OrderBoard.Domain.Entities;


namespace OrderBoard.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Category, OrderBoardDbContext> _repository;
        private readonly IMapper _mapper;

        public CategoryRepository(IRepository<Category, OrderBoardDbContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid?> AddAsync(Category model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }

        public Task<CategoryInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<CategoryInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public Task<CategoryDataModel> GetForUpdateAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<CategoryDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task UpdateAsync(Category model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model, cancellationToken);
            return;
        }
        public async Task DeleteByIdAsync(Category model, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(model, cancellationToken);
            return;
        }
    }
}
