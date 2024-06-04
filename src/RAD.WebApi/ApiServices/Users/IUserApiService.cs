using RAD.DTOs.Users;
using RAD.Services.Configurations;

namespace RAD.WebApi.ApiServices.Users;

public interface IUserApiService
{
    public ValueTask<UserViewModel> PostAsync(UserCreateModel model);
    public ValueTask<UserViewModel> PutAsync(long id, UserUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<UserViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<UserViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
    public ValueTask<UserViewModel> ChangePasswordAsync(UserChangePasswordModel changePasswordModel);
}
