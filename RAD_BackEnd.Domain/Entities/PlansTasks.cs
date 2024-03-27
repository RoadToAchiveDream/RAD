using RAD_BackEnd.Domain.Commons;

namespace RAD_BackEnd.Domain.Entities;
public class PlansTasks : Auditable
{
    public long PlanId { get; set; }
    public long TaskId { get; set; }
}
