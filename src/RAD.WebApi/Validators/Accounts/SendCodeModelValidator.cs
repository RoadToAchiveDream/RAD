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
            .WithMessage(sc => $"{nameof(sc.PhoneNumber)} is not specified");

        RuleFor(sc => sc.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid);
    }
}