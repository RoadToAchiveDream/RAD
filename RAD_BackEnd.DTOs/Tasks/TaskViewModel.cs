using RAD_BackEnd.Domain.Enums.TaskEnums;
using RAD_BackEnd.DTOs.Users;
namespace RAD_BackEnd.DTOs.Tasks;
public record TaskViewModel(
    long Id,
    UserViewModel User,
    string Title,
    string Description,
    DateTime DueDate,
    Priority Priority,
    Status Status ,
    DateTime ReminderDateTime,
    Reccuring Reccuring = Reccuring.None);
