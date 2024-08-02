using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Transactions;

public interface ITransactionService
{
    public ValueTask<Transaction> CreateAsync(Transaction transaction);
    public ValueTask<Transaction> UpdateAsync(long id, Transaction transaction);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<Transaction> GetByIdAsync(long id);
    public ValueTask<IEnumerable<Transaction>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
