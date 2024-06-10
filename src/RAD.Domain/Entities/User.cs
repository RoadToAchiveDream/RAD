using RAD.Domain.Commons;
using RAD.Domain.Enums.UserRoles;

namespace RAD.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public Role UserRole { get; set; } = Role.User;
    public DateTime DateOfBirth { get; set; }
}
