namespace RAD.WebApi.DTOs.Cashbooks;

public class CashbookViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public double Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
