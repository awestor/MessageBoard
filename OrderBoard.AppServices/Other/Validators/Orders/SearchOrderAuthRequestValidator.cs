using FluentValidation;
using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.Orders.Requests;

namespace OrderBoard.AppServices.Other.Validators.Orders
{
    public class SearchOrderAuthRequestValidator : AbstractValidator<SearchOrderAuthRequest>
    {
        public SearchOrderAuthRequestValidator()
        {
            RuleFor(x => x.MaxOrderStatus).NotNull().IsInEnum().WithMessage("Некорректный статус");
            RuleFor(x => x.MinOrderStatus).NotNull().IsInEnum().WithMessage("Некорректный статус");
            RuleFor(x => x.Skip).NotNull().GreaterThan(0);
            RuleFor(x => x.Take).NotNull().GreaterThan(0);
            return;
        }
    }
}
