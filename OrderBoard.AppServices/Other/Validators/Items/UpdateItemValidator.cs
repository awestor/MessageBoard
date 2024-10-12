using FluentValidation;
using OrderBoard.Contracts.Items;


namespace OrderBoard.AppServices.Other.Validators.ItemValidator
{
    public class UpdateItemValidator : AbstractValidator<ItemUpdateModel>
    {

        public UpdateItemValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEqual(Guid.Empty);
            /*if (model == null) { throw new EntitysNotVaildException("Модель пуста!"); }
            if (model.UserId.ToString() == claimId) { throw new EntitysNotVaildException("UserId пуст!"); }
            if (model.Name == null) { throw new EntitysNotVaildException("Имя пустое!"); }
            if (model.Count <= 0) { throw new EntitysNotVaildException("Количество меньше нуля или пусто!"); }
            if (model.Price <= 0) { throw new EntitysNotVaildException("Цена меньше нуля или пуста!"); }*/
            return;
        }
    }
}
