using RAD.Domain.Commons;

namespace RAD.Domain.Entities;

public class TaskCategory : Auditable
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }
}
