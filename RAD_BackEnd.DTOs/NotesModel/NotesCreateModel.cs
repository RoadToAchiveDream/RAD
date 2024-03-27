namespace RAD_BackEnd.DTOs.NotesModel;
public class NotesCreateModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Body { get; set; }
}
