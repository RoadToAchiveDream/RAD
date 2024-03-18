using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD_BackEnd.DTOs.PlansModel;
public class PlansViewModel
{
#pragma warning disable
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
