using FluentValidation;
using RAD.WebApi.DTOs.NoteCategories;

namespace RAD.WebApi.Validators.NoteCategories;

public class NoteCategoryCreateModelValidator : AbstractValidator<NoteCategoryCreateModel>
{
    public NoteCategoryCreateModelValidator()
    {
        RuleFor(nc => nc.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя не должно быть пустым");
    }
}
