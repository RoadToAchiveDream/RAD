using RAD_BackEnd.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RAD_BackEnd.Domain.Entities;
public class DreamPlans:Auditable
{
    public long DreamId {  get; set; }
    public long PlanId {  get; set; }
}
