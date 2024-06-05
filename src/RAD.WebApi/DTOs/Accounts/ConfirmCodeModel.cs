namespace RAD.WebApi.DTOs.Accounts;

public class ConfirmCodeModel
{
    public string PhoneNumber { get; set; }
    public string Code { get; set; }
}