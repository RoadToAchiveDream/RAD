namespace RAD.WebApi.DTOs.Accounts;

public class ResetPasswordModel
{
    public string PhoneNumber { get; set; }
    public string NewPassword { get; set; }
}
