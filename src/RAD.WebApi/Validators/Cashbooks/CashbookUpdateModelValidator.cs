using FluentValidation;
using RAD.WebApi.DTOs.Cashbooks;

namespace RAD.WebApi.Validators.Cashbooks;

public class CashbookUpdateModelValidator : AbstractValidator<CashbookUpdateModel>
{
    public CashbookUpdateModelValidator()
    {
        RuleFor(cashbook => cashbook.Name)
       .NotNull()
       .NotEmpty()
       .WithMessage("Имя не должен быть пустым!");
    }
}