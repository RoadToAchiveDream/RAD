using RAD_BackEnd.Domain.Enums.GoalEnums;
using RAD_BackEnd.DTOs.Users;
namespace RAD_BackEnd.DTOs.Goals;
public record GoalViewModel(
    long Id,
    UserViewModel User,
    string Title,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    Status Status,
    decimal Progress);
