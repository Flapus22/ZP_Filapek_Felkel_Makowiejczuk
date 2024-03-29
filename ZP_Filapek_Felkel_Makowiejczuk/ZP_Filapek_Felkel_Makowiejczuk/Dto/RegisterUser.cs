﻿namespace ZP_Filapek_Felkel_Makowiejczuk.Dto;
using Data.Model;

public class RegisterUser
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string City { get; set; }
    public string? District { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string HouseNumber { get; set; }
    public UserType UserType { get; set; }
    
}
