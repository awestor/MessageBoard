using FluentValidation;
using OrderBoard.AppServices.Other.Hasher;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Other.Validators.Users
{
    public class UpdateUserValidator
    {
        public static UserDataModel? UpdateValidator(UserDataModel userModel, UserUpdateInputModel inputModel)
        {
            if (!string.IsNullOrWhiteSpace(inputModel.Login) && inputModel.Login.Length>6 && inputModel.Login.Any(ch => char.IsLetterOrDigit(ch))) 
            { 
                userModel.Login = inputModel.Login; 
            }
            if (inputModel.Name != null && inputModel.Name.Length > 6 && inputModel.Name.Any(ch => char.IsLetter(ch))) 
            { 
                userModel.Name = inputModel.Name; 
            }
            if (inputModel.Description != null) 
            {
                userModel.Description = inputModel.Description; 
            }
            if (inputModel.Password != null && inputModel.Password.Length > 8) 
            {
                userModel.Password = inputModel.Password;
                userModel.Password = CryptoHasher.GetBase64Hash(userModel.Password);
            }
            if (inputModel.Email != null && inputModel.Email.Length > 8) 
            { 
                userModel.Email = inputModel.Email; 
            }
            if (inputModel.PhoneNumber != null && inputModel.PhoneNumber.Length > 6 && inputModel.PhoneNumber.Any(ch => !char.IsLetter(ch))) 
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
