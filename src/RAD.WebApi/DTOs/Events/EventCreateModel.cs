namespace RAD.WebApi.DTOs.Events;
public class EventCreateModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; }
    public DateTime Reminder { get; set; }
}

