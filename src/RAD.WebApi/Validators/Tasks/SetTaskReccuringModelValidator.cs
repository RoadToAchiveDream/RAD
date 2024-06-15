using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskReccuringModelValidator : AbstractValidator<SetTaskReccuringModel>
{
    public SetTaskReccuringModelValidator()
    {
        RuleFor(task => task.Id)
            .NotNull()
            .WithMessage("TaskId cannot be null")
            .NotEqual(0)
            .WithMessage("TaskId cannot be 0");

        RuleFor(task => task.Reccuring)
            .NotNull()
            .WithMessage("Reccuring cannot be null")
            .NotEmpty()
            .WithMessage("Reccuring cannot be empty");
    }
}
