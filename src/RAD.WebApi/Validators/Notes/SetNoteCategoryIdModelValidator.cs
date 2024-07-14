using FluentValidation;
using RAD.WebApi.DTOs.Notes;

namespace RAD.WebApi.Validators.Notes;

public class SetNoteCategoryIdModelValidator : AbstractValidator<SetNoteCategoryId>
{
    public SetNoteCategoryIdModelValidator()
    {
        RuleFor(note => note.Id)
            .NotNull()
            .WithMessage("Id заметки не может быть пустым")
            .NotEqual(0)
            .WithMessage("Id заметки не может быть равен 0");

        RuleFor(note => note.CategoryId)
           .NotNull()
           .WithMessage("Id категории не может быть пустым")
           .NotEqual(0)
           .WithMessage("Id категории не может быть равен 0");
    }

}