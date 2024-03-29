using RAD_BackEnd.Domain.Enums.NoteEnums;
using RAD_BackEnd.DTOs.Users;
namespace RAD_BackEnd.DTOs.Notes;
public record NoteViewModel(
    long Id,
    long UserId,
     UserViewModel User,
    string Title,
    string Content,
    Category category
    );

