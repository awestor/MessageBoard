using OrderBoard.AppServices.Adverts.Repositories;
using OrderBoard.Domain.Entities;
using OrderBoard.Infrastructure.Repository;

namespace OrderBoard.DataAccess.Repositories
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly IRepository<Advert, OrderBoardDbContext> _repository;

        public AdvertRepository(IRepository<Advert, OrderBoardDbContext> repository)
        {
            _repository = repository;
        }
    }
}
