using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;
using ZP_Filapek_Felkel_Makowiejczuk.Interface.Authentication;

namespace ZP_Filapek_Felkel_Makowiejczuk.Controllers;

[Route("test/acount")]
[ApiController]
public class AccountController : Controller
{
    private readonly IAccountService accountService;
    private readonly IValidator<RegisterUser> validator;

    public AccountController(IAccountService accountService, IValidator<RegisterUser> validator)
    {
        this.accountService = accountService;
        this.validator = validator;
    }

    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] RegisterUser registerUser)
    {
        //accountService.RegisterUser(registerUser);
        //return Ok();

        var validateResult = validator.Validate(registerUser);

        if (validateResult.IsValid)
        {
            try
            {
                accountService.RegisterUser(registerUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Register Error: {ex.Message}");
            }
        }
        else
        {
            return BadRequest(validateResult.Errors);
        }
    }

    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] Login loginUser)
    {
        var token = accountService.Login(loginUser);
        if (token != null)
        {
            return Ok(new { Token = token });
        }
        else
        {
            return Unauthorized();
        }
    }

}
