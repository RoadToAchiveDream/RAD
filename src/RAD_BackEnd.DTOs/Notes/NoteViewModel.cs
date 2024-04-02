using RAD_BackEnd.Domain.Enums.NoteEnums;
using RAD_BackEnd.DTOs.Users;
namespace RAD_BackEnd.DTOs.Notes;
public class NoteViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Category Category { get; set; }
}

