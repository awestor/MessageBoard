using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Domain.Entities;
using System.Linq.Expressions;

namespace OrderBoard.AppServices.OrderItems.SpecificationContext.Specifications
{
    public class MinPriceSpecification : Specification<OrderItem>
    {
        private readonly decimal _minPrice;

        /// <summary>
        /// Создаёт спецификацию отсечения по минимальной цене.
        /// </summary>
        /// <param name="minPrice">Минимальная цена.</param>
        public MinPriceSpecification(decimal minPrice)
        {
            _minPrice = minPrice;
        }

        /// <inheritdoc />
        public override Expression<Func<OrderItem, bool>> PredicateExpression =>
            orderItem => orderItem.OrderPrice >= _minPrice;
    }
}