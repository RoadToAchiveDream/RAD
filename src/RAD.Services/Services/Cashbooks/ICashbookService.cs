using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Cashbooks;

public interface ICashbookService
{
    public ValueTask<Cashbook> CreateAsync(Cashbook cashbook);
    public ValueTask<Cashbook> UpdateAsync(long id, Cashbook cashbook);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<Cashbook> GetByIdAsync(long id);
    public ValueTask<IEnumerable<Cashbook>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
