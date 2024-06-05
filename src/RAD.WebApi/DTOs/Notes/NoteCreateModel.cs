using RAD.Domain.Enums.NoteEnums;

namespace RAD.WebApi.DTOs.Notes;
public class NoteCreateModel
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteCategory Category { get; set; }
}

