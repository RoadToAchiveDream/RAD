using RAD.Services.Configurations;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.ApiServices.Tasks;

public interface ITaskApiService
{
    public ValueTask<TaskViewModel> PostAsync(TaskCreateModel model);
    public ValueTask<TaskViewModel> PutAsync(long id, TaskUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<TaskViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<TaskViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}
