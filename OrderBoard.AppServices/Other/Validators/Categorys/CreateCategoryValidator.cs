using FluentValidation;
using OrderBoard.Contracts.Categories;

namespace OrderBoard.AppServices.Other.Validators.Categorys
{
    public class CreateCategoryValidator : AbstractValidator<CategoryCreateModel>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Title).NotNull().MinimumLength(3);
        }
    }
}
