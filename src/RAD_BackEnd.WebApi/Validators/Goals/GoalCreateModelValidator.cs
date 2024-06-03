using FluentValidation;
using RAD_BackEnd.DTOs.Goals;

namespace RAD_BackEnd.WebApi.Validators.Goals;

public class GoalCreateModelValidator : AbstractValidator<GoalCreateModel>
{
    public GoalCreateModelValidator()
    {
        RuleFor(goal => goal.Title)
            .NotNull()
            .WithMessage(goal => $"{nameof(goal.Title)} is not specified");

        RuleFor(goal => goal.Description)
            .NotEmpty()
            .WithMessage(goal => $"{nameof(goal.Description)} is not specified");
    }
}
