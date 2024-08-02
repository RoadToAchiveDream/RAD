using RAD.Domain.Commons;
using RAD.Domain.Enums.TransactionEnums;

namespace RAD.Domain.Entities;

public class Transaction : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long CashbookId { get; set; }
    public Cashbook Cashbook { get; set; }
    public TransactionType Type { get; set; }
    public double Amount { get; set; }
    public string Name { get; set; }
}
