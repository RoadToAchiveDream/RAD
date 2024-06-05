using RAD.Domain.Commons;
using RAD.Domain.Enums.HabitEnums;

namespace RAD.Domain.Entities;

public class Habit : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public HabitFrequenty Frequenty { get; set; } = HabitFrequenty.Daily;
    public int Steak { get; set; }
    public int BestSteak { get; set; }
    public DateTime LastCompletedDate { get; set; }
}
