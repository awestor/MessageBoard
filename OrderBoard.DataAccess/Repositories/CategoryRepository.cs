using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.Domain;
using OrderBoard.Infrastructure.Repository;
using OrderBoard.Contracts.Categories;
using Microsoft.EntityFrameworkCore;


namespace OrderBoard.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Category, OrderBoardDbContext> _repository;

        public CategoryRepository(IRepository<Category, OrderBoardDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddAsync(Category model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }

        public Task<CategoryInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .Select(s => new CategoryInfoModel 
                {
                    Id = s.Id,
                    Name = s.Name,
                    Created = s.Created,
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
