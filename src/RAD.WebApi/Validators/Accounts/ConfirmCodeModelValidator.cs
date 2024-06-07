using FluentValidation;
using RAD.Services.Helpers;
using RAD.WebApi.DTOs.Accounts;

namespace RAD.WebApi.Validators.Accounts;

public class ConfirmCodeModelValidator : AbstractValidator<ConfirmCodeModel>
{
    public ConfirmCodeModelValidator()
    {
        RuleFor(cc => cc.Code)
            .NotNull()
            .WithMessage(cc => $"{nameof(cc.Code)} is not specified");

        RuleFor(cc => cc.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid);
    }
}
