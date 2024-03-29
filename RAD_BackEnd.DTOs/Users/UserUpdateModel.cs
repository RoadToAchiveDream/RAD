namespace RAD_BackEnd.DTOs.Users;
public record UserUpdateModel(
    long Id,
    string Username,
    string Password,
    string Email,
    string ProfilePicture,
    string FirstName,
    string LastName);