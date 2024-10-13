using OrderBoard.AppServices.Orders.SpecificationContext.Specifications;
using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.Orders.Requests;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Orders.SpecificationContext.Builders
{
    public class OrderSpecificationBuilder : IOrderSpecificationBuilder
    {
        public ISpecification<Order> Build(SearchOrderRequest request)
        {
            var specification = Specification<Order>.FromPredicate(order => order.UserId == request.UserId);

            if (request.MinOrderStatus.HasValue)
            {
                specification = specification.And(new MinStatusSpecification(request.MinOrderStatus.Value));
            }

            if (request.MaxOrderStatus.HasValue)
            {
                specification = specification.And(new MaxStatusSpecification(request.MaxOrderStatus.Value));
            }

            return specification;
        }
        /*public ISpecification<Order> Build(SearchOrderAuthRequest request, Guid userId)
        {
            var specification = Specification<Order>.FromPredicate(order => order.UserId == userId);

            if (request.MinOrderStatus.HasValue)
            {
                specification = specification.And(new MinStatusSpecification(request.MinOrderStatus.Value));
            }

            if (request.MaxOrderStatus.HasValue)
            {
                specification = specification.And(new MaxStatusSpecification(request.MaxOrderStatus.Value));
            }

            return specification;
        }*/
    }
}
