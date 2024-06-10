using RAD.Domain.Commons;
using RAD.Domain.Enums.TaskEnums;

namespace RAD.Domain.Entities;

public class Task : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long? CategoryId { get; set; }
    public TaskCategory Category { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public Enums.TaskEnums.TaskStatus Status { get; set; } = Enums.TaskEnums.TaskStatus.Pending;
    public DateTime Reminder { get; set; }
    public TaskReccuring Reccuring { get; set; } = TaskReccuring.None;
}
