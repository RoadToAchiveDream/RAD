using RAD_BackEnd.Domain.Enums.TaskEnums;
using TaskStatus = RAD_BackEnd.Domain.Enums.TaskEnums.TaskStatus;
namespace RAD_BackEnd.DTOs.Tasks;
public record TaskUpdateModel
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime Reminder { get; set; }
    public TaskReccuring Reccuring { get; set; }
}
