using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Domain.Entities;
using System.Linq.Expressions;

namespace OrderBoard.AppServices.OrderItems.SpecificationContext.Specifications
{
    public class ByItemIdSpecification : Specification<OrderItem>
    {
        private readonly Guid? _itemId;

        public ByItemIdSpecification(Guid? itemId)
        {
            _itemId = itemId;
        }

        /// <inheritdoc />
        public override Expression<Func<OrderItem, bool>> PredicateExpression =>
            orderItem => orderItem.ItemId == _itemId;
    }
}
