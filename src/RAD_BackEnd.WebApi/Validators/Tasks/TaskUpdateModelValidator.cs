using FluentValidation;
using RAD_BackEnd.DTOs.Tasks;

namespace RAD_BackEnd.WebApi.Validators.Tasks;

public class TaskUpdateModelValidator : AbstractValidator<TaskUpdateModel>
{
    public TaskUpdateModelValidator()
    {
        RuleFor(task => task.Title)
            .NotNull()
            .WithMessage(task => $"{nameof(task.Title)} is not specified");

        RuleFor(task => task.Description)
            .NotEmpty()
            .WithMessage(task => $"{nameof(task.Description)} is not specified");
    }
}
