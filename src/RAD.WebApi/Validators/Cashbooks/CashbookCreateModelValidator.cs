using FluentValidation;
using RAD.WebApi.DTOs.Cashbooks;

namespace RAD.WebApi.Validators.Cashbooks;

public class CashbookCreateModelValidator : AbstractValidator<CashbookCreateModel>
{
    public CashbookCreateModelValidator()
    {
        RuleFor(cashbook => cashbook.Name)
        .NotNull()
        .NotEmpty()
        .WithMessage("Имя не должен быть пустым!");
    }
}
