using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Domain.Entities;
using System.Linq.Expressions;

namespace OrderBoard.AppServices.Items.SpecificationContext.Specifications
{
    public class MaxPriceSpecification : Specification<Item>
    {
        private readonly decimal _maxPrice;

        /// <summary>
        /// Создаёт спецификацию отсечения по максимальной цене.
        /// </summary>
        /// <param name="maxPrice">Максимальная цена.</param>
        public MaxPriceSpecification(decimal maxPrice)
        {
            _maxPrice = maxPrice;
        }

        /// <inheritdoc />
        public override Expression<Func<Item, bool>> PredicateExpression =>
            advert => advert.Price <= _maxPrice;
    }
}