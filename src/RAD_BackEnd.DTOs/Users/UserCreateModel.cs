﻿namespace RAD_BackEnd.DTOs.Users;
public class UserCreateModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfilePicture { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
