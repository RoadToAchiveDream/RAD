using RAD_BackEnd.Domain.Entities;

namespace RAD_BackEnd.Services.Services.Users;

public interface IUserService
{
    ValueTask<User> CreateAsync(User user);
    ValueTask<User> UpdateAsync(long id, User user);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<User> GetByIdAsync(long id);
    ValueTask<IEnumerable<User>> GetAllAsync();
}
