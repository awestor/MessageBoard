using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.Items.Requests;
using OrderBoard.Contracts.OrderItem.Requests;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.OrderItems.SpecificationContext.Builders
{
    public interface IOrderItemSpecificationBuilder
    {
        /// <summary>
        /// Строит спецификацию по запросу.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Спецификация.</returns>
        ISpecification<OrderItem> Build(SearchOrderItemFromOrderRequest request);


        ISpecification<OrderItem> Build(Guid? itemId, Guid? orderId);
    }
}
