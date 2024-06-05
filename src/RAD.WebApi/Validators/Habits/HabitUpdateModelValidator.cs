using FluentValidation;
using RAD.WebApi.DTOs.Habits;

namespace RAD.WebApi.Validators.Habits;

public class HabitUpdateModelValidator : AbstractValidator<HabitUpdateModel>
{
    public HabitUpdateModelValidator()
    {
        RuleFor(habit => habit.Name)
            .NotNull()
            .WithMessage(habit => $"{nameof(habit.Name)} is not specified");

        RuleFor(habit => habit.Description)
            .NotEmpty()
            .WithMessage(habit => $"{nameof(habit.Description)} is not specified");
    }
}

