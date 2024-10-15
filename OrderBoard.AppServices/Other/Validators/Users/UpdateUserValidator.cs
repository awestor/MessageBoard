using FluentValidation;
using OrderBoard.AppServices.Other.Hasher;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Contracts.UserDto.Requests;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Other.Validators.Users
{
    public class UpdateUserValidator : AbstractValidator<UserUpdateInputModel>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Login).NotNull().MinimumLength(6).WithMessage("Некорректный идентефикатор пользователя");
            RuleFor(x => x.Password).NotNull().MinimumLength(8).WithMessage("Пароль должен содержать не менее 8 символов");
            RuleFor(x => x.Name).NotNull().MinimumLength(3).WithMessage("Имя должно быть не менее 3 символов");
            RuleFor(x => x.Email).NotNull().EmailAddress().MinimumLength(6).WithMessage("Некорректный e-mail");
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Поле с телефонным номером должно быть заполнено");
            return;
        }
        public static UserDataModel? UpdateValidator(UserDataModel userModel, UserUpdateInputModel inputModel)
        {
            if (!string.IsNullOrWhiteSpace(inputModel.Login) && inputModel.Login.Any(ch => char.IsLetterOrDigit(ch))) 
            { 
                userModel.Login = inputModel.Login; 
            }
            if (inputModel.Name.Any(ch => char.IsLetter(ch))) 
            { 
                userModel.Name = inputModel.Name; 
            }
            if (inputModel.Description != null) 
            {
                userModel.Description = inputModel.Description; 
            }
                userModel.Password = inputModel.Password;
                userModel.Password = CryptoHasher.GetBase64Hash(userModel.Password);
                userModel.Email = inputModel.Email; 
            if (inputModel.PhoneNumber.Length > 6 && inputModel.PhoneNumber.Any(ch => !char.IsLetter(ch))) 
            {
                var phonenumber = inputModel.PhoneNumber;
                if (inputModel.PhoneNumber[0]=='+')
                {
                    phonenumber = phonenumber.Substring(1);
                }
                if (phonenumber.Any(ch => char.IsDigit(ch)))
                {
                    userModel.PhoneNumber = "+" + phonenumber;
                }
            }
            return userModel;
        }
    }
}
