namespace RAD.WebApi.DTOs.Notes;

public class NoteViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPinned { get; set; }
}

