using AutoMapper;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.DTOs.Users;

namespace RAD_BackEnd.Services.Services.Users;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    public async ValueTask<UserViewModel> CreateAsync(UserCreateModel user)
    {
        var existsUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Email == user.Email && !u.IsDeleted);

        if (existsUser is not null)
            throw new Exception();
    }

    public ValueTask<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<UserViewModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public ValueTask<UserViewModel> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user)
    {
        throw new NotImplementedException();
    }
}