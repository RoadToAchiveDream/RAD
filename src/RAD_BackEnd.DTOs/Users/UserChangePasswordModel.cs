﻿namespace RAD_BackEnd.DTOs.Users;

public class UserChangePasswordModel
{
    public string Phone { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
