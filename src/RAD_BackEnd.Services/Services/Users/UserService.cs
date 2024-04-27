using AutoMapper;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.DTOs.Users;
using RAD_BackEnd.Services.Exceptions;

namespace RAD_BackEnd.Services.Services.Users;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    public async ValueTask<UserViewModel> CreateAsync(UserCreateModel user)
    {
        var existsUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Email == user.Email && !u.IsDeleted);

        if (existsUser is not null)
            throw new AlreadyExistException($"User is already exists with this Email ({user.Email})");

        var mapped = mapper.Map<User>(user);
        var created = await unitOfWork.Users.InsertAsync(mapped);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(mapped);
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

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync()
    {
        var Users = await unitOfWork.Users.SelectAsEnumerableAsync(
            expression: u => !u.IsDeleted);

        return mapper.Map<IEnumerable<UserViewModel>>(Users);
    }

    public async ValueTask<UserViewModel> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User with Id ({id}) is not found");

        return mapper.Map<UserViewModel>(existUser);
    }

    public async ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User with Id ({id}) is not found");

        var mapped = mapper.Map(user, existUser);
        var updated = await unitOfWork.Users.UpdateAsync(mapped);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(updated);
    }
}