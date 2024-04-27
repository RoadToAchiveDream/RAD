using RAD_BackEnd.Domain.Commons;
using RAD_BackEnd.Domain.Enums.TaskEnums;
using RAD_BackEnd.DTOs.Users;

namespace RAD_BackEnd.Domain.Entities;
public class Task : Auditable
{
    public long UserId { get; set; }
    public UserViewModel User { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public Status Status { get; set; } = Status.Pending;
    public DateTime Reminder { get; set; }
    public Reccuring Reccuring { get; set; } = Reccuring.None;
}
