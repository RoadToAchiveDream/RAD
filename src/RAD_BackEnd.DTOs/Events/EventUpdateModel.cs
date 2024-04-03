namespace RAD_BackEnd.DTOs.Events;
public class EventUpdateModel
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; }
    public DateTime Reminder { get; set; }
}
