using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskCategoryIdModelValidator : AbstractValidator<SetTaskCategoryIdModel>
{
    public SetTaskCategoryIdModelValidator()
    {
        RuleFor(task => task.CategoryId)
            .NotNull()
            .WithMessage("Categoryid cannot be null")
            .NotEqual(0)
            .WithMessage("CategoryId cannot be 0");
    }
}