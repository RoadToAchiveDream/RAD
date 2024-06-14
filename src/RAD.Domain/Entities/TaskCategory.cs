using RAD.Domain.Commons;

namespace RAD.Domain.Entities;

public class TaskCategory : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public IEnumerable<Task> Tasks { get; set; }
}
