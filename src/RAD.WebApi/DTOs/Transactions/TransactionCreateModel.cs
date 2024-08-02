using RAD.Domain.Enums.TransactionEnums;

namespace RAD.WebApi.DTOs.Transactions;

public class TransactionCreateModel
{
    public string Name { get; set; }
    public double Amount { get; set; }
    public long CashbookId { get; set; }
    public TransactionType Type { get; set; }
}
