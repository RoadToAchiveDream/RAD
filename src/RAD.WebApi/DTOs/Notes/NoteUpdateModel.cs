namespace RAD.WebApi.DTOs.Notes;
public record NoteUpdateModel
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
