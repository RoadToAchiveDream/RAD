using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAD_BackEnd.DTOs.TaskModels;

public class TasksCreateModel
{
#pragma warning disable 
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Body { get; set; }
}
