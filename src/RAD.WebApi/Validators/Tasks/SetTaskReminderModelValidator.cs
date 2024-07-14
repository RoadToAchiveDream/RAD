using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskReminderModelValidator : AbstractValidator<SetTaskReminderModel>
{
    public SetTaskReminderModelValidator()
    {
        RuleFor(task => task.Reminder)
            .NotNull()
            .WithMessage("Напоминание не может быть пустым")
            .NotEqual(DateTime.MinValue)
            .WithMessage("Напоминание должно быть больше минимального значения")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Напоминание должно быть в будущем");
    }
}