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
    private readonly IAccountService accountService;

    public UserController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [HttpGet("profile")]
    public IActionResult GetUserProfile()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return BadRequest("Nie można uzyskać dostępu do informacji o użytkowniku.");
            }

            var userData = accountService.GetUserData(userId);

            if (userData == null)
            {
                return NotFound("Nie znaleziono użytkownika.");
            }

            return Ok(new
            {
                UserData = userData
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
        }
    }

    [HttpPut("update")]
    public IActionResult UpdateUserProfile([FromBody] UpdateUserDataDto updateUserData)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return BadRequest("Nie można uzyskać dostępu do informacji o użytkowniku.");
            }

            var success = accountService.UpdateUserData(userId, updateUserData);

            if (!success)
            {
                return NotFound("Nie znaleziono użytkownika do zaktualizowania.");
            }

            return Ok("Dane użytkownika zostały zaktualizowane.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
        }
    }
}
