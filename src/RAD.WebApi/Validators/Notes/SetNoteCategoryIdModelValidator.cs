using FluentValidation;
using RAD.WebApi.DTOs.Notes;

namespace RAD.WebApi.Validators.Notes;

public class SetNoteCategoryIdModelValidator : AbstractValidator<SetNoteCategoryId>
{
    public SetNoteCategoryIdModelValidator()
    {
        RuleFor(note => note.Id)
            .NotNull()
            .WithMessage("NoteId cannot be null")
            .NotEqual(0)
            .WithMessage("NoteId cannot be 0");

        RuleFor(note => note.CategoryId)
           .NotNull()
           .WithMessage("CategoryId cannot be null")
           .NotEqual(0)
           .WithMessage("CategoryId cannot be 0");
    }
}