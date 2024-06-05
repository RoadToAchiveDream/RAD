namespace RAD.WebApi.DTOs.Users;

public class UserChangePasswordModel
{
    public string PhoneNumber { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
