using RAD_BackEnd.Domain.Enums.HabitEnums;
using RAD_BackEnd.DTOs.Users;
namespace RAD_BackEnd.DTOs.Habits;
public record HabitViewModel(
    long Id,
    long UserId,
    UserViewModel User,
     string Name,
     string Description,
     DateTime StartDate,
     DateTime EndDate,
     Frequenty Frequenty,
     int Steak,
     int BestSteak,
     DateTime LastCompletedDate);
