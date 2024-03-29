using RAD_BackEnd.Domain.Enums.NoteEnums;

namespace RAD_BackEnd.DTOs.Notes;
public record NoteCreateModel
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Category category { get; set; }
}
