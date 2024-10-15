using FluentValidation;
using OrderBoard.Contracts.Categories;
using OrderBoard.Contracts.Categories.Requests;

namespace OrderBoard.AppServices.Other.Validators.Categorys
{
    public class SearchCategoryValidator : AbstractValidator<SearchCategoryRequest>
    {
        public SearchCategoryValidator()
        {
            RuleFor(x => x.Skip).NotNull().GreaterThan(-1);
            RuleFor(x => x.Take).NotNull().GreaterThan(0);
        }
    }
}
