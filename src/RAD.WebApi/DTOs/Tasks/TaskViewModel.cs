using RAD.Domain.Enums.TaskEnums;
using TaskStatus = RAD.Domain.Enums.TaskEnums.TaskStatus;

namespace RAD.WebApi.DTOs.Tasks;

public record TaskViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime Reminder { get; set; }
    public TaskReccuring Reccuring { get; set; }
}
