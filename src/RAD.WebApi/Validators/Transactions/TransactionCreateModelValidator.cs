using FluentValidation;
using RAD.WebApi.DTOs.Transactions;

namespace RAD.WebApi.Validators.Transactions;

public class TransactionCreateModelValidator : AbstractValidator<TransactionCreateModel>
{
    public TransactionCreateModelValidator()
    {
        RuleFor(transaction => transaction.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя трансакции не может быть пустым");

        RuleFor(transaction => transaction.CashbookId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id кассовой книги не может быть пустым")
            .NotEqual(0)
            .WithMessage("Id кассовой книги не может быть равен 0");

        RuleFor(transaction => transaction.Amount)
            .NotNull()
            .NotEmpty()
            .WithMessage("Сумма не может быть пустым")
            .NotEqual(0)
            .WithMessage("Сумма не может быть равен 0");
    }
}
public class TransactionUpdateModelValidator : AbstractValidator<TransactionUpdateModel>
{
    public TransactionUpdateModelValidator()
    {
        RuleFor(transaction => transaction.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя трансакции не может быть пустым");


    }
}