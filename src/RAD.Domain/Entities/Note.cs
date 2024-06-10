using RAD.Domain.Commons;

namespace RAD.Domain.Entities;

public class Note : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long? CategoryId { get; set; }
    public NoteCategory Category { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPinned { get; set; }
}
