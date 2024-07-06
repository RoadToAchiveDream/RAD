using FluentValidation;
using RAD.Services.Helpers;
using RAD.WebApi.DTOs.Accounts;

namespace RAD.WebApi.Validators.Accounts;

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(loginModel => loginModel.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Пароль не должен буть пустым");
        
        RuleFor(loginModel => loginModel.Password)
            .Must(ValidationHelper.IsPasswordHard)
            .WithMessage("Пароль должен быть не слабым");

        RuleFor(loginModel => loginModel.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage("Номер телефона не должен буть пустым");

        RuleFor(loginModel => loginModel.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid)
            .WithMessage("Номер телефона должен быть правильным");

    }
}
