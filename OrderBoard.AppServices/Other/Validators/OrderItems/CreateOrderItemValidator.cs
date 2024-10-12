using FluentValidation;
using OrderBoard.Contracts.OrderItem;

namespace OrderBoard.AppServices.Other.Validators.OrderItems
{
    public class CreateOrderItemValidator : AbstractValidator<OrderItemCreateModel>
    {
        public CreateOrderItemValidator()
        {
            RuleFor(x => x.ItemId).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Count).NotNull().GreaterThan(0);
            return;
        }
    }
}
