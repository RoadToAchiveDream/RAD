using RAD_BackEnd.DTOs.Users;

namespace RAD_BackEnd.Services.Services.Users;

public interface IUserService
{
    ValueTask<UserViewModel> CreateAsync(UserCreateModel user);
    ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync();
}
