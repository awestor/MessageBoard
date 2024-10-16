using FluentValidation;
using OrderBoard.Contracts.UserDto.Requests;

namespace OrderBoard.AppServices.Other.Validators.Users
{
    public class LoginAuthValidator : AbstractValidator<UserLoginAuthRequest>
    {
        public LoginAuthValidator()
        {
            RuleFor(x => x.Login).NotNull().MinimumLength(6).WithMessage("Некорректный логин или пароль пользователя");
            RuleFor(x => x.Password).NotNull().MinimumLength(8).WithMessage("Некорректный логин или пароль пользователя");
            return;
        }
    }
}
