namespace RAD_BackEnd.Domain.Commons;
public class Auditable
{
    public long Id { get; set; }
    public DateTimeOffset Created_At { get; set; }
    public DateTimeOffset Updated_At { get; set; }
    public DateTimeOffset Deleted_At { get; set; }
    public bool Is_Deleted { get; set; } = false;
    public bool Is_Completed { get; set; } = false;
    public bool Is_Achieved { get; set; }
}
