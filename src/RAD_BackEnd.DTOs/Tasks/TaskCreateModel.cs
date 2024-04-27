using RAD_BackEnd.Domain.Enums.TaskEnums;
using TaskStatus = RAD_BackEnd.Domain.Enums.TaskEnums.TaskStatus;

namespace RAD_BackEnd.DTOs.Tasks;
public class TaskCreateModel
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime Reminder { get; set; }
    public Reccuring Reccuring { get; set; }

}
