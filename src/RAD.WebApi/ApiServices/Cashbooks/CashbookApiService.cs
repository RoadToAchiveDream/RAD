using AutoMapper;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Services.Cashbooks;
using RAD.WebApi.DTOs.Cashbooks;
using RAD.WebApi.Extensions;
using RAD.WebApi.Validators.Cashbooks;

namespace RAD.WebApi.ApiServices.Cashbooks;

public class CashbookApiService(IMapper mapper,
    ICashbookService cashbookService,
    CashbookCreateModelValidator createModelValidator,
    CashbookUpdateModelValidator updateModelValidator) : ICashbookApiService
{
    public async ValueTask<CashbookViewModel> GetAsync(long id)
    {
        var cashbook = await cashbookService.GetByIdAsync(id);
        return mapper.Map<CashbookViewModel>(cashbook);
    }

    public async ValueTask<IEnumerable<CashbookViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var cashbooks = await cashbookService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<CashbookViewModel>>(cashbooks);
    }

    public async ValueTask<CashbookViewModel> PostAsync(CashbookCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var cashbook = await cashbookService.CreateAsync(mapper.Map<Cashbook>(model));
        return mapper.Map<CashbookViewModel>(cashbook);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await cashbookService.DeleteAsync(id);
    }

    public async ValueTask<CashbookViewModel> PutAsync(long id, CashbookUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var cashbook = await cashbookService.UpdateAsync(id, mapper.Map<Cashbook>(model));
        return mapper.Map<CashbookViewModel>(cashbook);
    }
}