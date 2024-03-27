using RAD_BackEnd.Domain.Commons;

namespace RAD_BackEnd.Domain.Entities;

public class Tasks : Auditable
{
    public long UserId { get; set; }
    public string Body { get; set; }
}
