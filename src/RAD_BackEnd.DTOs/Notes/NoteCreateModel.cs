using RAD_BackEnd.Domain.Enums.NoteEnums;
namespace RAD_BackEnd.DTOs.Notes;
public record NoteCreateModel(
    long UserId,
    string Title,
    string Content,
    Category category);

