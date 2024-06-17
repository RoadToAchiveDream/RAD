using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskDueDateModelValidator : AbstractValidator<SetTaskDueDateModel>
{
    public SetTaskDueDateModelValidator()
    {
        RuleFor(task => task.DueDate)
            .NotNull()
            .WithMessage("DueDate cannot be null")
            .NotEqual(DateTime.MinValue)
            .WithMessage("DueDate must be greater tham minimal value")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("DueDate must be in the future");
    }
}
