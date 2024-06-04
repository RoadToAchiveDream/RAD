using RAD.DTOs.Tasks;
using RAD.Services.Configurations;

namespace RAD.WebApi.ApiServices.Tasks;

public interface ITaskApiService
{
    public ValueTask<TaskViewModel> PostAsync(TaskCreateModel model);
    public ValueTask<TaskViewModel> PutAsync(long id, TaskUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<TaskViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<TaskViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
