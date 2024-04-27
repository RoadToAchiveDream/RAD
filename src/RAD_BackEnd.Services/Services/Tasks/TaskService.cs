using AutoMapper;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.DTOs.Tasks;
using RAD_BackEnd.Services.Services.Users;
using Task = RAD_BackEnd.Domain.Entities.Task;

namespace RAD_BackEnd.Services.Services.Tasks;

public class TaskService(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper) : ITaskService
{
    public async ValueTask<TaskViewModel> CreateAsync(TaskCreateModel task)
    {
        var existUser = await userService.GetByIdAsync(task.UserId);

        var mapped = mapper.Map<Task>(task);
        var createdTask = await unitOfWork.Tasks.InsertAsync(mapped);
        await unitOfWork.SaveAsync();

        createdTask.User = existUser;
        

    }

    public ValueTask<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<TaskViewModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public ValueTask<TaskViewModel> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TaskViewModel> UpdateAsync(long id, TaskUpdateModel task)
    {
        throw new NotImplementedException();
    }
}
