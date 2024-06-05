using RAD.WebApi.DTOs.Accounts;
using RAD.WebApi.DTOs.Users;

namespace RAD.WebApi.ApiServices.Accounts;

public interface IAccountApiService
{
    ValueTask<UserViewModel> LoginAsync(LoginModel loginModel);
    ValueTask<bool> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
    ValueTask<bool> SendCodeAsync(SendCodeModel sendCodeModel);
    ValueTask<bool> ConfirmCodeAsync(ConfirmCodeModel confirmCodeModel);
}