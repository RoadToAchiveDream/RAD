using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Helpers;

namespace RAD.Services.Services.Users;

public class UserService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IUserService
{
    #region Private Accessories
    private readonly string cacheKey = "EmailCodeKey";
    #endregion

    #region User CRUD

    public async ValueTask<User> CreateAsync(User user)
    {
        var existsUser = await unitOfWork.Users.SelectAsync(
            expression: u => (u.Email == user.Email && u.PhoneNumber == user.PhoneNumber) && !u.IsDeleted);

        if (existsUser is not null)
            throw new AlreadyExistException($"Пользователь уже существует с этой электронной почтой ({user.Email}) или номером телефона ({user.PhoneNumber})");

        user.CreatedByUserId = HttpContextHelper.UserId;
        user.Password = PasswordHasher.Hash(user.Password);

        var created = await unitOfWork.Users.InsertAsync(user);
        await unitOfWork.SaveAsync();

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"Пользователь не найден");

        existUser.DeletedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Users.DeleteAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<User> UpdateAsync(long id, User user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"Пользователь не найден");

        existUser.FirstName = user.FirstName;
        existUser.LastName = user.LastName;
        existUser.Email = user.Email;
        existUser.PhoneNumber = user.PhoneNumber;
        existUser.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return updated;
    }

    public async ValueTask<User> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"Пользователь не найден");

        return existUser;
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Users = unitOfWork.Users.SelectAsQueryable(
            expression: u => !u.IsDeleted,
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            Users = Users.Where(user =>
                user.FirstName.ToLower().Contains(search.ToLower()) ||
                user.LastName.ToLower().Contains(search.ToLower()));

        return await Users.ToPaginateAsQueryable(@params).ToListAsync();
    }
    #endregion

    #region Account Management
    public async ValueTask<(User user, string token)> LoginAsync(string phone, string password)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: user => user.PhoneNumber == phone && !user.IsDeleted)
            ?? throw new ArgumentIsNotValidException("Введенный номер телефона или пароль недействительны");

        if (!PasswordHasher.Verify(password, existUser.Password))
            throw new ArgumentIsNotValidException("Введенный номер телефона или пароль недействительны");

        return (user: existUser, token: AuthHepler.GenerateToken(existUser));
    }

    public async ValueTask<User> ChangePasswordAsync(string phone, string oldPassword, string newPassword)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: user => user.PhoneNumber == phone && !user.IsDeleted)
            ?? throw new ArgumentIsNotValidException("Введенный номер телефона или пароль недействительны");

        if (!PasswordHasher.Verify(oldPassword, existUser.Password))
            throw new ArgumentIsNotValidException("Введенный номер телефона или пароль недействительны");

        existUser.Password = PasswordHasher.Hash(newPassword);
        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return existUser;
    }

    public async ValueTask<bool> ResetPasswordAsync(string phone, string newPassword)
    {
        var existUser = await unitOfWork.Users.SelectAsync(user => user.PhoneNumber == phone && !user.IsDeleted)
            ?? throw new NotFoundException($"Пользователь с номером телефона ({phone}) не найден");

        var code = memoryCache.Get(cacheKey) as string;
        if (!await ConfirmCodeAsync(phone, code))
            throw new ArgumentIsNotValidException("Подтверждение не удалось");

        existUser.Password = PasswordHasher.Hash(newPassword);
        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<bool> SendCodeAsync(string phone)
    {
        var existUser = await unitOfWork.Users.SelectAsync(user => user.PhoneNumber == phone && !user.IsDeleted)
           ?? throw new NotFoundException($"Пользователь с номером телефона ({phone}) не найден");

        var random = new Random();
        var code = random.Next(10000, 99999);
        await EmailHelper.SendMessageAsync(existUser.Email, "Код подтверждения", code.ToString());

        var memoryCacheOptions = new MemoryCacheEntryOptions()
            .SetSize(50)
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
            .SetSlidingExpiration(TimeSpan.FromSeconds(30))
            .SetPriority(CacheItemPriority.Normal);

        memoryCache.Set(cacheKey, code.ToString(), memoryCacheOptions);

        return true;
    }

    public async ValueTask<bool> ConfirmCodeAsync(string phone, string code)
    {
        var existUser = await unitOfWork.Users.SelectAsync(user => user.PhoneNumber == phone && !user.IsDeleted)
           ?? throw new NotFoundException($"Пользователь с номером телефона ({phone}) не найден");

        if (memoryCache.Get(cacheKey) as string == code)
            return true;

        return false;
    }
    #endregion
}