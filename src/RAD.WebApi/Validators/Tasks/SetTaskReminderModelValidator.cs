using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskReminderModelValidator : AbstractValidator<SetTaskReminderModel>
{
    public SetTaskReminderModelValidator()
    {
        RuleFor(task => task.Reminder)
            .NotNull()
            .WithMessage("Reminder cannot be null")
            .NotEqual(DateTime.MinValue)
            .WithMessage("Reminder must be greater tham minimal value")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Reminder must be in the future");
    }
}