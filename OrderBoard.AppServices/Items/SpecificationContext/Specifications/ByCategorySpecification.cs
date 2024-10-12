using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Domain.Entities;
using System.Linq.Expressions;

namespace OrderBoard.AppServices.Items.SpecificationContext.Specifications
{
    public class ByCategorySpecification : Specification<Item>
    {
        private readonly Guid? _categoryId;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ByCategorySpecification"/>.
        /// </summary>
        public ByCategorySpecification(Guid? categoryId)
        {
            _categoryId = categoryId;
        }

        /// <inheritdoc />
        public override Expression<Func<Item, bool>> PredicateExpression => advert =>
            advert.CategoryId == _categoryId;
    }
}