using RAD_BackEnd.Domain.Commons;
namespace RAD_BackEnd.Domain.Entities;
public class DreamPlans : Auditable
{
    public long DreamId { get; set; }
    public long PlanId { get; set; }
}
