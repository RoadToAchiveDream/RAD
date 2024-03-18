using RAD_BackEnd.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD_BackEnd.Domain.Entities;
public class PlansTasks:Auditable
{
    public long PlanId {  get; set; }
    public long TaskId {  get; set; }
}
