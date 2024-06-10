using FluentValidation;
using RAD.WebApi.DTOs.TaskCategories;

namespace RAD.WebApi.Validators.TaskCategories;

public class TaskCategoryUpdateModelValidator : AbstractValidator<TaskCategoryUpdateModel>
{
    public TaskCategoryUpdateModelValidator()
    {
        RuleFor(taskC => taskC.Name)
            .NotNull()
            .WithMessage("Name is required")
            .NotEmpty()
            .WithMessage("Name must not be empty");
    }
}
