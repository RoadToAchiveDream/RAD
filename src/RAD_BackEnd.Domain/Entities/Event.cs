using RAD_BackEnd.Domain.Commons;

namespace RAD_BackEnd.Domain.Entities;
public class Event : Auditable
{
    public long UserId { get; set; }
    public User user { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Start_Time { get; set; }
    public DateTime End_Time { get; set; }
    public string Location { get; set; }
    public DateTime Reminder_DateTime { get; set; }
}
