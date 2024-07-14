using RAD.Services.Configurations;
using RAD.WebApi.DTOs.Cashbooks;

namespace RAD.WebApi.ApiServices.Cashbooks;

public interface ICashbookApiService
{
    public ValueTask<CashbookViewModel> PostAsync(CashbookCreateModel model);
    public ValueTask<CashbookViewModel> PutAsync(long id, CashbookUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<CashbookViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<CashbookViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}
