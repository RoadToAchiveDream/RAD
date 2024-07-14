using FluentValidation;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.Validators.Tasks;

public class SetTaskCategoryIdModelValidator : AbstractValidator<SetTaskCategoryIdModel>
{
    public SetTaskCategoryIdModelValidator()
    {
        RuleFor(task => task.CategoryId)
            .NotNull()
            .WithMessage("Id категории не может быть пустым")
            .NotEqual(0)
            .WithMessage("Id категории не может быть равен 0");
    }
}