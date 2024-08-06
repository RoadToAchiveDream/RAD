namespace RAD.WebApi.DTOs.HabitTasks;

public class HabitTaskCreateModel
{
    public long HabitId { get; set; }
    public DateTime TaskDate { get; set; }
    public bool IsCompleted { get; set; }
}
