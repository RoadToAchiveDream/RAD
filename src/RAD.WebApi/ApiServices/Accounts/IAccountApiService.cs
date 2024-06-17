using RAD.WebApi.DTOs.Accounts;

namespace RAD.WebApi.ApiServices.Accounts;

public interface IAccountApiService
{
    ValueTask<string> LoginAsync(LoginModel loginModel);
    ValueTask<bool> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
    ValueTask<bool> SendCodeAsync(SendCodeModel sendCodeModel);
    ValueTask<bool> ConfirmCodeAsync(ConfirmCodeModel confirmCodeModel);
}
