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
            .NotEmpty()
            .WithMessage("Код не должен быть пустым");

        RuleFor(cc => cc.PhoneNumber)
             .NotNull()
             .NotEmpty()
             .WithMessage("Номер телефона не должен буть пустым");

        RuleFor(cc => cc.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid)
            .WithMessage("Номер телефона должен быть правильным");
    }
}
