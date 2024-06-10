namespace RAD.WebApi.DTOs.TaskCategories;

public class TaskCategoryViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public IEnumerable<Task> Tasks { get; set; }
}


