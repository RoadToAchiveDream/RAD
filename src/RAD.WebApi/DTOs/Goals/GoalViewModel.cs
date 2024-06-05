using RAD.Domain.Enums.GoalEnums;
using RAD.WebApi.DTOs.Users;

namespace RAD.WebApi.DTOs.Goals;
public class GoalViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public GoalStatus Status { get; set; }
    public decimal Progress { get; set; }
}
