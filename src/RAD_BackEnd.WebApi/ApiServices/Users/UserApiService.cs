using AutoMapper;
using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.DTOs.Users;
using RAD_BackEnd.Services.Configurations;
using RAD_BackEnd.Services.Services.Users;
using RAD_BackEnd.WebApi.Extensions;
using RAD_BackEnd.WebApi.Validators.Users;

namespace RAD_BackEnd.WebApi.ApiServices.Users;

public class UserApiService(
    IMapper mapper,
    IUserService userService,
    UserCreateModelValidator createModelValidator,
    UserUpdateModelValidator updateModelValidator,
    UserChangePasswordModelValidator changePasswordValidator) : IUserApiService
{

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await userService.DeleteAsync(id);
    }

    public async ValueTask<UserViewModel> GetAsync(long id)
    {
        var user = await userService.GetByIdAsync(id);
        return mapper.Map<UserViewModel>(user);
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = await userService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<UserViewModel>>(users);
    }

    public async ValueTask<UserViewModel> PostAsync(UserCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var createdModel = await userService.CreateAsync(mapper.Map<User>(model));
        return mapper.Map<UserViewModel>(createdModel);
    }

    public async ValueTask<UserViewModel> PutAsync(long id, UserUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var user = await userService.UpdateAsync(id, mapper.Map<User>(model));
        return mapper.Map<UserViewModel>(user);
    }

    public async ValueTask<UserViewModel> ChangePasswordAsync(UserChangePasswordModel changePasswordModel)
    {
        await changePasswordValidator.EnsureValidatedAsync(changePasswordModel);
        var user = await userService.ChangePasswordAsync(
            changePasswordModel.PhoneNumber,
            changePasswordModel.OldPassword,
            changePasswordModel.NewPassword);

        return mapper.Map<UserViewModel>(user);
    }
}
