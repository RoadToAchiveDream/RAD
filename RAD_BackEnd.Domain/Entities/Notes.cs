using RAD_BackEnd.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD_BackEnd.Domain.Entities;
public  class Notes:Auditable
{
#pragma warning disable
    public long UserId { get; set; }
    public string Body { get; set; }
}
