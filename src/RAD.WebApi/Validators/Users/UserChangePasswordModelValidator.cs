using FluentValidation;
using RAD.Services.Helpers;
using RAD.WebApi.DTOs.Users;

namespace RAD.WebApi.Validators.Users;

public class UserChangePasswordModelValidator : AbstractValidator<UserChangePasswordModel>
{
    public UserChangePasswordModelValidator()
    {
        RuleFor(user => user.OldPassword)
            .NotNull()
            .WithMessage(user => $"{nameof(user.OldPassword)} is not specified");

        RuleFor(user => user.NewPassword)
            .NotNull()
            .WithMessage(user => $"{nameof(user.NewPassword)} is not specified");

        RuleFor(user => user.PhoneNumber)
            .NotNull()
            .WithMessage(user => $"{nameof(user.PhoneNumber)} is not specified");

        RuleFor(user => user.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid);

        RuleFor(user => user.OldPassword)
            .Must(ValidationHelper.IsPasswordHard);

        RuleFor(user => user.NewPassword)
            .Must(ValidationHelper.IsPasswordHard);
    }
}