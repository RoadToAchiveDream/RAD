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
            .WithMessage(rp => $"{nameof(rp.NewPassword)} is not specified");

        RuleFor(rp => rp.PhoneNumber)
            .NotNull()
            .WithMessage(rp => $"{nameof(rp.PhoneNumber)} is not specified");

        RuleFor(rp => rp.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid);

        RuleFor(rp => rp.NewPassword)
            .Must(ValidationHelper.IsPasswordHard);
    }
}