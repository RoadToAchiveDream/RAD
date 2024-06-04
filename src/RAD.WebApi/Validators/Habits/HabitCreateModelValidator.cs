using FluentValidation;
using RAD.DTOs.Habits;

namespace RAD.WebApi.Validators.Habits;


public class HabitCreateModelValidator : AbstractValidator<HabitCreateModel>
{
    public HabitCreateModelValidator()
    {
        RuleFor(habit => habit.Name)
            .NotNull()
            .WithMessage(habit => $"{nameof(habit.Name)} is not specified");

        RuleFor(habit => habit.Description)
            .NotEmpty()
            .WithMessage(habit => $"{nameof(habit.Description)} is not specified");
    }
}

