using RAD_BackEnd.Domain.Enums.NoteEnums;
namespace RAD_BackEnd.DTOs.Notes;
public record NoteUpdateModel(
    long Id,
    long UserId,
    string Title,
    string Content,
    Category category
    );
