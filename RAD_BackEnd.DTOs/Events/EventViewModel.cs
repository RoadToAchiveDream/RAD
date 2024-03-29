namespace RAD_BackEnd.DTOs.Events;

public record EventViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Start_Time { get; set; }
    public DateTime End_Time { get; set; }
    public string Location { get; set; }
    public DateTime Reminder_DateTime { get; set; }
}
