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
            .WithMessage("Старый пароль не может быть пустым");

        RuleFor(user => user.NewPassword)
            .NotNull()
            .WithMessage("Новый пароль не может быть пустым");

        RuleFor(user => user.PhoneNumber)
            .NotNull()
            .WithMessage("Номер телефона не может быть пустым");

        RuleFor(user => user.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid);

        RuleFor(user => user.OldPassword)
            .Must(ValidationHelper.IsPasswordHard);

        RuleFor(user => user.NewPassword)
            .Must(ValidationHelper.IsPasswordHard);
    }
}