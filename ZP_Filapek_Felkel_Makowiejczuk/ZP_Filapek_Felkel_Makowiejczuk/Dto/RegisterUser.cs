﻿namespace ZP_Filapek_Felkel_Makowiejczuk.Dto;

public class RegisterUser
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
}