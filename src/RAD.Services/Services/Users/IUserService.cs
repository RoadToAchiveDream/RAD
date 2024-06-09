using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Users;

public interface IUserService
{
    public ValueTask<User> CreateAsync(User user);
    public ValueTask<User> UpdateAsync(long id, User user);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<User> GetByIdAsync(long id);
    public ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    public ValueTask<(User user, string token)> LoginAsync(string phone, string password);
    public ValueTask<bool> ResetPasswordAsync(string phone, string newPassword);
    public ValueTask<bool> SendCodeAsync(string phone);
    public ValueTask<bool> ConfirmCodeAsync(string phone, string code);
    public ValueTask<User> ChangePasswordAsync(string phone, string oldPassword, string newPassword);
}
