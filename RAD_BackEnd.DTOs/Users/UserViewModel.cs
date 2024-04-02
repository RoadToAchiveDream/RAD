namespace RAD_BackEnd.DTOs.Users;
public record UserViewModel(
    long Id,
    string Username,
    string Password,
    string Email,
    string ProfilePicture,
    string FirstName,
    string LastName);