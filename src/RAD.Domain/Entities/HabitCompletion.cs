using RAD.Domain.Commons;

namespace RAD.Domain.Entities;

public class HabitCompletion : Auditable
{
    public long UserId { get; set; }
    public long HabitTaskId { get; set; }
    public DateTime CompletionDate { get; set; }
}
