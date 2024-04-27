using RAD_BackEnd.Domain.Commons;
using RAD_BackEnd.Domain.Enums.NoteEnums;
using RAD_BackEnd.DTOs.Users;

namespace RAD_BackEnd.Domain.Entities;
public class Note : Auditable
{
    public long UserId { get; set; }
    public UserViewModel User { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Category Category { get; set; } = Category.General;
}
