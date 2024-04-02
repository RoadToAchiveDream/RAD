using RAD_BackEnd.Domain.Enums.HabitEnums;
namespace RAD_BackEnd.DTOs.Habits;
public class HabitUpdateModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Frequenty Frequenty { get; set; }
    public int Steak { get; set; }
    public int BestSteak { get; set; }
    public DateTime LastCompletedDate { get; set; }
}

