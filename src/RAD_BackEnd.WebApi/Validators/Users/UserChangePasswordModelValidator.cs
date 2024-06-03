using FluentValidation;
using RAD_BackEnd.DTOs.Users;
using RAD_BackEnd.Services.Helpers;

namespace RAD_BackEnd.WebApi.Validators.Users;

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

        RuleFor(user => user.Phone)
            .NotNull()
            .WithMessage(user => $"{nameof(user.Phone)} is not specified");

        RuleFor(user => user.Phone)
            .Must(ValidationHelper.IsPhoneValid);

        RuleFor(user => user.OldPassword)
            .Must(ValidationHelper.IsPasswordHard);

        RuleFor(user => user.NewPassword)
            .Must(ValidationHelper.IsPasswordHard);
    }
}