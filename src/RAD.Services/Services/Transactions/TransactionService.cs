using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Domain.Enums.TransactionEnums;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Helpers;
using RAD.Services.Services.Cashbooks;
using RAD.Services.Services.Users;

namespace RAD.Services.Services.Transactions;

public class TransactionService(
    IUserService userService,
    ICashbookService cashbookService,
    IUnitOfWork unitOfWork) : ITransactionService
{
    public async ValueTask<Transaction> CreateAsync(Transaction transaction)
    {
        var existUser = await userService.GetByIdAsync(HttpContextHelper.UserId);

        var existCashbook = await cashbookService.GetByIdAsync(transaction.CashbookId);

        existCashbook.Balance += transaction.Amount;
        transaction.UserId = existUser.Id;
        transaction.CashbookId = existCashbook.Id;
        transaction.User = existUser;
        transaction.CreatedByUserId = HttpContextHelper.UserId;

        var created = await unitOfWork.Transactions.InsertAsync(transaction);
        await unitOfWork.SaveAsync();

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTransaction = await unitOfWork.Transactions.SelectAsync(
            expression: t => (t.Id == id && t.UserId == HttpContextHelper.UserId) && !t.IsDeleted)
            ?? throw new NotFoundException($"Трансакция не найдена");

        var existCashbook = await cashbookService.GetByIdAsync(existTransaction.CashbookId);

        if (existTransaction.Type == TransactionType.Income)
            existCashbook.Balance -= existTransaction.Amount;

        else if (existTransaction.Type == TransactionType.Expense)
            existCashbook.Balance += existTransaction.Amount;

        existTransaction.DeletedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Transactions.DeleteAsync(existTransaction);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Transaction>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Transactions = unitOfWork.Transactions.SelectAsQueryable(
            expression: c => !c.IsDeleted && c.UserId == HttpContextHelper.UserId,
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            Transactions = Transactions.Where(cashbook =>
                cashbook.Name.ToLower().Contains(search.ToLower()));

        return await Transactions.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Transaction> GetByIdAsync(long id)
    {
        var existsTransaction = await unitOfWork.Transactions.SelectAsync(
            expression: t => t.Id == id && !t.IsDeleted && t.UserId == HttpContextHelper.UserId)
            ?? throw new NotFoundException($"Трансакция не найдена");

        return existsTransaction;
    }

    public async ValueTask<Transaction> UpdateAsync(long id, Transaction transaction)
    {
        var existTransaction = await unitOfWork.Transactions.SelectAsync(
           expression: t => (t.Id == id && t.UserId == HttpContextHelper.UserId) && !t.IsDeleted)
           ?? throw new NotFoundException("Трансакция не найдена");

        var existUser = await userService.GetByIdAsync(HttpContextHelper.UserId);
        var existCashbook = await cashbookService.GetByIdAsync(existTransaction.CashbookId);

        if (existTransaction.Type == transaction.Type && existTransaction.Amount == transaction.Amount && existTransaction.Name == transaction.Name)
            throw new AlreadyExistException("Трансакция уже имеет такие же данные");

        if (existTransaction.Type == TransactionType.Income)
        {
            existCashbook.Balance -= existTransaction.Amount;
        }
        else if (existTransaction.Type == TransactionType.Expense)
        {
            existCashbook.Balance += existTransaction.Amount;
        }

        if (transaction.Amount == 0)
            transaction.Amount = existTransaction.Amount;

        existTransaction.Type = transaction.Type;
        existTransaction.Amount = transaction.Amount;
        existTransaction.Name = transaction.Name;
        existTransaction.UpdatedByUserId = HttpContextHelper.UserId;

        if (transaction.Type == TransactionType.Income)
        {
            existCashbook.Balance += transaction.Amount;
        }
        else if (transaction.Type == TransactionType.Expense)
        {
            existCashbook.Balance -= transaction.Amount;
        }

        var updated = await unitOfWork.Transactions.UpdateAsync(existTransaction);
        await unitOfWork.SaveAsync();

        return updated;
    }
}
