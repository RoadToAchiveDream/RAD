using RAD.Domain.Entities;
using RAD.WebApi.DTOs.Notes;

namespace RAD.WebApi.DTOs.NoteCategories;

public class NoteCategoryViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public IEnumerable<NoteViewModel> Notes { get; set; }
}
