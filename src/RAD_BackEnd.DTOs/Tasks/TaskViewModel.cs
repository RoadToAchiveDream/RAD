using RAD_BackEnd.Domain.Enums.TaskEnums;
using RAD_BackEnd.DTOs.Users;
using TaskStatus = RAD_BackEnd.Domain.Enums.TaskEnums.TaskStatus;
namespace RAD_BackEnd.DTOs.Tasks;
public record TaskViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime Reminder { get; set; }
    public Reccuring Reccuring { get; set; }

}
