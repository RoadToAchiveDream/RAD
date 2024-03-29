﻿namespace RAD_BackEnd.DTOs.Users;
public record UserCreateModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string ProfilePicture { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}