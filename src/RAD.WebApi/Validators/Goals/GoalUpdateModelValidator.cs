using FluentValidation;
using RAD.WebApi.DTOs.Goals;

namespace RAD.WebApi.Validators.Goals;

public class GoalUpdateModelValidator : AbstractValidator<GoalUpdateModel>
{
    public GoalUpdateModelValidator()
    {
        RuleFor(goal => goal.Title)
            .NotNull()
            .WithMessage(goal => $"{nameof(goal.Title)} is not specified");

        RuleFor(goal => goal.Description)
            .NotEmpty()
            .WithMessage(goal => $"{nameof(goal.Description)} is not specified");
    }
}
