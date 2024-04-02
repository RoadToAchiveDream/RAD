using RAD_BackEnd.Domain.Enums.TaskEnums;
using RAD_BackEnd.DTOs.Users;
namespace RAD_BackEnd.DTOs.Tasks;
public record TaskViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime ReminderDateTime { get; set; }
    public Reccuring Reccuring { get; set; }

}
