using OrderBoard.AppServices.Items.SpecificationContext.Specifications;
using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.Items.Requests;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Items.SpecificationContext.Builders
{
    public class ItemSpecificationBuilder : IItemSpecificationBuilder
    {
        public ISpecification<Item> Build(SearchItemForPaginationRequest request)
        {
            var specification = Specification<Item>.FromPredicate(item => item.CategoryId == request.CategoryId);

            if (request.MinPrice.HasValue)
            {
                specification = specification.And(new MinPriceSpecification(request.MinPrice.Value));
            }

            if (request.MaxPrice.HasValue)
            {
                specification = specification.And(new MaxPriceSpecification(request.MaxPrice.Value));
            }
            return specification;
        }
    }
}
