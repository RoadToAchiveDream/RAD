using RAD.WebApi.DTOs.Users;

namespace RAD.WebApi.DTOs.Notes;
public class NoteViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}

