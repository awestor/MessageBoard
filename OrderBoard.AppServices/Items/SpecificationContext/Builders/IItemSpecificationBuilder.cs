using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.Items.Requests;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Items.SpecificationContext.Builders
{
    public interface IItemSpecificationBuilder
    {
        /// <summary>
        /// Строит спецификацию по запросу.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Спецификация.</returns>
        ISpecification<Item> Build(SearchItemForPaginationRequest request, Guid userId);

        /// <summary>
        /// Строит спецификацию по категории.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <returns>Спецификация.</returns>
        ISpecification<Item> Build(Guid? categoryId);
    }
}
