using FluentValidation;
using RAD.WebApi.DTOs.Notes;

namespace RAD.WebApi.Validators.Notes;

public class NoteCreateModelValidator : AbstractValidator<NoteCreateModel>
{
    public NoteCreateModelValidator()
    {
        RuleFor(note => note.Title)
            .NotNull()
            .WithMessage("Имя не должно быть пустым");

        RuleFor(note => note.Content)
            .NotEmpty()
            .WithMessage("Содержимое не должно быть пустым.");
    }
}
