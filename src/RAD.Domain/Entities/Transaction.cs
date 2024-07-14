using RAD.Domain.Commons;
using RAD.Domain.Enums.TransactionEnums;

namespace RAD.Domain.Entities;

public class Transaction : Auditable
{
    public long UsertId { get; set; }
    public User User { get; set; }
    public string CashbookId { get; set; }
    public Cashbook Cashbook { get; set; }
    public TransactionType Type { get; set; }
    public double Amount { get; set; }
    public string Name { get; set; }
}
