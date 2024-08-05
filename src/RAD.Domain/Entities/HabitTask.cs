using RAD.Domain.Commons;

namespace RAD.Domain.Entities;

public class HabitTask : Auditable
{
    public long UserId { get; set; }
    public long HabitId { get; set; }
    public DateTime TaskDate { get; set; }
    public bool IsCompleted { get; set; }
}
