namespace RAD.WebApi.DTOs.Notes;

public record NoteUpdateModel
{
    public string Title { get; set; }
    public string Content { get; set; }
}
