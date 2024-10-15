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
        ISpecification<Item> Build(SearchItemForPaginationRequest request);
        ISpecification<Item> Build(SearchItemByNameRequest request);
        ISpecification<Item> Build(Guid userId, SearchItemByUserIdRequest request);

    }
}
