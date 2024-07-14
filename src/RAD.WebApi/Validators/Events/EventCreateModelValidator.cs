using FluentValidation;
using RAD.WebApi.DTOs.Events;

namespace RAD.WebApi.Validators.Events;

public class EventCreateModelValidator : AbstractValidator<EventCreateModel>
{
    public EventCreateModelValidator()
    {
        RuleFor(@event => @event.Title)
            .NotNull()
            .WithMessage("Заголовок не должен быть пустым");

        RuleFor(@event => @event.Description)
            .NotEmpty()
            .WithMessage("Описание не должно быть пустым");
    }
}
