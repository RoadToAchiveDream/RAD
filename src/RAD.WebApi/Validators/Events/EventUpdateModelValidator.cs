using FluentValidation;
using RAD.DTOs.Events;

namespace RAD.WebApi.Validators.Events;

public class EventUpdateModelValidator : AbstractValidator<EventUpdateModel>
{
    public EventUpdateModelValidator()
    {
        RuleFor(@event => @event.Title)
            .NotNull()
            .WithMessage(@event => $"{nameof(@event.Title)} is not specified");

        RuleFor(@event => @event.Description)
            .NotEmpty()
            .WithMessage(@event => $"{nameof(@event.Description)} is not specified");
    }
}
