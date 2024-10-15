using FluentValidation;
using OrderBoard.Contracts.UserDto;


namespace OrderBoard.AppServices.Other.Validators.Users
{
    public class CreateUserValidator : AbstractValidator<UserCreateModel>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Login).NotNull().MinimumLength(6).WithMessage("Некорректный идентефикатор пользователя");
            RuleFor(x => x.Password).NotNull().MinimumLength(8).WithMessage("Пароль должен содержать не менее 8 символов");
            RuleFor(x => x.Name).NotNull().MinimumLength(3).WithMessage("Имя должно быть не менее 3 символов");
            RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Некорректный e-mail");
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Поле с телефонным номером должно быть заполнено");
            return;
        }
    }
}
