using RAD_BackEnd.Domain.Enums.GoalEnums;
namespace RAD_BackEnd.DTOs.Goals;
public record GoalCreateModel(
    long UserId,
    string Title,
    string Description ,
    DateTime StartTime,
    DateTime EndTime, 
    Status Status ,
    decimal Progress);

