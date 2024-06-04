using RAD.Domain.Commons;

namespace RAD.Domain.Entities;

public class Role : Auditable
{
    public string Name { get; set; }
}