using RAD.Domain.Enums.NoteEnums;

namespace RAD.DTOs.Notes;
public record NoteUpdateModel
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteCategory Category { get; set; }
}
