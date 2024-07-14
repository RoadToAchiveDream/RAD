using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class TaskCreateModelValidator : AbstractValidator<TaskCreateModel>
{
    public TaskCreateModelValidator()
    {
        RuleFor(task => task.Title)
            .NotNull()
            .WithMessage("Имя не должно быть пустым");

        RuleFor(task => task.Description)
            .NotEmpty()
            .WithMessage("Описание не должно быть пустым.");
    }
}
