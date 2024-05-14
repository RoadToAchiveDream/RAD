using RAD_BackEnd.Domain.Commons;

namespace RAD_BackEnd.Domain.Entities;

public class Role : Auditable
{
    public string Name { get; set; }
}