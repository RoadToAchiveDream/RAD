using RAD_BackEnd.Domain.Commons;

namespace RAD_BackEnd.Domain.Entities;
public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumer { get; set; }
    public string Password { get; set; }
    public string ProfilePicture { get; set; }
    public DateTime DateOfBirth { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }
}
