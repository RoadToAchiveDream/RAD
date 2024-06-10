using RAD.Domain.Commons;

namespace RAD.Domain.Entities;

public class NoteCategory : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public IEnumerable<Note> Notes { get; set; } = new List<Note>();
}
