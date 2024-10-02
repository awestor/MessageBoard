using OrderBoard.AppServices.Adverts.Repositories;
using OrderBoard.Domain;
using OrderBoard.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
