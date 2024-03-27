namespace RAD_BackEnd.DTOs.TaskModels;

public class TasksCreateModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Body { get; set; }
}
