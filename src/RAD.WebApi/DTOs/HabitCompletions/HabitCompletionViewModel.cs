namespace RAD.WebApi.DTOs.HabitCompletions;

public class HabitCompletionViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long HabitTaskId { get; set; }
    public DateTime CompletionDate { get; set; }

}
