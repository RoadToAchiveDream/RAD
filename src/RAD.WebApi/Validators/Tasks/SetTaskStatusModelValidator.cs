using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskStatusModelValidator : AbstractValidator<SetTaskStatusModel>
{
    public SetTaskStatusModelValidator()
    {
        RuleFor(task => task.Status)
            .NotNull()
            .WithMessage("Status cannot be null")
            .NotEmpty()
            .WithMessage("Status cannot be empty");
    }
}
