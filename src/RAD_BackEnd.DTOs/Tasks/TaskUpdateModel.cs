using RAD_BackEnd.Domain.Enums.TaskEnums;
namespace RAD_BackEnd.DTOs.Tasks;
public record TaskUpdateModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime ReminderDateTime { get; set; }
    public Reccuring Reccuring { get; set; }
}
