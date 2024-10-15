using FluentValidation;
using OrderBoard.Contracts.Items.Requests;


namespace OrderBoard.AppServices.Other.Validators.Items
{
    public class SearchItemForPaginationValidator : AbstractValidator<SearchItemForPaginationRequest>
    {
        public SearchItemForPaginationValidator()
        {
            RuleFor(x => x.CategoryId).NotNull().NotEqual(Guid.Empty).WithMessage("Некорректная идентефикатор категории");
            RuleFor(x => x.MinPrice).NotNull().GreaterThan(-1).WithMessage("Некорректная цена");
            RuleFor(x => x.MaxPrice).NotNull().GreaterThan(0).WithMessage("Некорректная цена");
            RuleFor(x => x.Skip).NotNull().GreaterThan(-1);
            RuleFor(x => x.Take).NotNull().GreaterThan(0);
        }
    }
}
