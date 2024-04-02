using RAD_BackEnd.Domain.Enums.HabitEnums;
namespace RAD_BackEnd.DTOs.Habits;
public record HabitCreateModel(
    long UserId,
     string Name,
     string Description,
     DateTime StartDate,
     DateTime EndDate,
     Frequenty Frequenty,
     int Steak,
     int BestSteak,
     DateTime LastCompletedDate);

