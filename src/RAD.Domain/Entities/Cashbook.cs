using RAD.Domain.Commons;

namespace RAD.Domain.Entities;

public class Cashbook : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public double Balance { get; set; }
}
