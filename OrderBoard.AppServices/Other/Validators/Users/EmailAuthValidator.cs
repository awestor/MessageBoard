using FluentValidation;
using OrderBoard.Contracts.Orders.Requests;
using OrderBoard.Contracts.UserDto.Requests;


namespace OrderBoard.AppServices.Other.Validators.Users
{
    public class EmailAuthValidator : AbstractValidator<UserEmailAuthRequest>
    {
        public EmailAuthValidator()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Некорректный e-mail или неверный пароль");
            RuleFor(x => x.Password).NotNull().MinimumLength(8).WithMessage("Некорректный e-mail или неверный пароль");
            return;
        }
    }
}
