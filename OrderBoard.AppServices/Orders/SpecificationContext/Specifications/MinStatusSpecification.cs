using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.Enums;
using OrderBoard.Domain.Entities;
using System.Linq.Expressions;


namespace OrderBoard.AppServices.Orders.SpecificationContext.Specifications
{
    public class MinStatusSpecification : Specification<Order>
    {
        private readonly OrderStatus _orderStatus;

        /// <summary>
        /// Создаёт спецификацию отсечения по минимальной цене.
        /// </summary>
        /// <param name="minPrice">Минимальная цена.</param>
        public MinStatusSpecification(OrderStatus orderStatus)
        {
            _orderStatus = orderStatus;
        }

        /// <inheritdoc />
        public override Expression<Func<Order, bool>> PredicateExpression =>
            orderItem => orderItem.OrderStatus >= _orderStatus;
    }
}
