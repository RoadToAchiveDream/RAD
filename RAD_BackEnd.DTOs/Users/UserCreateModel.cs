namespace RAD_BackEnd.DTOs.Users;
public record UserCreateModel(
    string Username ,
    string Password ,
    string Email ,
    string ProfilePicture,
    string FirstName,
    string LastName);
