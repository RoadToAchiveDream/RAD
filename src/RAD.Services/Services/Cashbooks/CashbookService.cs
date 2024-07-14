using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Helpers;
using RAD.Services.Services.Users;

namespace RAD.Services.Services.Cashbooks;

public class CashbookService(IUserService userService, IUnitOfWork unitOfWork) : ICashbookService
{
    public async ValueTask<Cashbook> CreateAsync(Cashbook cashbook)
    {
        var existUser = await userService.GetByIdAsync(HttpContextHelper.UserId);

        var existCashbook = await unitOfWork.Cashbooks.SelectAsync(
            expression: c => (c.Name == cashbook.Name && c.UserId == HttpContextHelper.UserId) && !c.IsDeleted);

        if (existCashbook is not null)
            throw new AlreadyExistException($"Кассовая книга с названием ({cashbook.Name} уже существует)");

        cashbook.UserId = existUser.Id;
        cashbook.User = existUser;
        cashbook.CreatedByUserId = HttpContextHelper.UserId;

        var created = await unitOfWork.Cashbooks.InsertAsync(cashbook);
        await unitOfWork.SaveAsync();

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existCashbook = await unitOfWork.Cashbooks.SelectAsync(
            expression: c => (c.Id == id && c.UserId == HttpContextHelper.UserId) && !c.IsDeleted)
            ?? throw new NotFoundException($"Кассовая книга не найдена");

        existCashbook.DeletedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Cashbooks.DeleteAsync(existCashbook);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Cashbook>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Cashbooks = unitOfWork.Cashbooks.SelectAsQueryable(
            expression: c => !c.IsDeleted && c.UserId == HttpContextHelper.UserId,
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            Cashbooks = Cashbooks.Where(cashbook =>
                cashbook.Name.ToLower().Contains(search.ToLower()));

        return await Cashbooks.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Cashbook> GetByIdAsync(long id)
    {
        var existsCashbook = await unitOfWork.Cashbooks.SelectAsync(
            expression: t => t.Id == id && !t.IsDeleted && t.UserId == HttpContextHelper.UserId)
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        return existsCashbook;
    }

    public ValueTask<Cashbook> UpdateAsync(long id, Cashbook cashbook)
    {
        throw new NotImplementedException();
    }
}
