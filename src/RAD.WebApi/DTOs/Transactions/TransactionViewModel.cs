using RAD.Domain.Enums.TransactionEnums;

namespace RAD.WebApi.DTOs.Transactions;

public class TransactionViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
    public long CashbookId { get; set; }
    public TransactionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}