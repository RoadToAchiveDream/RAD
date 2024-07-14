using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskStatusModelValidator : AbstractValidator<SetTaskStatusModel>
{
    public SetTaskStatusModelValidator()
    {
        RuleFor(task => task.Status)
            .NotNull()
            .NotEmpty()
            .WithMessage("Статус не может быть пустым");
    }
}
