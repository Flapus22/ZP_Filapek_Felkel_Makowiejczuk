using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;
using ZP_Filapek_Felkel_Makowiejczuk.Interface.Authentication;

namespace ZP_Filapek_Felkel_Makowiejczuk.Controllers;

[Authorize]
[ApiController]
[Route("api/user")]
public partial class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IValidator<UpdateUserDataDto> validator;

    public UserController(IUserService userService, IValidator<UpdateUserDataDto> validator)
    {
        this.userService = userService;
        this.validator = validator;
    }

    [HttpGet("profile")]
    public IActionResult GetUserProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return BadRequest("Nie można uzyskać dostępu do informacji o użytkowniku.");
        }

        if (!int.TryParse(userId, out var userIdNumber))
        {
            return BadRequest("Nieprawidlowy identyfikator użytkownika");
        }

        var userData = userService.GetUserData(userIdNumber);

        if (userData == null)
        {
            return NotFound("Nie znaleziono użytkownika.");
        }

        return Ok(new
        {
            UserData = userData
        });
    }

    [HttpPut]
    public IActionResult UpdateUserProfile([FromBody] UpdateUserDataDto updateUserData)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return BadRequest("Nie można uzyskać dostępu do informacji o użytkowniku.");
        }

        if (!int.TryParse(userId, out var userIdNumber))
        {
            return BadRequest("Nieprawidlowy identyfikator użytkownika");
        }

        var validationResult = validator.Validate(updateUserData);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.TryAddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        if (!userService.UpdateUserData(userIdNumber, updateUserData))
        {
            return NotFound("Błąd przy aktualizowaniu danych");
        }

        return Ok("Dane użytkownika zostały zaktualizowane.");
    }
}
