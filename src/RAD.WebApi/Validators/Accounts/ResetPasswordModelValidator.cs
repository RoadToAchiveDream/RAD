using FluentValidation;
using RAD.Services.Helpers;
using RAD.WebApi.DTOs.Accounts;

namespace RAD.WebApi.Validators.Accounts;

public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordModelValidator()
    {
        RuleFor(rp => rp.NewPassword)
            .NotNull()
            .NotEmpty()
            .WithMessage("Пароль не должен буть пустым");

        RuleFor(rp => rp.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage("Номер телефона не должен буть пустым");

        RuleFor(rp => rp.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid)
            .WithMessage("Номер телефона должен быть правильным");


        RuleFor(rp => rp.NewPassword)
            .Must(ValidationHelper.IsPasswordHard)
            .WithMessage("Пароль должен быть не слабым");
    }
}