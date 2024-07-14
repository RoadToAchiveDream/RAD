using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskDueDateModelValidator : AbstractValidator<SetTaskDueDateModel>
{
    public SetTaskDueDateModelValidator()
    {
        RuleFor(task => task.DueDate)
            .NotNull()
            .WithMessage("Дата выполнения не может быть пустой")
            .NotEqual(DateTime.MinValue)
            .WithMessage("Дата выполнения должна быть больше минимального значения")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Дата выполнения должна быть в будущем");
    }
}
