using FluentValidation;
using RAD.Services.Helpers;
using RAD.WebApi.DTOs.Accounts;

namespace RAD.WebApi.Validators.Accounts;

public class SendCodeModelValidator : AbstractValidator<SendCodeModel>
{
    public SendCodeModelValidator()
    {

        RuleFor(sc => sc.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage("Номер телефона не должен буть пустым");

        RuleFor(sc => sc.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid)
            .WithMessage("Номер телефона должен быть правильным");
    }
}