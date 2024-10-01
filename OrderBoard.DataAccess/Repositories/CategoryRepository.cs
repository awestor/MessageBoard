using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.Domain;
using OrderBoard.Infrastructure.Repository;

namespace OrderBoard.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Category, OrderBoardDbContext> _repository;

        public CategoryRepository(IRepository<Category, OrderBoardDbContext> repository)
        {
            _repository = repository;
        }
    }
}
