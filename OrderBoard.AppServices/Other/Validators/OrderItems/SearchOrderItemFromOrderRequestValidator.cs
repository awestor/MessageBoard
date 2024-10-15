using FluentValidation;
using OrderBoard.Contracts.OrderItem.Requests;


namespace OrderBoard.AppServices.Other.Validators.OrderItems
{
    public class SearchOrderItemFromOrderRequestValidator : AbstractValidator<SearchOrderItemFromOrderRequest>
    {
        public SearchOrderItemFromOrderRequestValidator()
        {
            RuleFor(x => x.OrderId).NotNull().NotEqual(Guid.Empty).WithMessage("Некорректный идентефикатор заказа");
            RuleFor(x => x.MinPrice).NotNull().GreaterThan(-1).WithMessage("Некорректная цена"); ;
            RuleFor(x => x.MaxPrice).NotNull().GreaterThan(0).WithMessage("Некорректная цена"); ;
            RuleFor(x => x.Skip).NotNull().GreaterThan(-1);
            RuleFor(x => x.Take).NotNull().GreaterThan(0);
            return;
        }
    }
}
