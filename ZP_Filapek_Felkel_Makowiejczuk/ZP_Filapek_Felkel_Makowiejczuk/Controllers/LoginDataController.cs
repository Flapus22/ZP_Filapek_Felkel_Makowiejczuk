using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;
using ZP_Filapek_Felkel_Makowiejczuk.Interface.Authentication;

namespace ZP_Filapek_Felkel_Makowiejczuk.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IAccountService accountService;

        public UserController(IAccountService accountService)
        {
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpGet("profile")]
        public IActionResult GetUserProfile()
        {
            try
            {
                // Pobierz identyfikator, imię i nazwisko zalogowanego użytkownika z JWT
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userName = User.Identity?.Name;

                if (userId == null)
                {
                    return BadRequest("Nie można uzyskać dostępu do informacji o użytkowniku.");
                }

                // Pobierz dane użytkownika
                var userData = accountService.GetUserData(userId);

                if (userData == null)
                {
                    return NotFound("Nie znaleziono użytkownika.");
                }

                // Możesz dostosować odpowiedź, dodając dodatkowe informacje z JWT
                var response = new
                {
                    UserId = userId,
                    UserName = userName,
                    UserData = userData
                };

                return Ok(response);
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
                // Pobierz identyfikator zalogowanego użytkownika
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    return BadRequest("Nie można uzyskać dostępu do informacji o użytkowniku.");
                }

                // Aktualizuj dane użytkownika
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

        public class UpdateUserDataDto
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
        }
    }
}
  