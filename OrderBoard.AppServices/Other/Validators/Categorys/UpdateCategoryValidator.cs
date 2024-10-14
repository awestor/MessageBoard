using FluentValidation;
using OrderBoard.Contracts.Categories;

namespace OrderBoard.AppServices.Other.Validators.Categorys
{
    public class UpdateCategoryValidator : AbstractValidator<CategoryUpdateModel>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(3).WithMessage("Название не должно быть меньше 3 символов");
            RuleFor(x => x.ParentId).NotNull().NotEqual(Guid.Empty).WithMessage("Неправильно введённый идентефикатор");
        }
    }
}
