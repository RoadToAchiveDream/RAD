using RAD_BackEnd.DTOs.Users;

namespace RAD_BackEnd.DTOs.Events;
public class EventCreateModel
{
    public long UserId { get; set; }
    public UserViewModel User { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; }
    public DateTime ReminderDateTime { get; set; }
}

