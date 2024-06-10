using RAD.Domain.Entities;

namespace RAD.WebApi.DTOs.NoteCategories;

public class NoteCategoryViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public IEnumerable<Note> Notes { get; set; }
}
