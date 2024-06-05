using RAD.WebApi.DTOs.Users;

namespace RAD.WebApi.DTOs.Events;
public class EventViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; }
    public DateTime Reminder { get; set; }
}

