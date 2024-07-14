using FluentValidation;
using RAD.WebApi.DTOs.TaskCategories;

namespace RAD.WebApi.Validators.TaskCategories;

public class TaskCategoryCreateModelValidator : AbstractValidator<TaskCategoryCreateModel>
{
    public TaskCategoryCreateModelValidator()
    {
        RuleFor(taskC => taskC.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя не должно быть пустым");
    }
}
