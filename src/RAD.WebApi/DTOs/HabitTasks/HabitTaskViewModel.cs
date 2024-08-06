namespace RAD.WebApi.DTOs.HabitTasks;

public class HabitTaskViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long HabitId { get; set; }
    public DateTime TaskDate { get; set; }
    public bool IsCompleted { get; set; }
}
