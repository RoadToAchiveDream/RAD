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
            .WithMessage(loginModel => $"{nameof(loginModel.Password)} is not specified");

        RuleFor(loginModel => loginModel.PhoneNumber)
            .NotNull()
            .WithMessage(loginModel => $"{nameof(loginModel.PhoneNumber)} is not specified");

        RuleFor(loginModel => loginModel.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid);

        RuleFor(loginModel => loginModel.Password)
            .Must(ValidationHelper.IsPasswordHard);
    }
}
