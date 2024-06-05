using RAD.Domain.Enums.HabitEnums;

namespace RAD.WebApi.DTOs.Habits;
public class HabitUpdateModel
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public HabitFrequenty Frequenty { get; set; }
    public int Steak { get; set; }
    public int BestSteak { get; set; }
    public DateTime LastCompletedDate { get; set; }
}

