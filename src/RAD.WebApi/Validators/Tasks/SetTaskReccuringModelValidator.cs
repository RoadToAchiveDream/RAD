using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskReccuringModelValidator : AbstractValidator<SetTaskReccuringModel>
{
    public SetTaskReccuringModelValidator()
    {
        RuleFor(task => task.Reccuring)
            .NotNull()
            .NotEmpty()
            .WithMessage("Повторение не может быть пустым");
    }
}
