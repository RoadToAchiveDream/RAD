using RAD.DTOs.Users;
using RAD.Services.Configurations;

namespace RAD.WebApi.ApiServices.Users;

public interface IUserApiService
{
    ValueTask<UserViewModel> PostAsync(UserCreateModel model);
    ValueTask<UserViewModel> PutAsync(long id, UserUpdateModel model);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<UserViewModel> ChangePasswordAsync(UserChangePasswordModel changePasswordModel);
}
