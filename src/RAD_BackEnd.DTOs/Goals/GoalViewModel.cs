using RAD_BackEnd.Domain.Enums.GoalEnums;
using RAD_BackEnd.DTOs.Users;

namespace RAD_BackEnd.DTOs.Goals;
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
