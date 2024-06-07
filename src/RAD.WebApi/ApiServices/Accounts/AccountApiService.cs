using AutoMapper;
using RAD.Services.Services.Users;
using RAD.WebApi.DTOs.Accounts;
using RAD.WebApi.DTOs.Users;
using RAD.WebApi.Extensions;
using RAD.WebApi.Validators.Accounts;

namespace RAD.WebApi.ApiServices.Accounts;

public class AccountApiService(
    IMapper mapper,
    IUserService userService,
    LoginModelValidator loginModelValidator,
    SendCodeModelValidator sendCodeModelValidator,
    ConfirmCodeModelValidator confirmCodeModelValidator,
    ResetPasswordModelValidator resetPasswordModelValidator) : IAccountApiService
{
    public async ValueTask<bool> ConfirmCodeAsync(ConfirmCodeModel confirmCodeModel)
    {
        await confirmCodeModelValidator.EnsureValidatedAsync(confirmCodeModel);
        return await userService.ConfirmCodeAsync(confirmCodeModel.PhoneNumber, confirmCodeModel.Code);

    }

    public async ValueTask<string> LoginAsync(LoginModel loginModel)
    {
        await loginModelValidator.EnsureValidatedAsync(loginModel);

        var loggedIn = await userService.LoginAsync(loginModel.PhoneNumber, loginModel.Password);

        var mapped = mapper.Map<UserViewModel>(loggedIn.user);

        return loggedIn.token;
    }

    public async ValueTask<bool> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
    {
        await resetPasswordModelValidator.EnsureValidatedAsync(resetPasswordModel);
        return await userService.ResetPasswordAsync(resetPasswordModel.PhoneNumber, resetPasswordModel.NewPassword);
    }

    public async ValueTask<bool> SendCodeAsync(SendCodeModel sendCodeModel)
    {
        await sendCodeModelValidator.EnsureValidatedAsync(sendCodeModel);
        return await userService.SendCodeAsync(sendCodeModel.PhoneNumber);
    }
}
