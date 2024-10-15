using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Domain.Entities;
using System.Linq.Expressions;

namespace OrderBoard.AppServices.Items.SpecificationContext.Specifications
{
    public class MinPriceSpecification : Specification<Item>
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
        public override Expression<Func<Item, bool>> PredicateExpression =>
            advert => advert.Price >= _minPrice;
    }
}