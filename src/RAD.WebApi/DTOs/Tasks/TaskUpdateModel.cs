namespace RAD.WebApi.DTOs.Tasks;

public record TaskUpdateModel
{
    public string Title { get; set; }
    public string Description { get; set; }
}
