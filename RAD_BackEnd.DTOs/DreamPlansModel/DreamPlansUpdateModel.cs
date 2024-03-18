using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD_BackEnd.DTOs.DreamPlansModel;

public class DreamPlansUpdateModel
{
#pragma warning disable
    public long Id { get; set; }
    public long DreamId { get; set; }
    public long PlanId { get; set; }
}
