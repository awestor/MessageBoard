using OrderBoard.AppServices.OrderItems.SpecificationContext.Specifications;
using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.OrderItem.Requests;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.OrderItems.SpecificationContext.Builders
{
    public class OrderItemSpecificationBuilder : IOrderItemSpecificationBuilder
    {
        public ISpecification<OrderItem> Build(SearchOrderItemFromOrderRequest request)
        {
            var specification = Specification<OrderItem>.FromPredicate(orderItem => orderItem.OrderId == request.OrderId);

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

        public ISpecification<OrderItem> Build(Guid? itemId, Guid? orderId)
        {
            var specification = Specification<OrderItem>.FromPredicate(orderItem =>
            orderItem.OrderId == orderId);

            specification = specification.And(new ByItemIdSpecification(itemId));

            return specification;
        }
    }
}
