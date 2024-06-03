using FluentValidation;
using RAD_BackEnd.DTOs.Users;
using RAD_BackEnd.Services.Helpers;

namespace RAD_BackEnd.WebApi.Validators.Users;

public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
{
    public UserUpdateModelValidator()
    {
        RuleFor(user => user.FirstName)
            .NotNull()
            .WithMessage(user => $"{nameof(user.FirstName)} is not specified");

        RuleFor(user => user.Phone)
            .NotNull()
            .WithMessage(user => $"{nameof(user.Phone)} is not specified");

        RuleFor(user => user.Phone)
            .Must(ValidationHelper.IsPhoneValid);

        RuleFor(user => user.Email)
            .NotNull()
            .WithMessage(user => $"{nameof(user.Email)} is not specified");

        RuleFor(user => user.Email)
            .Must(ValidationHelper.IsEmailValid);
    }
}