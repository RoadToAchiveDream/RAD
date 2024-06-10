using RAD.Domain.Enums.GoalEnums;

namespace RAD.WebApi.DTOs.Goals;
public class GoalCreateModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public GoalStatus Status { get; set; }
    public decimal Progress { get; set; }
}

