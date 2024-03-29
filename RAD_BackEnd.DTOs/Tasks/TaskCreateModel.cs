using RAD_BackEnd.Domain.Enums.TaskEnums;
namespace RAD_BackEnd.DTOs.Tasks;
public record TaskCreateModel(
    long UserId,
    string Title,
    string Description,
    DateTime DueDate,
    Priority Priority,
    Status Status,
    DateTime ReminderDateTime,
    Reccuring Reccuring = Reccuring.None);
