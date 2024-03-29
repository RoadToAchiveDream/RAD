namespace RAD_BackEnd.DTOs.Events;
public record EventUpdateModel(
    long Id,
    long UserId,
    string Title,
    string Description,
    DateTime Start_Time,
    DateTime End_Time,
    string Location,
    DateTime Reminder_DateTime);
