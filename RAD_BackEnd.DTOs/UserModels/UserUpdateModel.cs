namespace RAD_BackEnd.DTOs.UserModels;
public class UserUpdateModel
{
#pragma warning disable
    public long Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
