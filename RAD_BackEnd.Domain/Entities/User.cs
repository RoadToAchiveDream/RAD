using RAD_BackEnd.Domain.Commons;

namespace RAD_BackEnd.Domain.Entities;

public class User : Auditable
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
