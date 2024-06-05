using FluentValidation;
using RAD.WebApi.DTOs.Events;

namespace RAD.WebApi.Validators.Events;

public class EventCreateModelValidator : AbstractValidator<EventCreateModel>
{
    public EventCreateModelValidator()
    {
        RuleFor(@event => @event.Title)
            .NotNull()
            .WithMessage(@event => $"{nameof(@event.Title)} is not specified");

        RuleFor(@event => @event.Description)
            .NotEmpty()
            .WithMessage(@event => $"{nameof(@event.Description)} is not specified");
    }
}
