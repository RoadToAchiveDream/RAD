using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.Services.Exceptions;

namespace RAD_BackEnd.Services.Services.Users;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async ValueTask<User> CreateAsync(User user)
    {
        var existsUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Email == user.Email && !u.IsDeleted);

        if (existsUser is not null)
            throw new AlreadyExistException($"User is already exists with this Email ({user.Email})");

        var created = await unitOfWork.Users.InsertAsync(user);
        await unitOfWork.SaveAsync();

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User with Id ({id}) is not found");

        await unitOfWork.Users.DeleteAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync()
    {
        var Users = await unitOfWork.Users.SelectAsEnumerableAsync(
            expression: u => !u.IsDeleted);

        return Users;
    }

    public async ValueTask<User> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User with Id ({id}) is not found");

        return existUser;
    }

    public async ValueTask<User> UpdateAsync(long id, User user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User with Id ({id}) is not found");

        existUser.FirstName = user.FirstName;
        existUser.LastName = user.LastName;
        existUser.Email = user.Email;
        existUser.ProfilePicture = user.ProfilePicture;

        var updated = await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return updated;
    }
}