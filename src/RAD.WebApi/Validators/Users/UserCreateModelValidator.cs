using FluentValidation;
using RAD.Services.Helpers;
using RAD.WebApi.DTOs.Users;

namespace RAD.WebApi.Validators.Users;

public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
{
    public UserCreateModelValidator()
    {
        RuleFor(user => user.FirstName)
            .NotNull()
            .WithMessage("Имя не может быть пустым");

        RuleFor(user => user.LastName)
            .NotNull()
            .WithMessage("Фамилия не может быть пустым");

        RuleFor(user => user.PhoneNumber)
            .NotNull()
            .WithMessage("Номер телефона не может быть пустым");

        RuleFor(user => user.PhoneNumber)
            .Must(ValidationHelper.IsPhoneValid);

        RuleFor(user => user.Email)
            .NotNull()
            .WithMessage("Электронная почта не может быть пустой");

        RuleFor(user => user.Email)
            .Must(ValidationHelper.IsEmailValid);

        RuleFor(user => user.Password)
            .Must(ValidationHelper.IsPasswordHard);
    }

}