using RAD_BackEnd.Domain.Commons;
using RAD_BackEnd.Domain.Enums.GoalEnums;
namespace RAD_BackEnd.Domain.Entities;
public class Goal : Auditable
{
    public long UserId { get; set; }
    public User user { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Status status { get; set; }=Status.InProgress;
    public decimal Progress { get; set; }

}
