using RAD.Domain.Enums.TransactionEnums;

namespace RAD.WebApi.DTOs.Transactions;

public class TransactionUpdateModel
{
    public string Name { get; set; }
    public double Amount { get; set; }
    public TransactionType Type { get; set; }
}
