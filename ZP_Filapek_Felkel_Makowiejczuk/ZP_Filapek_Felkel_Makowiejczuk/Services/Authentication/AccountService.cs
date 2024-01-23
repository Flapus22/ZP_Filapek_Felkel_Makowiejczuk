﻿using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZP_Filapek_Felkel_Makowiejczuk.Controllers;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;
using ZP_Filapek_Felkel_Makowiejczuk.Interface.Authentication;
using ZP_Filapek_Felkel_Makowiejczuk.Model.Authentication;

namespace ZP_Filapek_Felkel_Makowiejczuk.Services.Authentication
{
    public class AccountService : IAccountService
    {
        private readonly DBContext context;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;

        public AccountService(DBContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
        }

    public void RegisterUser(RegisterUser registerUser)
    {
        var newUser = new User
        {
            Email = registerUser.Email,
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            DateOfBirth = registerUser.DateOfBirth,
            PasswordHash = passwordHasher.HashPassword(null, registerUser.Password)
        };
        var hashedPassword = passwordHasher.HashPassword(newUser, registerUser.Password);
        newUser.PasswordHash = hashedPassword;

            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public string Login(Login login)
        {
            var user = context.Users.SingleOrDefault(x => x.Email.Equals(login.Email));

            if (user is null)
            {
                throw new Exception("Invalid user");
            }

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid user");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            };

            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer, authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public object GetUserData(string userId)
        {
            var user = context.Users.Find(userId);

            if (user == null)
            {
                return null;
            }

            var userData = new
            {
                UserId = user.Id,
                UserName = $"{user.FirstName} {user.LastName}",
                Email = user.Email
            };

            return userData;
        }

        public bool UpdateUserData(string userId, UserController.UpdateUserDataDto updateUserData)
        {
            var user = context.Users.Find(userId);

            if (user == null)
            {
                return false;
            }

            
            if (!string.IsNullOrEmpty(updateUserData.Email))
            {
                user.Email = updateUserData.Email;
            }

            if (!string.IsNullOrEmpty(updateUserData.Password))
            {
                
                user.PasswordHash = passwordHasher.HashPassword(user, updateUserData.Password);
            }

            if (!string.IsNullOrEmpty(updateUserData.FirstName))
            {
                user.FirstName = updateUserData.FirstName;
            }

            if (!string.IsNullOrEmpty(updateUserData.LastName))
            {
                user.LastName = updateUserData.LastName;
            }

            context.SaveChanges();
            return true;
        }
    }
}
