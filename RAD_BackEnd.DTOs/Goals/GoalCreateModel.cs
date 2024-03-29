using RAD_BackEnd.Domain.Enums.GoalEnums;
namespace RAD_BackEnd.DTOs.Goals;
public record GoalCreateModel
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Status status { get; set; }
    public decimal Progress { get; set; }
}
