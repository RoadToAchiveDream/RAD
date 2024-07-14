using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskPriorityModelValidator : AbstractValidator<SetTaskPriorityModel>
{
    public SetTaskPriorityModelValidator()
    {
        RuleFor(task => task.Priority)
            .NotNull()
            .NotEmpty()
            .WithMessage("Приоритет не может быть пустым");
    }
}