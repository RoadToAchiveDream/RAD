using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.DTOs.TaskCategories;

public class TaskCategoryViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public IEnumerable<TaskViewModel> Tasks { get; set; }
}


