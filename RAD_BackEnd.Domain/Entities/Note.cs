using RAD_BackEnd.Domain.Commons;
using RAD_BackEnd.Domain.Enums;
namespace RAD_BackEnd.Domain.Entities;
public class Note : Auditable
{
    public long UserId { get; set; }
    public User user { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Category category { get; set; }
}
