using FluentValidation;
using OrderBoard.Contracts.Items;


namespace OrderBoard.AppServices.Other.Validators.ItemValidator
{
    public class UpdateItemValidator : AbstractValidator<ItemUpdateModel>
    {

        public UpdateItemValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            return;
        }
    }
}
