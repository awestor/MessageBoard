using FluentValidation;
using OrderBoard.Contracts.Orders.Requests;

namespace OrderBoard.AppServices.Other.Validators.Orders
{
    public class SearchOrderRequestValidator : AbstractValidator<SearchOrderRequest>
    {
        public SearchOrderRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEqual(Guid.Empty).WithMessage("Некорректный идентефикатор пользователя");
            RuleFor(x => x.MaxOrderStatus).NotNull().IsInEnum().WithMessage("Некорректный статус");
            RuleFor(x => x.MinOrderStatus).NotNull().IsInEnum().WithMessage("Некорректный статус");
            RuleFor(x => x.Skip).NotNull().GreaterThan(-1);
            RuleFor(x => x.Take).NotNull().GreaterThan(0);
            return;
        }
    }
}
