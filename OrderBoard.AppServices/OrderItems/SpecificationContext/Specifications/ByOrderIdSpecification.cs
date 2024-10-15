using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Domain.Entities;
using System.Linq.Expressions;

namespace OrderBoard.AppServices.OrderItems.SpecificationContext.Specifications
{
    public class ByOrderIdSpecification : Specification<OrderItem>
    {
        private readonly Guid? _orderId;

        public ByOrderIdSpecification(Guid? orderId)
        {
            _orderId = orderId;
        }

        /// <inheritdoc />
        public override Expression<Func<OrderItem, bool>> PredicateExpression =>
            orderItem => orderItem.OrderId == _orderId;
    }
}
