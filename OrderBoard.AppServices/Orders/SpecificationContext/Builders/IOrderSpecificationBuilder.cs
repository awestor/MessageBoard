using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.Orders.Requests;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Orders.SpecificationContext.Builders
{
    public interface IOrderSpecificationBuilder
    {
        /// <summary>
        /// Строит спецификацию по запросу.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Спецификация.</returns>
        ISpecification<Order> Build(SearchOrderRequest request);
        /// <summary>
        /// Строит спецификацию по запросу.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Спецификация.</returns>
        //ISpecification<Order> Build(SearchOrderAuthRequest request, Guid UserId);
    }
}
